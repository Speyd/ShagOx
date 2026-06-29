import { z } from "zod";

export const loginSchema = z.object({
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

  password: z.string().min(1, "Введіть пароль"),
});

export type LoginFormData = z.infer<typeof loginSchema>;
