import { Link } from "react-router-dom";
import { Bell, Check, ShieldAlert, ShieldCheck, X } from "lucide-react";
import clsx from "clsx";
import { useAuthStore, useTwoFactorStatus } from "@/features/auth";
import { SETTINGS_NAV, type SettingsSectionId } from "../model/sections";
import { useActiveSection } from "../lib/useActiveSection";
import styles from "./Settings.module.css";

const SECTION_IDS = SETTINGS_NAV.map((item) => item.id);

export default function SettingsSidebar() {
  const user = useAuthStore((state) => state.user);
  const twoFactorEnabled = useTwoFactorStatus(user?.id);
  const [active, setActive] = useActiveSection(SECTION_IDS);

  const checks = [
    { label: "Пароль встановлено", done: true },
    { label: "Пошту підтверджено", done: Boolean(user?.email) },
    { label: "Телефон додано", done: Boolean(user?.phone) },
    { label: "Увімкнено 2FA", done: twoFactorEnabled === true },
  ];
  const score = checks.filter((check) => check.done).length;
  const percent = Math.round((score / checks.length) * 100);
  const isProtected = score >= 3;

  const goTo = (id: SettingsSectionId) => {
    setActive(id);
    document
      .getElementById(id)
      ?.scrollIntoView({ behavior: "smooth", block: "start" });
  };

  return (
    <aside className={styles.sidebar}>
      <div className={styles.status}>
        <div
          className={clsx(
            styles.statusIcon,
            !isProtected && styles.statusIconWarning,
          )}
        >
          {isProtected ? <ShieldCheck size={22} /> : <ShieldAlert size={22} />}
        </div>
        <div>
          <p className={styles.statusLabel}>Статус акаунта</p>
          <p className={styles.statusValue}>
            {isProtected ? "Захищено" : "Потребує уваги"}
          </p>
        </div>
      </div>

      <div className={styles.progress}>
        <div className={styles.progressTrack}>
          <div
            className={styles.progressBar}
            style={{ width: `${percent}%` }}
          />
        </div>
        <span className={styles.progressValue}>{percent}%</span>
      </div>

      <ul className={styles.checks}>
        {checks.map((check) => (
          <li
            key={check.label}
            className={clsx(styles.check, check.done && styles.checkDone)}
          >
            {check.done ? <Check size={14} /> : <X size={14} />}
            {check.label}
          </li>
        ))}
      </ul>

      <div className={styles.sidebarDivider} />

      <nav className={styles.nav} aria-label="Розділи налаштувань">
        {SETTINGS_NAV.map((item) => {
          const Icon = item.icon;
          return (
            <button
              key={item.id}
              type="button"
              className={clsx(
                styles.navItem,
                active === item.id && styles.navItemActive,
                item.danger && styles.navItemDanger,
              )}
              onClick={() => goTo(item.id)}
            >
              <Icon size={18} />
              <span>{item.label}</span>
            </button>
          );
        })}

        <Link to="/notifications/settings" className={styles.navItem}>
          <Bell size={18} />
          <span>Сповіщення</span>
        </Link>
      </nav>
    </aside>
  );
}
