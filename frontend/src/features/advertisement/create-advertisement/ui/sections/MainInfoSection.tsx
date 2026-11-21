import { useMemo } from "react";
import { Select, SimpleGrid, Textarea, TextInput } from "@mantine/core";
import { Controller, useFormContext, useWatch } from "react-hook-form";
import { useGetCategories } from "@/entities/advertisement/model/hooks/useGetCategories";
import { createVariantRow } from "../../lib/variants";
import {
  DESCRIPTION_MAX,
  TITLE_MAX,
  type AdvertisementFormValues,
} from "../../model/schema";
import type { SelectOption } from "../../model/types";
import FormCard from "../shared/FormCard";
import { fieldProps } from "../shared/fieldProps";
import styles from "../CreateAdvertisementForm.module.css";

type MainInfoSectionProps = {
  conditionOptions: SelectOption[];
};

export default function MainInfoSection({
  conditionOptions,
}: MainInfoSectionProps) {
  const { control, getValues, setValue, clearErrors } =
    useFormContext<AdvertisementFormValues>();
  const [title, description] = useWatch({
    control,
    name: ["title", "description"],
  });

  const { data: categories = [], isLoading: isLoadingCategories } =
    useGetCategories();
  const categoryOptions = useMemo(
    () => categories.map((c) => ({ value: String(c.id), label: c.label })),
    [categories],
  );

  /** Атрибути належать категорії, тому при її зміні скидаються (ціна зберігається). */
  const resetCategoryData = () => {
    const [firstRow] = getValues("variants");
    setValue("attributes", {});
    setValue("variantOptions", {});
    setValue("variants", [createVariantRow({}, firstRow)]);
    clearErrors(["attributes", "variantOptions", "variants"]);
  };

  return (
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
              placeholder="Наприклад: iPhone 15 Pro 256GB"
              description={`${title.length}/${TITLE_MAX}`}
              maxLength={TITLE_MAX}
              error={fieldState.error?.message}
            />
          )}
        />

        <SimpleGrid cols={{ base: 1, sm: 2 }} spacing="md">
          <Controller
            control={control}
            name="categoryId"
            render={({ field, fieldState }) => (
              <Select
                {...fieldProps}
                ref={field.ref}
                label="Категорія"
                withAsterisk
                placeholder="Оберіть категорію"
                data={categoryOptions}
                disabled={isLoadingCategories}
                searchable
                nothingFoundMessage="Категорій не знайдено"
                value={field.value}
                onChange={(value) => {
                  if (value === field.value) return;
                  field.onChange(value);
                  resetCategoryData();
                }}
                onBlur={field.onBlur}
                error={fieldState.error?.message}
              />
            )}
          />

          <Controller
            control={control}
            name="conditionId"
            render={({ field, fieldState }) => (
              <Select
                {...fieldProps}
                ref={field.ref}
                label="Стан товару"
                withAsterisk
                placeholder="Оберіть стан"
                data={conditionOptions}
                value={field.value}
                onChange={field.onChange}
                onBlur={field.onBlur}
                error={fieldState.error?.message}
              />
            )}
          />
        </SimpleGrid>

        <Controller
          control={control}
          name="description"
          render={({ field, fieldState }) => (
            <Textarea
              {...fieldProps}
              {...field}
              label="Опис товару"
              withAsterisk
              placeholder="Опишіть товар, його особливості, комплектацію, переваги..."
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
  );
}
