import { api } from "@/shared/api/api";

export type CreateReplyPayload = {
  reviewId: number;
  /** id відповіді, на яку відповідаємо (null — відповідь на сам відгук) */
  parentReplyId: number | null;
  text: string;
};

export async function createReply({ reviewId, parentReplyId, text }: CreateReplyPayload) {
  const formData = new FormData();
  formData.append("CommentableEntityId", String(reviewId));
  formData.append("Text", text);
  if (parentReplyId) formData.append("RepliId", String(parentReplyId));

  const { data } = await api.post<{ id: number }>("/review-repli-comment", formData);
  return data;
}

export async function updateReply({ id, text }: { id: number; text: string }) {
  await api.put(`/review-repli-comment/${id}`, { text, pictures: null });
}

export async function deleteReply(id: number) {
  await api.delete(`/review-repli-comment/${id}`);
}
