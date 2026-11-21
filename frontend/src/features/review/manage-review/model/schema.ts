import { z } from "zod";

export const REVIEW_TEXT_MAX = 2000;
export const REVIEW_MAX_PHOTOS = 5;

export const reviewSchema = z
  .object({
    grade: z.number().int().min(1, "Поставте оцінку").max(5),
    text: z
      .string()
      .trim()
      .min(3, "Напишіть хоча б кілька слів")
      .max(REVIEW_TEXT_MAX, `Максимум ${REVIEW_TEXT_MAX} символів`),
    keptPictureIds: z.array(z.number()),
    pictures: z.array(z.instanceof(File)),
  })
  .refine(
    ({ keptPictureIds, pictures }) =>
      keptPictureIds.length + pictures.length <= REVIEW_MAX_PHOTOS,
    { path: ["pictures"], message: `Максимум ${REVIEW_MAX_PHOTOS} фото` },
  );

export type ReviewFormValues = z.infer<typeof reviewSchema>;
