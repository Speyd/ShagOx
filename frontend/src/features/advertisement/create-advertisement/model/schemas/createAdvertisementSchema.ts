import { z } from "zod";

export const createAdvertisementSchema = z.object({
  title: z
    .string()
    .trim()
    .min(5, "Назва повинна містити хоча б 5 символів")
    .max(120, "Назва не може перевищувати 120 символів"),

  categoryId: z
    .string()
    .min(1, "Оберіть категорію товару"),

  price: z
    .number({
      required_error: "Вкажіть ціну товару",
      invalid_type_error: "Ціна повинна бути числом",
    })
    .min(1, "Ціна повинна бути більшою за 0")
    .max(100_000_000, "Ціна занадто велика"),

  stock: z
    .number({
      required_error: "Вкажіть кількість",
      invalid_type_error: "Кількість повинна бути числом",
    })
    .int("Кількість повинна бути цілим числом")
    .min(1, "Мінімум 1 одиниця товару")
    .max(10_000, "Кількість занадто велика"),

  description: z
    .string()
    .trim()
    .min(10, "Опис повинен містити хоча б 10 символів")
    .max(2000, "Опис не може перевищувати 2000 символів"),

  hashtags: z.array(z.string()).default([]),

  brand: z.string().trim().max(100).optional().default(""),

  conditionId: z.string().min(1, "Оберіть стан товару"),

  tags: z.string().trim().max(300).optional().default(""),

  attributes: z.record(z.string(), z.unknown()).default({}),

  variantAttributes: z.record(z.string(), z.unknown()).default({}),
});

export type CreateAdvertisementSchema = z.infer<typeof createAdvertisementSchema>;

