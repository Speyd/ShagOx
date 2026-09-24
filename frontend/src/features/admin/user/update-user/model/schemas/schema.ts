import { z } from "zod";

export const adminUpdateUserSchema = z.object({
  firstName: z.string().min(1, "Введіть ім'я"),
  lastName: z.string().min(1, "Введіть прізвище"),
  userName: z.string().min(1, "Введіть нікнейм").max(40, "Максимум 40 символів"),
  bio: z.string().max(500, "Максимум 500 символів").optional().default(""),
  phone: z.string().min(1, "Введіть телефон"),
  email: z.string().email("Некоректний email"),
  avatar: z.instanceof(File).nullable().optional(),
  cityId: z.number().nullable(),
});

export type UpdateUserDto = z.infer<typeof adminUpdateUserSchema>;
