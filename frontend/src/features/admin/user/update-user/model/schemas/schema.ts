import { z } from "zod";

export const adminUpdateUserSchema = z.object({
  firstName: z.string().trim().max(50, "Максимум 50 символів"),
  lastName: z.string().trim().max(50, "Максимум 50 символів"),
  userName: z
    .string()
    .trim()
    .min(3, "Мінімум 3 символи")
    .max(40, "Максимум 40 символів")
    .regex(/^[a-zA-Z0-9_.]+$/, "Лише латиниця, цифри, «_» та «.»"),
  bio: z.string().max(500, "Максимум 500 символів"),
  avatar: z.instanceof(File).nullable(),
  cityId: z.string().nullable(),
});

export type UpdateUserDto = z.infer<typeof adminUpdateUserSchema>;
