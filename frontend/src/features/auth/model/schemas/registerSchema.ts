import { z } from "zod";

export const registerSchema = z.object({
  emailOrPhone: z
    .string()
    .min(1, "Введіть email або номер телефону")
    .refine(
      (value) => {
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        const phoneRegex = /^\+?[0-9]{10,15}$/;

        return emailRegex.test(value) || phoneRegex.test(value);
      },
      {
        message: "Некоректний email або номер телефону",
      },
    ),

  password: z.string().min(6, "Пароль має містити мінімум 6 символів"),
});

export type RegisterFormData = z.infer<typeof registerSchema>;
