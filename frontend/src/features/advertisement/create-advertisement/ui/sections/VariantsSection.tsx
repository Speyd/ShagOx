import { Select, SimpleGrid } from "@mantine/core";
import { Layers } from "lucide-react";
import { Controller, useFormContext, useWatch } from "react-hook-form";
import { toast } from "sonner";
import type { AttributeInfo } from "@/entities/advertisement/model/types";
import { buildVariantRows, MAX_VARIANTS } from "../../lib/variants";
import type { AdvertisementFormValues } from "../../model/schema";
import type { SelectOption } from "../../model/types";
import VariantNumberField from "../fields/VariantNumberField";
import VariantOptionField from "../fields/VariantOptionField";
import FormCard from "../shared/FormCard";
import { fieldProps } from "../shared/fieldProps";
import VariantsTable from "./VariantsTable";
import styles from "../CreateAdvertisementForm.module.css";

type VariantsSectionProps = {
  variantAttributes: AttributeInfo[];
  optionsByKey: Record<string, SelectOption[]>;
  currencyOptions: SelectOption[];
  currencySymbols: Record<string, string>;
};

/**
 * Ціна та наявність.
 * Без варіантних атрибутів: один набір полів (ціна, стара ціна, кількість).
 * З варіантними атрибутами: користувач обирає значення, а таблиця варіантів
 * будується з усіх їхніх комбінацій.
 */
export default function VariantsSection({
  variantAttributes,
  optionsByKey,
  currencyOptions,
  currencySymbols,
}: VariantsSectionProps) {
  const {
    control,
    getValues,
    setValue,
    formState: { isSubmitted },
  } = useFormContext<AdvertisementFormValues>();
  const [rows, currencyId] = useWatch({
    control,
    name: ["variants", "currencyId"],
  });

  const currencySymbol = currencyId ? currencySymbols[currencyId] : undefined;
  const hasCombinations = rows.some((row) => row.key !== "");

  const handleOptionsChange = (key: string, values: string[]) => {
    const nextRows = buildVariantRows(
      { ...getValues("variantOptions"), [key]: values },
      variantAttributes.map((d) => d.key),
      getValues("variants"),
    );

    if (nextRows.length > MAX_VARIANTS) {
      toast.error(`Можна створити не більше ${MAX_VARIANTS} варіантів`);
      return;
    }

    setValue(`variantOptions.${key}`, values, { shouldValidate: isSubmitted });
    setValue("variants", nextRows);
  };

  const currencySelect = (
    <Controller
      control={control}
      name="currencyId"
      render={({ field, fieldState }) => (
        <Select
          size="sm"
          w={110}
          aria-label="Валюта"
          placeholder="Валюта"
          data={currencyOptions}
          allowDeselect={false}
          value={field.value}
          onChange={field.onChange}
          error={fieldState.error?.message}
        />
      )}
    />
  );

  return (
    <FormCard
      title="Ціна та наявність"
      subtitle={
        variantAttributes.length > 0
          ? "Оберіть значення, і ми сформуємо всі варіанти товару"
          : undefined
      }
      icon={<Layers size={18} />}
      aside={currencySelect}
    >
      <div className={styles.stack}>
        {variantAttributes.length > 0 && (
          <SimpleGrid cols={{ base: 1, sm: 2 }} spacing="md">
            {variantAttributes.map((definition) => (
              <VariantOptionField
                key={definition.id}
                definition={definition}
                options={optionsByKey[definition.key]}
                onChange={handleOptionsChange}
              />
            ))}
          </SimpleGrid>
        )}

        {hasCombinations ? (
          <VariantsTable
            rows={rows}
            variantAttributes={variantAttributes}
            optionsByKey={optionsByKey}
            currencySymbol={currencySymbol}
          />
        ) : (
          <SimpleGrid cols={{ base: 1, sm: 3 }} spacing="md">
            <VariantNumberField
              {...fieldProps}
              index={0}
              name="price"
              label="Ціна"
              withAsterisk
              placeholder="0"
              rightSection={currencySymbol}
            />
            <VariantNumberField
              {...fieldProps}
              index={0}
              name="previousPrice"
              label="Стара ціна"
              description="Для показу знижки"
              placeholder="—"
              rightSection={currencySymbol}
            />
            <VariantNumberField
              {...fieldProps}
              index={0}
              name="stock"
              label="Кількість"
              withAsterisk
              rightSection="шт."
            />
          </SimpleGrid>
        )}
      </div>
    </FormCard>
  );
}
