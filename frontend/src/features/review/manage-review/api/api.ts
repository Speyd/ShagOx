import { api } from "@/shared/api/api";

export type CreateReviewPayload = {
  advertisementId: number;
  text: string;
  grade: number;
  pictures: File[];
};

export type UpdateReviewPayload = {
  id: number;
  text: string;
  grade: number;
};

export async function createReview(payload: CreateReviewPayload) {
  const formData = new FormData();
  formData.append("CommentableEntityId", String(payload.advertisementId));
  formData.append("Text", payload.text);
  formData.append("Grade", String(payload.grade));
  payload.pictures.forEach((file) => formData.append("Pictures", file));

  const { data } = await api.post<{ id: number }>("/review-comment", formData);
  return data;
}

export async function updateReview({ id, text, grade }: UpdateReviewPayload) {
  // Pictures = null → фото залишаються без змін
  await api.put(`/review-comment/${id}`, { text, grade, pictures: null });
}

export async function deleteReview(id: number) {
  await api.delete(`/review-comment/${id}`);
}
