import { MultiSelect, TagsInput } from "@mantine/core";
import { Controller, useFormContext } from "react-hook-form";
import type { AttributeInfo } from "@/entities/advertisement/model/types";
import {
  getAttributeLabel,
  hasPredefinedOptions,
} from "../../lib/attributes";
import type { AdvertisementFormValues } from "../../model/schema";
import type { SelectOption } from "../../model/types";
import { fieldProps } from "../shared/fieldProps";

type VariantOptionFieldProps = {
  definition: AttributeInfo;
  options?: SelectOption[];
  onChange: (key: string, values: string[]) => void;
};

/** Вибір кількох значень варіантного атрибута (наприклад, кольори або розміри). */
export default function VariantOptionField({
  definition,
  options = [],
  onChange,
}: VariantOptionFieldProps) {
  const { control } = useFormContext<AdvertisementFormValues>();

  return (
    <Controller
      control={control}
      name={`variantOptions.${definition.key}`}
      render={({ field, fieldState }) => {
        const common = {
          ...fieldProps,
          ref: field.ref,
          onBlur: field.onBlur,
          label: getAttributeLabel(definition),
          withAsterisk: definition.required,
          error: fieldState.error?.message,
          value: field.value ?? [],
          onChange: (values: string[]) => onChange(definition.key, values),
        };

        return hasPredefinedOptions(definition) ? (
          <MultiSelect
            {...common}
            data={options}
            placeholder="Оберіть значення"
            searchable
            clearable
          />
        ) : (
          <TagsInput
            {...common}
            placeholder="Введіть значення та натисніть Enter"
            clearable
          />
        );
      }}
    />
  );
}
