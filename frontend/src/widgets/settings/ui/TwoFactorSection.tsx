import { useState } from "react";
import {
  MessageSquare,
  QrCode,
  ShieldCheck,
  type LucideIcon,
} from "lucide-react";
import { toast } from "sonner";
import clsx from "clsx";
import Button from "@/shared/ui/buttons/button";
import SettingsSection, { StatusBadge } from "./SettingsSection";
import styles from "./Settings.module.css";

type Method = "sms" | "app";

const METHODS: {
  id: Method;
  title: string;
  description: string;
  icon: LucideIcon;
}[] = [
  {
    id: "sms",
    title: "SMS-код",
    description: "Код приходитиме на ваш номер телефону під час входу.",
    icon: MessageSquare,
  },
  {
    id: "app",
    title: "Додаток-автентифікатор",
    description: "Google Authenticator, Microsoft Authenticator або аналог.",
    icon: QrCode,
  },
];

export default function TwoFactorSection() {
  const [method, setMethod] = useState<Method>("app");

  return (
    <SettingsSection
      id="two-factor"
      title="Двофакторна автентифікація (2FA)"
      badge={<StatusBadge tone="danger">Вимкнено</StatusBadge>}
      description="Захистіть свій обліковий запис додатковим рівнем безпеки. Після введення пароля вам знадобиться ввести код, який ми надішлемо на ваш телефон або який згенерує додаток-автентифікатор."
      icon={ShieldCheck}
    >
      <div className={styles.methods} role="radiogroup" aria-label="Спосіб 2FA">
        {METHODS.map((item) => {
          const Icon = item.icon;
          const selected = method === item.id;

          return (
            <button
              key={item.id}
              type="button"
              role="radio"
              aria-checked={selected}
              className={clsx(styles.method, selected && styles.methodActive)}
              onClick={() => setMethod(item.id)}
            >
              <span className={styles.methodIcon}>
                <Icon size={20} />
              </span>
              <span className={styles.methodText}>
                <span className={styles.methodTitle}>{item.title}</span>
                <span className={styles.methodDescription}>
                  {item.description}
                </span>
              </span>
              <span className={styles.radio} aria-hidden />
            </button>
          );
        })}
      </div>

      <div className={styles.sectionActions}>
        <Button
          leftSection={ShieldCheck}
          iconProps={{ size: 16 }}
          onClick={() =>
            toast.info("Двофакторна автентифікація з'явиться незабаром.")
          }
        >
          Налаштувати 2FA
        </Button>
      </div>
    </SettingsSection>
  );
}
