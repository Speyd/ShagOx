import { api } from "../api";

type CreateImageDto = {
  image: File;
  advertisementId: number;
};

export async function createImage(data: CreateImageDto) {
  const formData = new FormData();

  formData.append("file", data.image);
  formData.append("advertisementId", data.advertisementId.toString());

  const response = await api.post("/api/images", formData);

  return response.data;
}

export async function deleteImage(id: number) {
  await api.delete(`/api/images/${id}`);
}
