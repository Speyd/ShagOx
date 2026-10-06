import { z } from "zod";

const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
const phoneRegex = /^\+?[0-9]{10,15}$/;
const userNameRegex = /^[\p{L}\p{N}_]{3,20}$/u;

export const loginSchema = z.object({
  emailOrPhoneOrUserName: z
    .string()
    .trim()
    .min(1, "Введіть електронну пошту, телефон або нікнейм.")
    .refine(
      (value) =>
        emailRegex.test(value) ||
        phoneRegex.test(value) ||
        userNameRegex.test(value),
      "Введіть коректну пошту, телефон або нікнейм.",
    ),
  password: z
    .string()
    .refine((value) => value.trim().length > 0, "Введіть пароль."),
});

export type LoginFormData = z.infer<typeof loginSchema>;
