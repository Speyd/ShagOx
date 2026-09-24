import { z } from "zod";

export const registerSchema = z.object({
  userName: z
    .string()
    .trim()
    .min(3, "Нікнейм має містити мінімум 3 символи")
    .max(30, "Нікнейм не може бути довшим за 30 символів")
    .regex(
      /^[A-Za-zА-Яа-яЁё0-9._-]+$/,
      "Нікнейм може містити лише літери, цифри, крапку, дефіс та нижнє підкреслення",
    ),
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
