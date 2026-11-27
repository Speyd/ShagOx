import { useMemo, useState } from "react";
import { useNavigate } from "react-router-dom";
import { LogOut, Monitor, MonitorSmartphone, Smartphone } from "lucide-react";
import { useLogout } from "@/features/auth";
import {
  useGetSessions,
  useRevokeAllSessions,
  useRevokeOtherSessions,
  useRevokeSession,
  type UserSession,
} from "@/entities/session";
import Button from "@/shared/ui/buttons/button";
import ConfirmModal from "@/shared/ui/feedback/confirm-modal";
import SettingsSection, { StatusBadge } from "./SettingsSection";
import styles from "./Settings.module.css";

type ConfirmMode = "others" | "all" | null;

function formatSessionTitle(session: UserSession): string {
  const { deviceName, operatingSystem } = session.userDevice.device;
  const baseName =
    deviceName && deviceName !== "Other"
      ? deviceName
      : operatingSystem || "Невідомий пристрій";

  const majorVersion = session.browserVersion?.split(".")[0];
  const browserLabel = majorVersion
    ? `${session.browser} ${majorVersion}`
    : session.browser;

  return browserLabel ? `${baseName} • ${browserLabel}` : baseName;
}

function formatIpAddress(ip: string): string {
  if (!ip || ip === "::1" || ip === "127.0.0.1") {
    return "Localhost";
  }
  return ip;
}

function formatLastUsed(isoDate: string): string {
  const date = new Date(isoDate);
  if (Number.isNaN(date.getTime())) return "Невідомо";

  const diffSeconds = Math.floor((Date.now() - date.getTime()) / 1000);
  if (diffSeconds < 60) return "Активно зараз";

  const diffMinutes = Math.floor(diffSeconds / 60);
  if (diffMinutes < 60) return `${diffMinutes} хв тому`;

  const diffHours = Math.floor(diffMinutes / 60);
  if (diffHours < 24) return `${diffHours} год тому`;

  const diffDays = Math.floor(diffHours / 24);
  if (diffDays < 7) return `${diffDays} дн. тому`;

  return date.toLocaleDateString("uk-UA", {
    day: "numeric",
    month: "short",
    year: "numeric",
  });
}

function isMobileSession(session: UserSession): boolean {
  const { deviceType, operatingSystem } = session.userDevice.device;
  const raw = `${deviceType} ${operatingSystem}`.toLowerCase();
  return (
    raw.includes("mobile") ||
    raw.includes("phone") ||
    raw.includes("android") ||
    raw.includes("ios")
  );
}

const DeviceIcon = ({ session }: { session: UserSession }) =>
  isMobileSession(session) ? <Smartphone size={20} /> : <Monitor size={20} />;

