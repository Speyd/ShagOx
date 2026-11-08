import { NumberInput, Select, Switch } from "@mantine/core";
import { Controller, type Control } from "react-hook-form";
import { FormField, TextAreaField, TextField } from "@/shared/ui/form";
import type { FieldConfig, FormValues } from "../model/types";
import styles from "./CrudPage.module.css";

type FieldRendererProps = {
  field: FieldConfig;
  control: Control<FormValues>;
  isEdit: boolean;
};

function buildRules(field: FieldConfig) {
  return {
    validate: (value: unknown) => {
      const empty =
        value === undefined ||
        value === null ||
        (typeof value === "string" && value.trim() === "");

      if (empty) return field.required ? "Обов'язкове поле" : true;

      if (field.kind === "text" && field.pattern && !field.pattern.test(String(value)))
        return field.patternMessage ?? "Невірний формат";

      if ((field.kind === "text" || field.kind === "textarea") && field.maxLength &&
        String(value).length > field.maxLength)
        return `Максимум ${field.maxLength} символів`;

      if (field.kind === "number") {
        const number = Number(value);
        if (field.min !== undefined && number < field.min)
          return `Мінімум ${field.min}`;
        if (field.max !== undefined && number > field.max)
          return `Максимум ${field.max}`;
      }

      return true;
    },
  };
}

export default function FieldRenderer({ field, control, isEdit }: FieldRendererProps) {
  const disabled = Boolean(field.disabledOnEdit && isEdit);
  const id = `field-${field.name}`;

  return (
    <Controller
      control={control}
      name={field.name}
      rules={buildRules(field)}
      render={({ field: input, fieldState }) => {
        const error = fieldState.error?.message;

        if (field.kind === "switch") {
          return (
            <div className={field.fullWidth ? styles.full : undefined}>
              <Switch
                checked={Boolean(input.value)}
                onChange={(event) => input.onChange(event.currentTarget.checked)}
                label={field.label}
                description={field.hint}
                disabled={disabled}
              />
            </div>
          );
        }

        return (
          <FormField
            label={field.label}
            htmlFor={id}
            required={field.required}
            hint={field.hint}
            error={error}
            className={field.fullWidth || field.kind === "textarea" ? styles.full : undefined}
          >
            {field.kind === "text" && (
              <TextField
                id={id}
                ref={input.ref}
                value={String(input.value ?? "")}
                onChange={(event) => input.onChange(event.currentTarget.value)}
                onBlur={input.onBlur}
                placeholder={field.placeholder}
                maxLength={field.maxLength}
                invalid={Boolean(error)}
                disabled={disabled}
              />
            )}

            {field.kind === "textarea" && (
              <TextAreaField
                id={id}
                ref={input.ref}
                value={String(input.value ?? "")}
                onChange={(event) => input.onChange(event.currentTarget.value)}
                onBlur={input.onBlur}
                placeholder={field.placeholder}
                maxLength={field.maxLength}
                invalid={Boolean(error)}
                disabled={disabled}
              />
            )}

            {field.kind === "number" && (
              <NumberInput
                id={id}
                value={(input.value as number | string | null) ?? ""}
                onChange={(value) => input.onChange(value === "" ? null : Number(value))}
                onBlur={input.onBlur}
                placeholder={field.placeholder}
                min={field.min}
                max={field.max}
                allowDecimal={field.allowDecimal ?? true}
                error={Boolean(error)}
                disabled={disabled}
                radius="md"
              />
            )}

            {field.kind === "select" && (
              <Select
                id={id}
                data={field.options}
                value={input.value == null ? null : String(input.value)}
                onChange={(value) => input.onChange(value)}
                onBlur={input.onBlur}
                placeholder={field.placeholder ?? "Оберіть значення"}
                searchable={field.searchable ?? true}
                clearable={field.clearable ?? !field.required}
                nothingFoundMessage="Нічого не знайдено"
                error={Boolean(error)}
                disabled={disabled}
                radius="md"
                comboboxProps={{ withinPortal: true, zIndex: 1100 }}
              />
            )}
          </FormField>
        );
      }}
    />
  );
}
