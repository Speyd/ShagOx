import { z } from "zod";

export const adminUpdateUserSchema = z.object({
  surname: z.string().min(1, "Введіть прізвище"),
  name: z.string().min(1, "Введіть ім'я"),
  phone: z.string().min(1, "Введіть телефон"),
  email: z.string().email("Некоректний email"),
  avatar: z.instanceof(File).nullable().optional(),
  cityId: z.number().nullable(),
});

export type UpdateUserDto = z.infer<typeof adminUpdateUserSchema>;
