import { z } from "zod";
import {
  DESCRIPTION_MAX,
  TITLE_MAX,
} from "@/features/advertisement/create-advertisement/model/schema";

const numberOrEmpty = z.union([z.number(), z.literal("")]);

export const updateAdvertisementSchema = z.object({
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
  variants: z.array(
    z.object({
      id: z.number(),
      label: z.string(),
      price: numberOrEmpty.refine(
        (value) => value !== "" && value > 0,
        "Вкажіть ціну",
      ),
      stock: numberOrEmpty.refine(
        (value) => value !== "" && Number.isInteger(value) && value >= 0,
        "Вкажіть кількість",
      ),
    }),
  ),
});

export type UpdateAdvertisementFormValues = z.infer<
  typeof updateAdvertisementSchema
>;
