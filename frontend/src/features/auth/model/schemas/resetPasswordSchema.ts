import { z } from "zod";
import { passwordSchema } from "./passwordSchema";

export const resetPasswordSchema = z
  .object({
    contact: z
      .string()
      .min(1, "Поле обов’язкове для заповнення.")
      .refine(
        (value) =>
          z.string().email().safeParse(value).success ||
          /^\+?[0-9]{10,14}$/.test(value),
        "Введіть коректну електронну пошту або номер телефону.",
      ),
    newPassword: passwordSchema,
    confirmPassword: z.string().min(1, "Підтвердіть новий пароль."),
    code: z.string().min(1, "Введіть код підтвердження."),
  })
  .refine((data) => data.newPassword === data.confirmPassword, {
    message: "Паролі не збігаються.",
    path: ["confirmPassword"],
  });

export type ResetPasswordFormData = z.infer<typeof resetPasswordSchema>;