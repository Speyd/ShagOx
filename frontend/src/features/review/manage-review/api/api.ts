import { api } from "@/shared/api/api";

export type CreateReviewPayload = {
  advertisementId: number;
  text: string;
  grade: number;
  pictures: File[];
};

export type ReviewPictureChange = {
  id?: number;
  file?: File;
  order: number;
  isDeleted: boolean;
};

export type UpdateReviewPayload = {
  id: number;
  text: string;
  grade: number;
  pictures?: ReviewPictureChange[];
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

export async function updateReview({ id, text, grade, pictures }: UpdateReviewPayload) {
  const formData = new FormData();
  formData.append("Text", text);
  formData.append("Grade", String(grade));

  pictures?.forEach((picture, index) => {
    if (picture.id != null) {
      formData.append(`Pictures[${index}].Id`, String(picture.id));
    }
    if (picture.file) {
      formData.append(`Pictures[${index}].File`, picture.file);
    }
    formData.append(`Pictures[${index}].Order`, String(picture.order));
    formData.append(`Pictures[${index}].IsDeleted`, String(picture.isDeleted));
  });

  await api.put(`/review-comment/${id}`, formData);
}

export async function deleteReview(id: number) {
  await api.delete(`/review-comment/${id}`);
}
