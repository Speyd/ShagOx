import { Button, Group, Rating, Text, Textarea } from "@mantine/core";
import { Controller, useForm, useWatch } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import {
  REVIEW_TEXT_MAX,
  reviewSchema,
  type ReviewFormValues,
} from "../model/schema";
import { ReviewPhotosInput } from "./ReviewPhotosInput";
import styles from "./ReviewForm.module.css";

const GRADE_LABELS = ["", "Жахливо", "Погано", "Нормально", "Добре", "Чудово"];

type ReviewFormProps = {
  defaultValues?: Partial<ReviewFormValues>;
  /** Фото можна додати лише при створенні: редагування змінює текст і оцінку */
  withPhotos?: boolean;
  submitLabel: string;
  isPending: boolean;
  onSubmit: (values: ReviewFormValues) => void;
  onCancel: () => void;
};

export function ReviewForm({
  defaultValues,
  withPhotos = false,
  submitLabel,
  isPending,
  onSubmit,
  onCancel,
}: ReviewFormProps) {
  const { control, handleSubmit } = useForm<ReviewFormValues>({
    resolver: zodResolver(reviewSchema),
    defaultValues: { grade: 0, text: "", pictures: [], ...defaultValues },
  });
  const [grade, text] = useWatch({ control, name: ["grade", "text"] });

  return (
    <form onSubmit={handleSubmit(onSubmit)} className={styles.form} noValidate>
      <Controller
        control={control}
        name="grade"
        render={({ field, fieldState }) => (
          <div>
            <Text fz={14} fw={600} mb={6}>
              Ваша оцінка <span className={styles.required}>*</span>
            </Text>
            <Group gap="sm">
              <Rating
                value={field.value}
                onChange={field.onChange}
                size="lg"
                color="#fea945"
              />
              <Text fz={14} c="dimmed">
                {GRADE_LABELS[grade]}
              </Text>
            </Group>
            {fieldState.error && (
              <Text c="red" fz={12} mt={4}>
                {fieldState.error.message}
              </Text>
            )}
          </div>
        )}
      />

      <Controller
        control={control}
        name="text"
        render={({ field, fieldState }) => (
          <Textarea
            {...field}
            label="Відгук"
            withAsterisk
            placeholder="Розкажіть про якість, відповідність опису, доставку..."
            description={`${text.length}/${REVIEW_TEXT_MAX}`}
            maxLength={REVIEW_TEXT_MAX}
            autosize
            minRows={4}
            maxRows={10}
            error={fieldState.error?.message}
          />
        )}
      />

      {withPhotos && (
        <Controller
          control={control}
          name="pictures"
          render={({ field, fieldState }) => (
            <ReviewPhotosInput
              value={field.value}
              onChange={field.onChange}
              error={fieldState.error?.message}
            />
          )}
        />
      )}

      <Group justify="flex-end" gap="sm">
        <Button variant="default" onClick={onCancel} disabled={isPending}>
          Скасувати
        </Button>
        <Button type="submit" loading={isPending} bg="var(--color-base)">
          {submitLabel}
        </Button>
      </Group>
    </form>
  );
}
