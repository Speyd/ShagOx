import { useState } from "react";
import { Controller, useFieldArray, useForm, useWatch } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { NumberInput, Textarea, TextInput } from "@mantine/core";
import { Image as ImageIcon, Layers } from "lucide-react";
import { toast } from "sonner";
import type { Advertisement } from "@/shared/lib/types/advertisements";
import type { ImageItem } from "@/shared/lib/types/image";
import ImageUploader from "@/shared/ui/data-display/inputs/image-uploader";
import FormCard from "@/features/advertisement/create-advertisement/ui/shared/FormCard";
import { fieldProps } from "@/features/advertisement/create-advertisement/ui/shared/fieldProps";
import {
  DESCRIPTION_MAX,
  MAX_PHOTOS,
  TITLE_MAX,
} from "@/features/advertisement/create-advertisement/model/schema";
import {
  toFormValues,
  toImageItems,
  toUpdateRequest,
} from "../lib/mapping";
import {
  updateAdvertisementSchema,
  type UpdateAdvertisementFormValues,
} from "../model/schema";
import type { UpdateAdvertisementRequestDto } from "../model/types";
import styles from "./UpdateAdvertisementForm.module.css";

type UpdateAdvertisementFormProps = {
  formId: string;
  advertisement: Advertisement;
  onSubmit: (data: UpdateAdvertisementRequestDto) => void;
};

export default function UpdateAdvertisementForm({
  formId,
  advertisement,
  onSubmit,
}: UpdateAdvertisementFormProps) {
  const [images, setImages] = useState<ImageItem[]>(() =>
    toImageItems(advertisement),
  );
  const [deletedImageIds, setDeletedImageIds] = useState<number[]>([]);
  const [imagesError, setImagesError] = useState("");

  const { control, handleSubmit } = useForm<UpdateAdvertisementFormValues>({
    resolver: zodResolver(updateAdvertisementSchema),
    defaultValues: toFormValues(advertisement),
  });
  const { fields } = useFieldArray({ control, name: "variants" });
  const [title, description] = useWatch({
    control,
    name: ["title", "description"],
  });

  const currencySymbol = advertisement.currency?.symbol ?? "₴";

  const handleImageDelete = (image: ImageItem) => {
    if (image.file) URL.revokeObjectURL(image.url);
    if (image.imageId != null) {
      setDeletedImageIds((prev) => [...prev, image.imageId!]);
    }
    setImages((prev) => prev.filter((item) => item.id !== image.id));
  };

  const submit = handleSubmit(
    (values) => {
      if (images.length === 0) {
        setImagesError("Додайте хоча б 1 фотографію товару");
        return;
      }
      if (images.length > MAX_PHOTOS) {
        setImagesError(`Максимум ${MAX_PHOTOS} фотографій`);
        return;
      }
      setImagesError("");
      onSubmit(toUpdateRequest(advertisement, values, images, deletedImageIds));
    },
    () => toast.error("Перевірте правильність заповнення полів"),
  );

  return (
    <form id={formId} onSubmit={submit} className={styles.form} noValidate>
      <div className={styles.column}>
        <FormCard title="Основна інформація">
          <div className={styles.stack}>
            <Controller
              control={control}
              name="title"
              render={({ field, fieldState }) => (
                <TextInput
                  {...fieldProps}
                  {...field}
                  label="Назва товару"
                  withAsterisk
                  description={`${title.length}/${TITLE_MAX}`}
                  maxLength={TITLE_MAX}
                  error={fieldState.error?.message}
                />
              )}
            />

            <div className={styles.readonlyGrid}>
              <div>
                <span className={styles.readonlyLabel}>Категорія</span>
                <span className={styles.readonlyValue}>
                  {advertisement.category?.label ?? "—"}
                </span>
              </div>
              <div>
                <span className={styles.readonlyLabel}>Стан товару</span>
                <span className={styles.readonlyValue}>
                  {advertisement.condition?.label ??
                    advertisement.condition?.code ??
                    "—"}
                </span>
              </div>
            </div>

            <Controller
              control={control}
              name="description"
              render={({ field, fieldState }) => (
                <Textarea
                  {...fieldProps}
                  {...field}
                  label="Опис товару"
                  withAsterisk
                  description={`${description.length}/${DESCRIPTION_MAX}`}
                  maxLength={DESCRIPTION_MAX}
                  autosize
                  minRows={5}
                  maxRows={12}
                  error={fieldState.error?.message}
                />
              )}
            />
          </div>
        </FormCard>

        <FormCard
          title="Ціна та наявність"
          subtitle="Якщо знизити ціну, попередня покажеться як знижка"
          icon={<Layers size={18} />}
        >
          <div className={styles.variants}>
            {fields.map((field, index) => (
              <div key={field.id} className={styles.variantRow}>
                <span className={styles.variantLabel}>{field.label}</span>

                <Controller
                  control={control}
                  name={`variants.${index}.price`}
                  render={({ field: input, fieldState }) => (
                    <NumberInput
                      {...fieldProps}
                      ref={input.ref}
                      value={input.value}
                      onChange={input.onChange}
                      onBlur={input.onBlur}
                      label="Ціна"
                      min={0}
                      allowNegative={false}
                      decimalScale={2}
                      thousandSeparator=" "
                      rightSection={currencySymbol}
                      error={fieldState.error?.message}
                    />
                  )}
                />

                <Controller
                  control={control}
                  name={`variants.${index}.stock`}
                  render={({ field: input, fieldState }) => (
                    <NumberInput
                      {...fieldProps}
                      ref={input.ref}
                      value={input.value}
                      onChange={input.onChange}
                      onBlur={input.onBlur}
                      label="Кількість"
                      min={0}
                      allowNegative={false}
                      allowDecimal={false}
                      rightSection="шт."
                      error={fieldState.error?.message}
                    />
                  )}
                />
              </div>
            ))}
          </div>
        </FormCard>
      </div>

      <div className={styles.column}>
        <FormCard
          title="Фото товару"
          subtitle="Перетягніть фото, щоб змінити порядок. Перше фото буде головним."
          icon={<ImageIcon size={18} />}
          required
        >
          <ImageUploader
            images={images}
            setImages={setImages}
            onDelete={handleImageDelete}
            setImagesError={setImagesError}
          />
          {imagesError && <p className={styles.errorMessage}>{imagesError}</p>}
        </FormCard>
      </div>
    </form>
  );
}
