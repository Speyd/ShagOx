import {
  priceRangeSchema,
  type PriceRange,
} from "@/widgets/catalog/model/priceRangeSchema";

export type AttributeInfo = {
  id: number;
  categoryId: number;
  categoryCode: string;
  key: string;
  dictionaryId: number | null;
  type: string;
  required: boolean;
  min: number | null;
  max: number | null;
  isVariant: boolean;
  multiple: boolean;
  lable: string;
};

export type DictionaryValueItem = {
  id: number;
  dictionaryId: number;
  code: string;
  value: string | null;
  label: string;
};

export type CategoryAttributeValueResponse = {
  attribute: AttributeInfo;
  values: DictionaryValueItem[] | null;
};

export function parseAttributeValue(value: string, type: string): unknown {
  const normalizedType = type.toLowerCase();

  if (normalizedType === "integer" || normalizedType === "decimal") {
    const numberValue = Number(value);
    return Number.isFinite(numberValue) ? numberValue : value;
  }

  if (normalizedType === "boolean") {
    if (value.toLowerCase() === "true") return true;
    if (value.toLowerCase() === "false") return false;
  }

  return value;
}

export function buildAttributeFilters(
  items: CategoryAttributeValueResponse[],
  selectedValues: Record<number, string[]>,
  priceRange: PriceRange,
) {
  const attributes: Record<string, unknown[]> = {};
  const variantAttributes: Record<string, unknown> = {};

  for (const item of items) {
    const { attribute } = item;
    const values = selectedValues[attribute.id];
    if (!values || values.length === 0) continue;

    const parsedValues = values.map((val) =>
      parseAttributeValue(val, attribute.type),
    );
    const target = attribute.isVariant ? variantAttributes : attributes;
    target[attribute.key] = parsedValues;
  }

  let minPrice: number | undefined = undefined;
  let maxPrice: number | undefined = undefined;

  const priceValidation = priceRangeSchema.safeParse(priceRange);
  if (priceValidation.success) {
    if (typeof priceRange.min === "number") {
      minPrice = priceRange.min;
    }
    if (typeof priceRange.max === "number") {
      maxPrice = priceRange.max;
    }
  }

  return { attributes, variantAttributes, minPrice, maxPrice };
}
