import { NumberInput, type NumberInputProps } from "@mantine/core";
import { Controller, useFormContext } from "react-hook-form";
import type { AdvertisementFormValues } from "../../model/schema";

type VariantNumberFieldProps = Omit<
  NumberInputProps,
  "name" | "value" | "onChange"
> & {
  index: number;
  name: "price" | "previousPrice" | "stock";
};

/** Числове поле рядка варіанта: ціна, стара ціна або кількість. */
export default function VariantNumberField({
  index,
  name,
  ...inputProps
}: VariantNumberFieldProps) {
  const { control } = useFormContext<AdvertisementFormValues>();
  const isStock = name === "stock";

  return (
    <Controller
      control={control}
      name={`variants.${index}.${name}`}
      render={({ field, fieldState }) => (
        <NumberInput
          {...inputProps}
          ref={field.ref}
          value={field.value}
          onChange={field.onChange}
          onBlur={field.onBlur}
          error={fieldState.error?.message}
          min={0}
          allowNegative={false}
          allowDecimal={!isStock}
          decimalScale={isStock ? 0 : 2}
          thousandSeparator=" "
        />
      )}
    />
  );
}
