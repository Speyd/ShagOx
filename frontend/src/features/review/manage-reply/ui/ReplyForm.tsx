import { useState } from "react";
import { Button, Group, Textarea } from "@mantine/core";

const REPLY_MAX = 1000;

type ReplyFormProps = {
  initialText?: string;
  placeholder?: string;
  submitLabel?: string;
  isPending: boolean;
  onSubmit: (text: string) => void;
  onCancel: () => void;
};

/** Компактна інлайн-форма відповіді (Ctrl+Enter — надіслати, Esc — скасувати). */
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

  const submit = () => {
    if (trimmed) onSubmit(trimmed);
  };

  return (
    <div>
      <Textarea
        value={text}
        onChange={(event) => setText(event.currentTarget.value)}
        onKeyDown={(event) => {
          if (event.key === "Enter" && (event.ctrlKey || event.metaKey)) submit();
          if (event.key === "Escape") onCancel();
        }}
        placeholder={placeholder}
        maxLength={REPLY_MAX}
        autosize
        minRows={2}
        maxRows={6}
        autoFocus
      />
      <Group justify="flex-end" gap="xs" mt="xs">
        <Button size="xs" variant="subtle" onClick={onCancel} disabled={isPending}>
          Скасувати
        </Button>
        <Button
          size="xs"
          onClick={submit}
          loading={isPending}
          disabled={!trimmed || trimmed === initialText.trim()}
        >
          {submitLabel}
        </Button>
      </Group>
    </div>
  );
}
