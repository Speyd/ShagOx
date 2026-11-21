import { useMemo, useState } from "react";
import { Controller, FormProvider, useForm, useWatch } from "react-hook-form";
import { useMutation } from "@tanstack/react-query";
import { Loader, Select, Text } from "@mantine/core";
import { Image as ImageIcon, Info, Sliders } from "lucide-react";
import { toast } from "sonner";
import { useAdminOptions, useInvalidateAll } from "@/shared/api/crudHooks";
import { getErrorMessage } from "@/shared/api/getErrorMessage";
import type { AdminCategory, AdminCodeEntity, AdminCurrency } from "@/shared/lib/types/admin";
import type { ImageItem } from "@/shared/lib/types/image";
import Button from "@/shared/ui/buttons/button";
import { FormField, TextAreaField, TextField } from "@/shared/ui/form";
import ImageUploader from "@/shared/ui/data-display/inputs/image-uploader";
import { confirmAction } from "@/widgets/admin/row-actions";
import { useCategoryAttributes } from "@/features/advertisement/create-advertisement/model/hooks/useCategoryAttributes";
import { validateAttributeValue } from "@/features/advertisement/create-advertisement/lib/attributes";
import AttributeField from "@/features/advertisement/create-advertisement/ui/fields/AttributeField";
import { saveAdvertisement } from "../api/api";
import { toEditorValues } from "../lib/mapping";
import type { AdminAdvertisement, EditorValues, EditorVariant } from "../model/types";
import EditorCard from "./EditorCard";
import VariantsCard, { type VariantErrors } from "./VariantsCard";
import styles from "./AdvertisementEditor.module.css";

const TITLE_MAX = 120;
const DESCRIPTION_MAX = 2000;
const MAX_PHOTOS = 10;

type AdvertisementEditorProps = {
  advertisement: AdminAdvertisement;
  onSaved?: () => void;
  onCancel?: () => void;
};

const toOptions = <T extends { id: number }>(items: T[], label: (item: T) => string) =>
  items.map((item) => ({ value: String(item.id), label: label(item) }));

