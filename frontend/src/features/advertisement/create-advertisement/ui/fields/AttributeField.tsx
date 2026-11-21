import {
  MultiSelect,
  NumberInput,
  Select,
  TagsInput,
  TextInput,
} from "@mantine/core";
import { Controller, useFormContext } from "react-hook-form";
import type { AttributeInfo } from "@/entities/advertisement/model/types";
import {
  getAttributeLabel,
  hasPredefinedOptions,
  isNumericAttribute,
} from "../../lib/attributes";
import type { AdvertisementFormValues } from "../../model/schema";
import type { SelectOption } from "../../model/types";
import { fieldProps } from "../shared/fieldProps";

type AttributeFieldProps = {
  definition: AttributeInfo;
  options?: SelectOption[];
};

/** Поле характеристики товару; тип інпуту визначається типом атрибута. */
export default function AttributeField({
  definition,
  options = [],
}: AttributeFieldProps) {
  const { control } = useFormContext<AdvertisementFormValues>();

  return (
    <Controller
      control={control}
      name={`attributes.${definition.key}`}
      render={({ field: { value, onChange, onBlur, ref }, fieldState }) => {
        const common = {
          ...fieldProps,
          ref,
          onBlur,
          label: getAttributeLabel(definition),
          withAsterisk: definition.required,
          error: fieldState.error?.message,
        };
        const list = Array.isArray(value) ? value : [];

        if (hasPredefinedOptions(definition)) {
          return definition.multiple ? (
            <MultiSelect
              {...common}
              data={options}
              value={list}
              onChange={onChange}
              placeholder="Оберіть значення"
              searchable
              clearable
            />
          ) : (
            <Select
              {...common}
              data={options}
              value={typeof value === "string" ? value : null}
              onChange={onChange}
              placeholder="Оберіть значення"
              searchable
              clearable
            />
          );
        }

        if (definition.multiple) {
          return (
            <TagsInput
              {...common}
              value={list}
              onChange={onChange}
              placeholder="Введіть значення та натисніть Enter"
            />
          );
        }

        if (isNumericAttribute(definition)) {
          return (
            <NumberInput
              {...common}
              value={typeof value === "number" ? value : ""}
              onChange={onChange}
              allowDecimal={definition.type === "Decimal"}
              min={definition.min ?? undefined}
              max={definition.max ?? undefined}
              placeholder="Введіть число"
            />
          );
        }

        return (
          <TextInput
            {...common}
            value={typeof value === "string" ? value : ""}
            onChange={(event) => onChange(event.currentTarget.value)}
            placeholder="Введіть значення"
          />
        );
      }}
    />
  );
}
