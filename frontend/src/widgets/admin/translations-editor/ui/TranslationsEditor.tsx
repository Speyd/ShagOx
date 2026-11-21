import { Loader, Text } from "@mantine/core";
import { Languages } from "lucide-react";
import {
  LANGUAGES,
  type TranslationValues,
} from "@/shared/api/translations";
import { TextAreaField, TextField } from "@/shared/ui/form";
import styles from "./TranslationsEditor.module.css";

type TranslationsEditorProps = {
  value: TranslationValues;
  onChange: (value: TranslationValues) => void;
  withDescription?: boolean;
  loading?: boolean;
  error?: string;
};

export default function TranslationsEditor({
  value,
  onChange,
  withDescription,
  loading,
  error,
}: TranslationsEditorProps) {
  const update = (language: string, key: "name" | "description", text: string) =>
    onChange({
      ...value,
      [language]: { ...(value[language] ?? { name: "" }), [key]: text },
    });

  return (
    <section className={styles.section}>
      <div className={styles.header}>
        <Languages size={16} />
        <Text fw={600} size="sm" c="var(--color-base)">
          Назви різними мовами
        </Text>
        {loading && <Loader size={14} />}
      </div>
      <Text size="xs" c="dimmed">
        Відображаються користувачам відповідно до мови інтерфейсу. Українська
        обов'язкова.
      </Text>

      <div className={styles.list}>
        {LANGUAGES.map(({ code, label }) => (
          <div key={code} className={styles.row}>
            <span className={styles.lang}>
              <span className={styles.code}>{code.toUpperCase()}</span>
              {label}
            </span>
            <div className={styles.inputs}>
              <TextField
                value={value[code]?.name ?? ""}
                onChange={(event) => update(code, "name", event.currentTarget.value)}
                placeholder="Назва"
                maxLength={150}
                invalid={code === "uk" && Boolean(error)}
                disabled={loading}
              />
              {withDescription && (
                <TextAreaField
                  rows={2}
                  value={value[code]?.description ?? ""}
                  onChange={(event) =>
                    update(code, "description", event.currentTarget.value)
                  }
                  placeholder="Опис"
                  disabled={loading}
                />
              )}
            </div>
          </div>
        ))}
      </div>

      {error && (
        <Text size="xs" c="red.7">
          {error}
        </Text>
      )}
    </section>
  );
}
