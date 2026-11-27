import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { useQueryClient } from "@tanstack/react-query";
import { Mail, Phone, ShieldCheck, ShieldOff } from "lucide-react";
import { toast } from "sonner";
import {
  CodeConfirmation,
  useAuthStore,
  useRequestTwoFactorChange,
  useTwoFactorStatus,
  useTwoFactorStore,
  useVerify,
  type TwoFactorAction,
} from "@/features/auth";
import Button from "@/shared/ui/buttons/button";
import { getErrorMessage } from "@/shared/api/getErrorMessage";
import SettingsSection, { StatusBadge } from "./SettingsSection";
import styles from "./Settings.module.css";

const ACTION_TEXT: Record<
  TwoFactorAction,
  { submit: string; success: string }
> = {
  enable: {
    submit: "Увімкнути 2FA",
    success: "Двофакторну автентифікацію увімкнено. Увійдіть знову.",
  },
  disable: {
    submit: "Вимкнути 2FA",
    success: "Двофакторну автентифікацію вимкнено. Увійдіть знову.",
  },
};

export default function TwoFactorSection() {
  const navigate = useNavigate();
  const queryClient = useQueryClient();
  const user = useAuthStore((state) => state.user);
  const logoutUser = useAuthStore((state) => state.logout);
  const enabled = useTwoFactorStatus(user?.id);
  const setStatus = useTwoFactorStore((state) => state.setStatus);

  const [pending, setPending] = useState<TwoFactorAction | null>(null);
  const requestChange = useRequestTwoFactorChange();
  const verify = useVerify(null);

  const contact = user?.email || user?.phone || null;
  const ContactIcon = user?.email ? Mail : Phone;

  const request = async (action: TwoFactorAction) => {
    try {
      await requestChange.mutateAsync(action);
      setPending(action);
      toast.success("Код підтвердження надіслано.");
    } catch (error) {
      toast.error(getErrorMessage(error, "Не вдалося надіслати код."));
    }
  };

  const resend = async () => {
    if (!pending) return;

    try {
      await requestChange.mutateAsync(pending);
      toast.success("Новий код надіслано.");
    } catch (error) {
      toast.error(getErrorMessage(error, "Не вдалося надіслати код."));
      throw error;
    }
  };

  const confirm = async (code: string) => {
    if (!pending || !user) return false;

    try {
      await verify.mutateAsync({ code });
    } catch {
      return false;
    }

    setStatus(user.id, pending === "enable");
    toast.success(ACTION_TEXT[pending].success);

    logoutUser();
    queryClient.clear();
    navigate("/authentication", { replace: true });

    return true;
  };

  const badge =
    enabled === true ? (
      <StatusBadge tone="success">Увімкнено</StatusBadge>
    ) : enabled === false ? (
      <StatusBadge tone="danger">Вимкнено</StatusBadge>
    ) : (
      <StatusBadge tone="neutral">Статус невідомий</StatusBadge>
    );

  return (
    <SettingsSection
      id="two-factor"
      title="Двофакторна автентифікація (2FA)"
      badge={badge}
      description="Після введення пароля на новому пристрої ми надішлемо одноразовий код на вашу пошту (або на телефон, якщо пошта не підтверджена). Пристрій, з якого ви підтвердили вхід, стає довіреним, і код на ньому більше не запитується."
      icon={ShieldCheck}
    >
      <div className={styles.listItem}>
        <div className={styles.listItemRow}>
          <div className={styles.listItemIcon}>
            <ContactIcon size={20} />
          </div>
          <div className={styles.listItemBody}>
            <span className={styles.listItemTitle}>Куди надходять коди</span>
            <span className={styles.listItemValue}>{contact ?? "—"}</span>
            <span className={styles.listItemMeta}>
              Після увімкнення або вимкнення 2FA всі сеанси буде завершено, а
              довірені пристрої — скинуто. Потрібно буде увійти знову.
            </span>
          </div>
        </div>
      </div>

      {pending ? (
        <div className={styles.narrowForm}>
          <CodeConfirmation
            description={
              <>
                Ми надіслали код підтвердження
                {contact ? ` на ${contact}` : ""}. Введіть його, щоб{" "}
                {pending === "enable" ? "увімкнути" : "вимкнути"} 2FA. Якщо код
                не надійшов, можливо, 2FA вже{" "}
                {pending === "enable" ? "увімкнено" : "вимкнено"}.
              </>
            }
            submitLabel={ACTION_TEXT[pending].submit}
            onSubmit={confirm}
            onResend={resend}
            isSubmitting={verify.isPending}
            isResending={requestChange.isPending}
            secondaryAction={
              <Button
                type="button"
                variant="ghost"
                onClick={() => setPending(null)}
                disabled={verify.isPending}
              >
                Скасувати
              </Button>
            }
          />
        </div>
      ) : (
        <div className={styles.sectionActions}>
          {enabled !== true && (
            <Button
              leftSection={ShieldCheck}
              iconProps={{ size: 16 }}
              loading={
                requestChange.isPending && requestChange.variables === "enable"
              }
              disabled={requestChange.isPending || !contact}
              onClick={() => request("enable")}
            >
              {ACTION_TEXT.enable.submit}
            </Button>
          )}
          {enabled !== false && (
            <Button
              variant="outline"
              leftSection={ShieldOff}
              iconProps={{ size: 16 }}
              loading={
                requestChange.isPending && requestChange.variables === "disable"
              }
              disabled={requestChange.isPending || !contact}
              onClick={() => request("disable")}
            >
              {ACTION_TEXT.disable.submit}
            </Button>
          )}
        </div>
      )}
    </SettingsSection>
  );
}
