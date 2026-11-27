import { useState } from "react";
import { Collapse } from "@mantine/core";
import { AtSign, Mail, Phone, type LucideIcon } from "lucide-react";
import {
  ChangeContactForm,
  useAuthStore,
  type ContactKind,
} from "@/features/auth";
import Button from "@/shared/ui/buttons/button";
import SettingsSection, { StatusBadge } from "./SettingsSection";
import styles from "./Settings.module.css";

type ContactRow = {
  kind: ContactKind;
  label: string;
  icon: LucideIcon;
  value: string | null;
  hint: string;
};

export default function ContactsSection() {
  const user = useAuthStore((state) => state.user);
  const [editing, setEditing] = useState<ContactKind | null>(null);

  const rows: ContactRow[] = [
    {
      kind: "email",
      label: "Електронна пошта",
      icon: Mail,
      value: user?.email || null,
      hint: "Використовується для входу, чеків та відновлення доступу.",
    },
    {
      kind: "phone",
      label: "Номер телефону",
      icon: Phone,
      value: user?.phone || null,
      hint: "Потрібен для SMS-сповіщень про замовлення та зв'язку з продавцями.",
    },
  ];

  return (
    <SettingsSection
      id="contacts"
      title="Пошта та телефон"
      description="Контактні дані, які використовуються для входу в акаунт і підтвердження важливих дій. Нове значення потрібно підтвердити кодом."
      icon={AtSign}
    >
      <div className={styles.list}>
        {rows.map((row) => {
          const Icon = row.icon;
          const isEditing = editing === row.kind;

          return (
            <div key={row.kind} className={styles.listItem}>
              <div className={styles.listItemRow}>
                <div className={styles.listItemIcon}>
                  <Icon size={20} />
                </div>

                <div className={styles.listItemBody}>
                  <div className={styles.listItemTitleRow}>
                    <span className={styles.listItemTitle}>{row.label}</span>
                    {row.value ? (
                      <StatusBadge tone="success">Підтверджено</StatusBadge>
                    ) : (
                      <StatusBadge tone="warning">Не вказано</StatusBadge>
                    )}
                  </div>
                  <span className={styles.listItemValue}>
                    {row.value ?? "—"}
                  </span>
                  <span className={styles.listItemMeta}>{row.hint}</span>
                </div>

                {!isEditing && (
                  <Button
                    variant="outline"
                    size="sm"
                    onClick={() => setEditing(row.kind)}
                  >
                    {row.value ? "Змінити" : "Додати"}
                  </Button>
                )}
              </div>

              <Collapse expanded={isEditing}>
                {isEditing && (
                  <ChangeContactForm
                    kind={row.kind}
                    currentValue={row.value}
                    className={styles.inlineForm}
                    onCancel={() => setEditing(null)}
                    onSuccess={() => setEditing(null)}
                  />
                )}
              </Collapse>
            </div>
          );
        })}
      </div>
    </SettingsSection>
  );
}
