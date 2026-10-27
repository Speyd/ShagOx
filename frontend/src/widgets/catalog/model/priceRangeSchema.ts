import z from "zod";

export const priceRangeSchema = z
  .object({
    min: z.union([z.number(), z.literal("")]),
    max: z.union([z.number(), z.literal("")]),
  })
  .refine(
    (data) => {
      if (typeof data.min === "number" && typeof data.max === "number") {
        return data.min <= data.max;
      }
      return true;
    },
    {
      message: "Мінімальна ціна не може бути більшою за максимальну",
      path: ["max"],
    },
  );

export type PriceRange = z.infer<typeof priceRangeSchema>;
