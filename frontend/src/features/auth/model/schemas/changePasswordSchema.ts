import { z } from "zod";
import { passwordSchema } from "./passwordSchema";

export const changePasswordSchema = z
  .object({
    oldPassword: z.string().min(1, "Введіть поточний пароль."),
    newPassword: passwordSchema,
    confirmPassword: z.string().min(1, "Підтвердіть новий пароль."),
  })
  .refine((data) => data.newPassword === data.confirmPassword, {
    message: "Паролі не збігаються.",
    path: ["confirmPassword"],
  })
  .refine((data) => data.newPassword !== data.oldPassword, {
    message: "Новий пароль має відрізнятися від поточного.",
    path: ["newPassword"],
  });

export type ChangePasswordFormData = z.infer<typeof changePasswordSchema>;
