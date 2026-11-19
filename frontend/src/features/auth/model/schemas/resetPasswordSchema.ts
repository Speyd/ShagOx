import { z } from "zod";
import { passwordSchema } from "./passwordSchema";

const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
const phoneRegex = /^\+?[0-9]{10,15}$/;

export const contactSchema = z.object({
  contact: z
    .string()
    .trim()
    .min(1, "Поле обов’язкове для заповнення.")
    .refine(
      (value) => emailRegex.test(value) || phoneRegex.test(value),
      "Введіть коректну електронну пошту або номер телефону.",
    ),
});

export const newPasswordSchema = z
  .object({
    newPassword: passwordSchema,
    confirmPassword: z.string().min(1, "Підтвердіть новий пароль."),
  })
  .refine((data) => data.newPassword === data.confirmPassword, {
    message: "Паролі не збігаються.",
    path: ["confirmPassword"],
  });

export type ContactFormData = z.infer<typeof contactSchema>;
export type NewPasswordFormData = z.infer<typeof newPasswordSchema>;
