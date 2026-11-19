import { z } from "zod";
import type {
  DeliveryCity,
  DeliveryPoint,
  DeliveryStreet,
} from "@/entities/delivery/model/types";

const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
const phoneRegex = /^\+?380\d{9}$/;

export const DELIVERY_METHODS = ["branch", "locker", "courier", "pickup"] as const;
export type DeliveryMethod = (typeof DELIVERY_METHODS)[number];

export const checkoutSchema = z
  .object({
    firstName: z.string().trim().min(2, "Введіть ім'я."),
    lastName: z.string().trim().min(2, "Введіть прізвище."),
    phone: z
      .string()
      .trim()
      .regex(phoneRegex, "Введіть телефон у форматі +380XXXXXXXXX."),
    email: z.string().trim().regex(emailRegex, "Некоректна електронна пошта."),
    deliveryMethod: z.enum(DELIVERY_METHODS),
    city: z.custom<DeliveryCity>().nullable(),
    point: z.custom<DeliveryPoint>().nullable(),
    street: z.custom<DeliveryStreet>().nullable(),
    building: z.string().trim(),
    flat: z.string().trim(),
    paymentMethod: z.enum(["CashOnDelivery", "CardOnDelivery"]),
    comment: z.string().trim().max(500, "Не більше 500 символів."),
  })
  .superRefine((data, ctx) => {
    if (data.deliveryMethod === "pickup") return;

    if (!data.city) {
      ctx.addIssue({ code: "custom", path: ["city"], message: "Оберіть місто." });
      return;
    }

    if (data.deliveryMethod === "courier") {
      if (!data.street) {
        ctx.addIssue({ code: "custom", path: ["street"], message: "Оберіть вулицю." });
      }
      if (!data.building) {
        ctx.addIssue({ code: "custom", path: ["building"], message: "Введіть номер будинку." });
      }
      return;
    }

    if (!data.point) {
      ctx.addIssue({ code: "custom", path: ["point"], message: "Оберіть відділення." });
    }
  });

export type CheckoutFormData = z.infer<typeof checkoutSchema>;
