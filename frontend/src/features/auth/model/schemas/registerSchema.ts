import { z } from "zod";
import { passwordSchema } from "./passwordSchema";

const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
const phoneRegex = /^\+?[0-9]{10,15}$/;

export const registerSchema = z.object({
  userName: z
    .string()
    .trim()
    .min(3, "Нікнейм має містити щонайменше 3 символи.")
    .max(20, "Нікнейм не може бути довшим за 20 символів.")
    .regex(/^[\p{L}\p{N}_]+$/u, "Використовуйте літери, цифри та _ у нікнеймі."),
  emailOrPhone: z
    .string()
    .trim()
    .min(1, "Введіть електронну пошту або номер телефону.")
    .refine(
      (value) => emailRegex.test(value) || phoneRegex.test(value),
      "Введіть коректну електронну пошту або номер телефону.",
    ),
  password: passwordSchema,
});

export type RegisterFormData = z.infer<typeof registerSchema>;
