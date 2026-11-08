import z from "zod";

export const updateCategorySchema = z.object({
  name: z
    .string()
    .min(1, "Введіть назву категорії")
    .max(100, "Назва не може бути довшою за 100 символів"),

  productTypeId: z.number(),
});

export type UpdateCategoryFormValues = z.infer<typeof updateCategorySchema>;