export default function SessionsSection() {
  const navigate = useNavigate();
  const logout = useLogout();

  const { data: sessions = [], isLoading } = useGetSessions();
  const revokeOne = useRevokeSession();
  const revokeOthers = useRevokeOtherSessions();
  const revokeAll = useRevokeAllSessions();

  const [confirmMode, setConfirmMode] = useState<ConfirmMode>(null);

  const { currentSession, otherSessions } = useMemo(() => {
    const active = sessions
      .filter((s) => s.isActive && !s.revokedAt)
      .sort(
        (a, b) =>
          new Date(b.lastUsedAt).getTime() - new Date(a.lastUsedAt).getTime(),
      );

    return {
      currentSession: active[0] ?? null,
      otherSessions: active.slice(1),
    };
  }, [sessions]);

  const logoutHere = () =>
    logout.mutate(undefined, { onSuccess: () => navigate("/") });

  const handleConfirm = () => {
    if (confirmMode === "others") {
      revokeOthers.mutate(undefined, {
        onSuccess: () => setConfirmMode(null),
      });
    } else if (confirmMode === "all") {
      revokeAll.mutate(undefined, {
        onSuccess: () => setConfirmMode(null),
      });
    }
  };

  const isBulkPending = revokeOthers.isPending || revokeAll.isPending;

  return (
    <SettingsSection
      id="sessions"
      title="Активні сесії"
      description="Перегляньте пристрої, на яких наразі здійснено вхід у ваш обліковий запис. Якщо бачите незнайомий пристрій — завершіть сеанс і змініть пароль."
      aside={
        <div className={styles.headerActions}>
          <Button
            size="sm"
            variant="outline"
            disabled={otherSessions.length === 0 || isBulkPending}
            onClick={() => setConfirmMode("others")}
          >
            Завершити інші
          </Button>
          <Button
            size="sm"
            variant="outline"
            disabled={sessions.length === 0 || isBulkPending}
            onClick={() => setConfirmMode("all")}
          >
            Вийти всюди
          </Button>
        </div>
      }
    >
      <div className={styles.list}>
        {isLoading && (
          <p className={styles.emptyNote}>Завантаження активних сеансів...</p>
        )}

        {!isLoading && currentSession && (
          <div className={styles.listItem}>
            <div className={styles.listItemRow}>
              <div className={styles.listItemIcon}>
                <DeviceIcon session={currentSession} />
              </div>
              <div className={styles.listItemBody}>
                <div className={styles.listItemTitleRow}>
                  <span className={styles.listItemTitle}>
                    {formatSessionTitle(currentSession)}
                  </span>
                  <StatusBadge tone="neutral">Цей пристрій</StatusBadge>
                  {currentSession.userDevice.isTrusted && (
                    <StatusBadge tone="success">Довірений</StatusBadge>
                  )}
                </div>
                <span className={styles.listItemMeta}>
                  IP: {formatIpAddress(currentSession.ipAddress)}{" "}
                  <span className={styles.dot} /> Активно зараз
                </span>
              </div>
              <button
                type="button"
                className={styles.iconButton}
                onClick={logoutHere}
                disabled={logout.isPending || isBulkPending}
                aria-label="Вийти на цьому пристрої"
                title="Вийти на цьому пристрої"
              >
                <LogOut size={18} />
              </button>
            </div>
          </div>
        )}

        {!isLoading &&
          otherSessions.map((session) => {
            const title = formatSessionTitle(session);
            const isRevokingThis =
              revokeOne.isPending && revokeOne.variables === session.id;

            return (
              <div key={session.id} className={styles.listItem}>
                <div className={styles.listItemRow}>
                  <div className={styles.listItemIcon}>
                    <DeviceIcon session={session} />
                  </div>
                  <div className={styles.listItemBody}>
                    <div className={styles.listItemTitleRow}>
                      <span className={styles.listItemTitle}>{title}</span>
                      {session.userDevice.isTrusted && (
                        <StatusBadge tone="success">Довірений</StatusBadge>
                      )}
                    </div>
                    <span className={styles.listItemMeta}>
                      IP: {formatIpAddress(session.ipAddress)}{" "}
                      <span className={styles.dot} />{" "}
                      {formatLastUsed(session.lastUsedAt)}
                    </span>
                  </div>
                  <button
                    type="button"
                    className={styles.iconButton}
                    onClick={() => revokeOne.mutate(session.id)}
                    disabled={isRevokingThis || isBulkPending}
                    aria-label={`Завершити сеанс: ${title}`}
                    title="Завершити сеанс"
                  >
                    <LogOut size={18} />
                  </button>
                </div>
              </div>
            );
          })}

        {!isLoading && otherSessions.length === 0 && (
          <p className={styles.emptyNote}>Інших активних сеансів немає.</p>
        )}
      </div>

      <ConfirmModal
        opened={confirmMode !== null}
        onClose={() => !isBulkPending && setConfirmMode(null)}
        onConfirm={handleConfirm}
        icon={MonitorSmartphone}
        iconProps={{ size: 32 }}
        title={
          confirmMode === "all"
            ? "Вийти на всіх пристроях?"
            : "Завершити інші сеанси?"
        }
        message={
          confirmMode === "all"
            ? "Усі сеанси, включно з поточним, буде завершено. Вам доведеться увійти в обліковий запис знову."
            : "Усі сеанси, крім поточного, буде завершено. На інших пристроях потрібно буде увійти знову."
        }
        confirmLabel={confirmMode === "all" ? "Вийти всюди" : "Завершити інші"}
      />
    </SettingsSection>
  );
}
