import { z } from "zod";
import type { AttributeInfo } from "@/entities/advertisement/model/types";
import { validateAttributeValue } from "../lib/attributes";
import { createVariantRow } from "../lib/variants";
import type { AttributeFormValue, ImageFileItem } from "./types";

export const TITLE_MAX = 120;
export const DESCRIPTION_MAX = 2000;
export const MAX_PHOTOS = 10;

const requiredId = (message: string) =>
  z
    .string()
    .nullable()
    .refine((value) => Boolean(value), message);

const numberOrEmpty = z.union([z.number(), z.literal("")]);

const variantRowSchema = z
  .object({
    key: z.string(),
    options: z.record(z.string(), z.string()),
    price: numberOrEmpty.refine(
      (value) => value !== "" && value > 0,
      "Вкажіть ціну",
    ),
    previousPrice: numberOrEmpty,
    stock: numberOrEmpty.refine(
      (value) => value !== "" && Number.isInteger(value) && value >= 0,
      "Вкажіть кількість",
    ),
  })
  .refine(
    ({ price, previousPrice }) =>
      previousPrice === "" || price === "" || previousPrice > price,
    {
      path: ["previousPrice"],
      message: "Має бути більшою за ціну",
    },
  );

/** Схема одного атрибута будується з його визначення (required, min, max, type). */
const attributeSchema = <T extends AttributeFormValue>(
  definition: AttributeInfo,
) =>
  z.custom<T>().superRefine((value, ctx) => {
    const message = validateAttributeValue(definition, value);
    if (message) ctx.addIssue({ code: "custom", message });
  });

const shapeOf = <T extends AttributeFormValue>(definitions: AttributeInfo[]) =>
  Object.fromEntries(
    definitions.map((definition) => [
      definition.key,
      attributeSchema<T>(definition),
    ]),
  );

/**
 * Схема залежить від атрибутів обраної категорії:
 * - attributes: характеристики товару (isVariant = false);
 * - variantOptions: значення, з яких генеруються варіанти (isVariant = true).
 */
export function createAdvertisementSchema(definitions: AttributeInfo[]) {
  const productAttributes = definitions.filter((d) => !d.isVariant);
  const variantAttributes = definitions.filter((d) => d.isVariant);

  return z.object({
    title: z
      .string()
      .trim()
      .min(5, "Назва повинна містити хоча б 5 символів")
      .max(TITLE_MAX, `Назва не може перевищувати ${TITLE_MAX} символів`),
    description: z
      .string()
      .trim()
      .min(10, "Опис повинен містити хоча б 10 символів")
      .max(
        DESCRIPTION_MAX,
        `Опис не може перевищувати ${DESCRIPTION_MAX} символів`,
      ),
    categoryId: requiredId("Оберіть категорію"),
    conditionId: requiredId("Оберіть стан товару"),
    currencyId: requiredId("Оберіть валюту"),
    images: z
      .array(z.custom<ImageFileItem>())
      .min(1, "Додайте хоча б 1 фотографію товару")
      .max(MAX_PHOTOS, `Максимум ${MAX_PHOTOS} фотографій`),
    attributes: z.object(shapeOf<AttributeFormValue>(productAttributes)),
    variantOptions: z.object(
      shapeOf<string[] | undefined>(variantAttributes),
    ),
    variants: z.array(variantRowSchema).min(1, "Додайте хоча б один варіант"),
  });
}

export type AdvertisementFormValues = z.infer<
  ReturnType<typeof createAdvertisementSchema>
>;

export const DEFAULT_VALUES: AdvertisementFormValues = {
  title: "",
  description: "",
  categoryId: null,
  conditionId: null,
  currencyId: null,
  images: [],
  attributes: {},
  variantOptions: {},
  variants: [createVariantRow()],
};
