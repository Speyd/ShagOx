import { z } from "zod";

const hasUppercase = /\p{Lu}/u;
const hasLowercase = /\p{Ll}/u;
const hasDigit = /\p{Nd}/u;
const hasSpecialCharacter = /[^\p{L}\p{N}\s]/u;

export const passwordSchema = z
  .string()
  .min(8, "Пароль має містити щонайменше 8 символів.")
  .max(128, "Пароль не може бути довшим за 128 символів.")
  .refine(
    (password) =>
      hasUppercase.test(password) &&
      hasLowercase.test(password) &&
      hasDigit.test(password) &&
      hasSpecialCharacter.test(password),
    {
      message:
        "Додайте велику й малу літери, цифру та спеціальний символ.",
    },
  );
