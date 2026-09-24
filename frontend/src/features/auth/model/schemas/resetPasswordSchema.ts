import { z } from "zod";

export const resetPasswordSchema = z
  .object({
    contact: z
      .string()
      .min(1, "Поле обов’язкове для заповнення")
      .refine(
        (val) =>
          z.string().email().safeParse(val).success ||
          /^\+?[0-9]{10,14}$/.test(val),
        {
          message: "Введіть коректну електронну пошту або номер телефону",
        },
      ),
    newPassword: z.string().min(6, "Пароль має містити щонайменше 6 символів"),
    confirmPassword: z.string().min(1, "Підтвердіть новий пароль"),
    code: z.string().min(1, "Введіть код підтвердження"),
  })
  .refine((data) => data.newPassword === data.confirmPassword, {
    message: "Паролі не збігаються",
    path: ["confirmPassword"],
  });

export type ResetPasswordFormData = z.infer<typeof resetPasswordSchema>;
