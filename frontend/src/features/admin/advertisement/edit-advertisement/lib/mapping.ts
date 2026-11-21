import type { AttributeInfo } from "@/entities/advertisement/model/types";
import type {
  AttributeValue,
  AdvertisementVariant,
  VariantAttribute,
} from "@/shared/lib/types/advertisements";
import type { ImageItem } from "@/shared/lib/types/image";
import { toApiAttributes } from "@/features/advertisement/create-advertisement/lib/attributes";
import type { AttributeFormValue } from "@/features/advertisement/create-advertisement/model/types";
import type {
  AdminAdvertisement,
  EditorValues,
  EditorVariant,
} from "../model/types";

function scalarOf(type: string, value: AttributeValue): string | number {
  switch (type) {
    case "Select":
      return String(value.id);
    case "Boolean":
      return String(value.value).toLowerCase() === "true" ? "true" : "false";
    case "Integer":
    case "Decimal":
      return Number(String(value.value).replace(",", "."));
    default:
      return value.value ?? value.label ?? value.code ?? "";
  }
}

export function toFormValue(attr: VariantAttribute): AttributeFormValue {
  const { type, multiple } = attr.attribute;
  const values = attr.values ?? [];

  if (multiple) return values.map((value) => String(scalarOf(type, value)));

  const first = values[0];
  if (!first) return undefined;

  const scalar = scalarOf(type, first);
  return type === "Integer" || type === "Decimal" ? scalar : String(scalar);
}

export function toOptionValue(attr: VariantAttribute): string {
  const value = toFormValue(attr);
  return Array.isArray(value) ? value.join(",") : String(value ?? "");
}

export function variantKey(options: Record<string, string>): string {
  return Object.entries(options)
    .filter(([, value]) => value !== "")
    .map(([key, value]) => `${key}=${value}`)
    .sort()
    .join("|");
}

function toEditorVariant(variant: AdvertisementVariant): EditorVariant {
  const options = Object.fromEntries(
    (variant.attributes ?? [])
      .filter((attr) => attr.attribute)
      .map((attr) => [attr.attribute.key, toOptionValue(attr)]),
  );

  return {
    uid: `existing-${variant.id}`,
    id: variant.id,
    options,
    labels: Object.fromEntries(
      (variant.attributes ?? [])
        .filter((attr) => attr.attribute)
        .map((attr) => [
          attr.attribute.key,
          attr.values
            .map((value) =>
              attr.attribute.type === "Boolean"
                ? String(value.value).toLowerCase() === "true"
                  ? "Так"
                  : "Ні"
                : value.label || value.value || value.code,
            )
            .join(", "),
        ]),
    ),
    price: variant.price,
    previousPrice: variant.previousPrice || "",
    stock: variant.stock,
    initialPrice: variant.price,
    initialStock: variant.stock,
  };
}

export function toEditorValues(ad: AdminAdvertisement): EditorValues {
  const attributes = Array.isArray(ad.attributes) ? ad.attributes : [];

  return {
    title: ad.title,
    description: ad.description,
    categoryId: ad.category ? String(ad.category.id) : null,
    conditionId: null,
    currencyId: ad.currency ? String(ad.currency.id) : null,
    statusId: ad.status ? String(ad.status.id) : null,
    attributes: Object.fromEntries(
      attributes
        .filter((attr) => attr.attribute)
        .map((attr) => [attr.attribute.key, toFormValue(attr)]),
    ),
    variants: (ad.variants ?? []).map(toEditorVariant),
    images: [...(ad.pictures ?? [])]
      .sort((a, b) => a.order - b.order)
      .map<ImageItem>((picture) => ({
        id: String(picture.id),
        imageId: picture.id,
        url: picture.url,
      })),
  };
}

export function toVariantAttributes(
  options: Record<string, string>,
  definitions: AttributeInfo[],
) {
  const byKey = new Map(definitions.map((d) => [d.key, d]));
  const values: Record<string, AttributeFormValue> = {};

  for (const [key, value] of Object.entries(options)) {
    const definition = byKey.get(key);
    if (!definition || value === "") continue;
    values[key] = definition.multiple ? value.split(",") : value;
  }

  return toApiAttributes(values, byKey);
}

export function toProductAttributes(
  values: Record<string, AttributeFormValue>,
  definitions: AttributeInfo[],
) {
  return toApiAttributes(
    values,
    new Map(definitions.filter((d) => !d.isVariant).map((d) => [d.key, d])),
  );
}
