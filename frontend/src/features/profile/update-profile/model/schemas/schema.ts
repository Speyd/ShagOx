import { z } from "zod";

export const updateProfileSchema = z.object({
  firstName: z.string().min(1, "Введіть ім'я"),
  lastName: z.string().min(1, "Введіть прізвище"),
  userName: z
    .string()
    .min(1, "Введіть ім'я користувача")
    .max(40, "Максимум 40 символів"),
  bio: z.string().max(160, "Максимум 160 символів").optional().default(""),
  email: z.string().email("Некоректний email").or(z.literal("")),
  avatar: z.instanceof(File).nullable().optional(),
  cityId: z.number().nullable(),
});

export type UpdateProfileDto = z.infer<typeof updateProfileSchema>;
