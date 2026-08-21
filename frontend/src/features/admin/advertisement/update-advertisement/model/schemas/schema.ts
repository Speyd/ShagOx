import { z } from "zod";

export const adminUpdateAdvertisementSchema = z.object({
  title: z
    .string()
    .trim()
    .min(3, "Мінімум 3 символи")
    .max(100, "Максимум 100 символів")
    .refine(
      (value) => /[a-zA-Zа-яА-ЯіїєґІЇЄҐ0-9]/.test(value),
      "Назва повинна містити літери або цифри",
    ),

  description: z
    .string()
    .trim()
    .min(10, "Мінімум 10 символів")
    .max(1000, "Максимум 1000 символів"),

  price: z.coerce
    .number({
      error: "Введіть ціну",
    })
    .int("Ціна повинна бути цілим числом")
    .min(1, "Ціна повинна бути більшою за 0")
    .max(100_000_000, "Занадто велика ціна"),
});

export type UpdateAdvertisementDto = z.infer<
  typeof adminUpdateAdvertisementSchema
>;