export default function AdvertisementEditor({
  advertisement,
  onSaved,
  onCancel,
}: AdvertisementEditorProps) {
  const initial = useMemo(() => toEditorValues(advertisement), [advertisement]);
  const invalidate = useInvalidateAll();

  const form = useForm<EditorValues>({ defaultValues: initial });
  const { control, register, setValue, getValues, setError, clearErrors, formState } = form;
  const { errors } = formState;

  const [variants, setVariants] = useState<EditorVariant[]>(initial.variants);
  const [deletedVariantIds, setDeletedVariantIds] = useState<number[]>([]);
  const [variantErrors, setVariantErrors] = useState<VariantErrors>({});
  const [images, setImages] = useState<ImageItem[]>(initial.images);
  const [deletedImageIds, setDeletedImageIds] = useState<number[]>([]);
  const [imagesError, setImagesError] = useState("");

  const [categoryId, currencyId, title, description] = useWatch({
    control,
    name: ["categoryId", "currencyId", "title", "description"],
  });

  const category = useCategoryAttributes(categoryId ? Number(categoryId) : undefined);

  const { data: categories = [] } = useAdminOptions<AdminCategory>("/admin/categories");
  const { data: conditions = [] } = useAdminOptions<AdminCodeEntity>("/admin/conditions");
  const { data: currencies = [] } = useAdminOptions<AdminCurrency>("/admin/currencies");
  const { data: statuses = [] } = useAdminOptions<AdminCodeEntity>("/admin/statuses");

  const currencySymbol = currencies.find((c) => String(c.id) === currencyId)?.symbol;

  const removeVariant = (variant: EditorVariant) => {
    if (variant.id) setDeletedVariantIds((prev) => [...prev, variant.id!]);
    setVariants((prev) => prev.filter((v) => v.uid !== variant.uid));
  };

  const changeCategory = (next: string | null, apply: (value: string | null) => void) => {
    if (!next || next === getValues("categoryId")) return;
    confirmAction({
      title: "Змінити категорію?",
      message:
        "Характеристики та варіанти належать категорії, тому їх буде скинуто. Існуючі варіанти буде видалено після збереження, ціну першого варіанта збережемо.",
      confirmLabel: "Змінити",
      danger: false,
      onConfirm: () => {
        apply(next);
        setValue("attributes", {});
        const [first] = variants;
        setDeletedVariantIds((prev) => [
          ...prev,
          ...variants.flatMap((v) => (v.id ? [v.id] : [])),
        ]);
        setVariants([
          {
            uid: crypto.randomUUID(),
            options: {},
            labels: {},
            price: first?.price ?? "",
            previousPrice: "",
            stock: first?.stock ?? 1,
          },
        ]);
      },
    });
  };

  const save = useMutation({
    mutationFn: (values: EditorValues) =>
      saveAdvertisement(
        advertisement,
        { ...values, variants, images },
        { deletedVariantIds, deletedImageIds },
        category.definitions,
      ),
    onSuccess: () => {
      invalidate();
      toast.success("Оголошення збережено");
      onSaved?.();
    },
    onError: (error) =>
      toast.error(getErrorMessage(error, "Не вдалося зберегти оголошення")),
  });

  const validateExtra = (values: EditorValues) => {
    let valid = true;
    clearErrors("attributes");

    for (const definition of category.productAttributes) {
      const message = validateAttributeValue(definition, values.attributes[definition.key]);
      if (message) {
        setError(`attributes.${definition.key}`, { message });
        valid = false;
      }
    }

    const nextVariantErrors: VariantErrors = {};
    for (const variant of variants) {
      const rowErrors: VariantErrors[string] = {};
      if (variant.price === "" || variant.price <= 0) rowErrors.price = "Вкажіть ціну";
      if (variant.stock === "" || variant.stock < 0 || !Number.isInteger(variant.stock))
        rowErrors.stock = "Ціле число ≥ 0";
      if (
        !variant.id &&
        variant.previousPrice !== "" &&
        variant.price !== "" &&
        variant.previousPrice <= variant.price
      )
        rowErrors.previousPrice = "Має бути > ціни";
      if (Object.keys(rowErrors).length) nextVariantErrors[variant.uid] = rowErrors;
    }
    setVariantErrors(nextVariantErrors);
    if (Object.keys(nextVariantErrors).length) valid = false;

    if (variants.length === 0) {
      toast.error("Потрібен хоча б один варіант");
      valid = false;
    }

    if (images.length === 0) {
      setImagesError("Додайте хоча б одне фото");
      valid = false;
    } else if (images.length > MAX_PHOTOS) {
      setImagesError(`Максимум ${MAX_PHOTOS} фото`);
      valid = false;
    } else {
      setImagesError("");
    }

    return valid;
  };

  const onSubmit = form.handleSubmit(
    (values) => {
      if (!validateExtra(values)) {
        toast.error("Перевірте правильність заповнення полів");
        return;
      }
      save.mutate(values);
    },
    () => {
      validateExtra(getValues());
      toast.error("Перевірте правильність заповнення полів");
    },
  );

  return (
    <FormProvider {...form}>
      <form onSubmit={onSubmit} className={styles.editor} noValidate>
        <EditorCard title="Основна інформація" icon={<Info size={18} />}>
          <div className={styles.stack}>
            <FormField
              label="Назва"
              htmlFor="ad-title"
              required
              hint={`${title?.length ?? 0}/${TITLE_MAX}`}
              error={errors.title?.message}
            >
              <TextField
                id="ad-title"
                maxLength={TITLE_MAX}
                invalid={Boolean(errors.title)}
                {...register("title", {
                  validate: (value) =>
                    value.trim().length >= 5 || "Назва повинна містити хоча б 5 символів",
                })}
              />
            </FormField>

            <FormField
              label="Опис"
              htmlFor="ad-description"
              required
              hint={`${description?.length ?? 0}/${DESCRIPTION_MAX}`}
              error={errors.description?.message}
            >
              <TextAreaField
                id="ad-description"
                rows={7}
                maxLength={DESCRIPTION_MAX}
                invalid={Boolean(errors.description)}
                {...register("description", {
                  validate: (value) =>
                    value.trim().length >= 10 || "Опис повинен містити хоча б 10 символів",
                })}
              />
            </FormField>

            <div className={styles.grid2}>
              <Controller
                control={control}
                name="categoryId"
                rules={{ required: "Оберіть категорію" }}
                render={({ field, fieldState }) => (
                  <FormField label="Категорія" required error={fieldState.error?.message}>
                    <Select
                      data={toOptions(categories, (c) => c.label || c.code)}
                      value={field.value}
                      onChange={(value) => changeCategory(value, field.onChange)}
                      searchable
                      allowDeselect={false}
                      radius="md"
                      nothingFoundMessage="Категорій не знайдено"
                    />
                  </FormField>
                )}
              />

              <Controller
                control={control}
                name="statusId"
                render={({ field }) => (
                  <FormField label="Статус оголошення">
                    <Select
                      data={toOptions(statuses, (s) => s.label || s.code)}
                      value={field.value}
                      onChange={field.onChange}
                      allowDeselect={false}
                      radius="md"
                    />
                  </FormField>
                )}
              />

              <Controller
                control={control}
                name="conditionId"
                render={({ field }) => (
                  <FormField label="Стан товару" hint="Залиште порожнім, щоб не змінювати">
                    <Select
                      data={toOptions(conditions, (c) => c.label || c.code)}
                      value={field.value}
                      onChange={field.onChange}
                      placeholder="Без змін"
                      clearable
                      radius="md"
                    />
                  </FormField>
                )}
              />

              <Controller
                control={control}
                name="currencyId"
                rules={{ required: "Оберіть валюту" }}
                render={({ field, fieldState }) => (
                  <FormField label="Валюта" required error={fieldState.error?.message}>
                    <Select
                      data={toOptions(currencies, (c) => `${c.code} (${c.symbol})`)}
                      value={field.value}
                      onChange={field.onChange}
                      allowDeselect={false}
                      radius="md"
                    />
                  </FormField>
                )}
              />
            </div>
          </div>
        </EditorCard>

        <EditorCard
          title="Характеристики"
          subtitle="Використовуються у фільтрах каталогу та порівнянні"
          icon={<Sliders size={18} />}
        >
          {category.isLoading ? (
            <div className={styles.loading}>
              <Loader size="sm" />
              <Text size="sm" c="dimmed">
                Завантаження характеристик...
              </Text>
            </div>
          ) : category.productAttributes.length === 0 ? (
            <Text size="sm" c="dimmed">
              Для цієї категорії немає характеристик. Додайте їх у розділі «Категорії».
            </Text>
          ) : (
            <div className={styles.grid2}>
              {category.productAttributes.map((definition) => (
                <AttributeField
                  key={definition.id}
                  definition={definition}
                  options={category.optionsByKey[definition.key]}
                />
              ))}
            </div>
          )}
        </EditorCard>

        <VariantsCard
          variants={variants}
          onChange={setVariants}
          onRemove={removeVariant}
          variantAttributes={category.variantAttributes}
          optionsByKey={category.optionsByKey}
          currencySymbol={currencySymbol}
          errors={variantErrors}
        />

        <EditorCard
          title="Фотографії"
          subtitle={`Перше фото — головне. Перетягуйте, щоб змінити порядок · ${images.length}/${MAX_PHOTOS}`}
          icon={<ImageIcon size={18} />}
        >
          <ImageUploader
            images={images}
            setImages={setImages}
            setImagesError={setImagesError}
            onDelete={(image) => {
              if (image.imageId) setDeletedImageIds((prev) => [...prev, image.imageId!]);
              if (image.file) URL.revokeObjectURL(image.url);
              setImages((prev) => prev.filter((item) => item.id !== image.id));
            }}
          />
          {imagesError && (
            <Text size="sm" c="red.7">
              {imagesError}
            </Text>
          )}
        </EditorCard>

        <div className={styles.saveBar}>
          <Text size="sm" c="dimmed" className={styles.saveHint}>
            {deletedVariantIds.length > 0 &&
              `Буде видалено варіантів: ${deletedVariantIds.length}. `}
            {variants.filter((v) => !v.id).length > 0 &&
              `Нових варіантів: ${variants.filter((v) => !v.id).length}.`}
          </Text>
          {onCancel && (
            <Button variant="ghost" onClick={onCancel} type="button">
              Скасувати
            </Button>
          )}
          <Button type="submit" loading={save.isPending}>
            Зберегти зміни
          </Button>
        </div>
      </form>
    </FormProvider>
  );
}
