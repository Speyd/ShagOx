import { useState, type FormEvent } from "react";
import { Text } from "@mantine/core";
import Button from "@/shared/ui/buttons/button";
import { TextAreaField } from "@/shared/ui/form";
import styles from "./ReplyForm.module.css";

const REPLY_MAX = 1000;

type ReplyFormProps = {
  initialText?: string;
  placeholder?: string;
  submitLabel?: string;
  isPending: boolean;
  onSubmit: (text: string) => void;
  onCancel: () => void;
};

export function ReplyForm({
  initialText = "",
  placeholder = "Напишіть відповідь...",
  submitLabel = "Надіслати",
  isPending,
  onSubmit,
  onCancel,
}: ReplyFormProps) {
  const [text, setText] = useState(initialText);
  const trimmed = text.trim();
  const canSubmit =
    !isPending && trimmed.length > 0 && trimmed !== initialText.trim();

  const submit = () => {
    if (canSubmit) onSubmit(trimmed);
  };

  const handleSubmit = (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    submit();
  };

  return (
    <form className={styles.form} onSubmit={handleSubmit}>
      <TextAreaField
        value={text}
        onChange={(event) => setText(event.currentTarget.value)}
        onKeyDown={(event) => {
          if (event.key === "Enter" && (event.ctrlKey || event.metaKey)) {
            event.preventDefault();
            submit();
          }
          if (event.key === "Escape") onCancel();
        }}
        placeholder={placeholder}
        maxLength={REPLY_MAX}
        rows={3}
        disabled={isPending}
        autoFocus
      />

      <div className={styles.footer}>
        <Text fz={12} c="dimmed">
          {text.length}/{REPLY_MAX} · Ctrl+Enter — надіслати
        </Text>

        <div className={styles.actions}>
          <Button
            type="button"
            variant="ghost"
            size="sm"
            onClick={onCancel}
            disabled={isPending}
          >
            Скасувати
          </Button>
          <Button
            type="submit"
            size="sm"
            loading={isPending}
            disabled={!canSubmit}
          >
            {submitLabel}
          </Button>
        </div>
      </div>
    </form>
  );
}
