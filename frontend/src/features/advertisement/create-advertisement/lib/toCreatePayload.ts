import type { AttributeInfo } from "@/entities/advertisement/model/types";
import type { AdvertisementFormValues } from "../model/schema";
import type { CreateAdvertisementPayload } from "../model/types";
import { toApiAttributes } from "./attributes";

export function toCreatePayload(
  values: AdvertisementFormValues,
  definitions: AttributeInfo[],
  statusId: number,
): CreateAdvertisementPayload {
  const definitionsByKey = new Map(definitions.map((d) => [d.key, d]));

  return {
    title: values.title,
    description: values.description,
    categoryId: Number(values.categoryId),
    conditionId: Number(values.conditionId),
    currencyId: Number(values.currencyId),
    statusId,
    pictures: values.images.map((image) => image.file),
    attributes: toApiAttributes(values.attributes, definitionsByKey),
    variants: values.variants.map((row) => ({
      price: Number(row.price),
      previousPrice: Number(row.previousPrice || 0),
      stock: Number(row.stock),
      attributes: toApiAttributes(row.options, definitionsByKey),
    })),
  };
}
