import { z } from "zod";
import { emailRegex, phoneRegex } from "./contactPatterns";

export type ContactKind = "email" | "phone";

const normalize = (value: string) => value.replace(/[\s()-]/g, "");

export function createChangeContactSchema(
  kind: ContactKind,
  current: string | null | undefined,
) {
  const isEmail = kind === "email";

  return z.object({
    value: z
      .string()
      .trim()
      .min(1, isEmail ? "Введіть нову пошту." : "Введіть новий номер телефону.")
      .refine(
        (value) =>
          isEmail ? emailRegex.test(value) : phoneRegex.test(normalize(value)),
        isEmail
          ? "Введіть коректну електронну пошту."
          : "Введіть номер у форматі +380XXXXXXXXX.",
      )
      .refine(
        (value) =>
          !current ||
          normalize(value).toLowerCase() !== normalize(current).toLowerCase(),
        isEmail
          ? "Нова пошта збігається з поточною."
          : "Новий номер збігається з поточним.",
      )
      .transform((value) => (isEmail ? value : normalize(value))),
  });
}

export type ChangeContactFormInput = { value: string };
export type ChangeContactFormData = { value: string };
