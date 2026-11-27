import { z } from "zod";

const NAME_MAX_LENGTH = 35;

export const updateProfileSchema = z.object({
  firstName: z
    .string()
    .trim()
    .min(1, "Введіть ім'я")
    .max(NAME_MAX_LENGTH, `Максимум ${NAME_MAX_LENGTH} символів`),
  lastName: z
    .string()
    .trim()
    .max(NAME_MAX_LENGTH, `Максимум ${NAME_MAX_LENGTH} символів`)
    .optional()
    .default(""),
  userName: z
    .string()
    .trim()
    .min(1, "Введіть ім'я користувача")
    .max(40, "Максимум 40 символів"),
  bio: z.string().max(160, "Максимум 160 символів").optional().default(""),
  email: z.string().email("Некоректний email").or(z.literal("")),
  avatar: z.instanceof(File).nullable().optional(),
  cityId: z.number().nullable(),
  currencyId: z.number().int().positive().nullable(),
});

export type UpdateProfileInput = z.input<typeof updateProfileSchema>;
export type UpdateProfileDto = z.output<typeof updateProfileSchema>;
