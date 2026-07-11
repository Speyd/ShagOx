import { api } from "@/shared/api/api";
import type { UpdateAdvertisementDto } from "./types";

export async function updateAdvertisement(
  id: number,
  data: UpdateAdvertisementDto,
) {
  const formData = new FormData();

  if (data.title) formData.append("title", data.title);

  if (data.description) formData.append("description", data.description);

  if (data.price != null) formData.append("price", data.price.toString());

  data.newImages?.forEach((file) => {
    formData.append("newImages", file);
  });

  data.deletedImageIds?.forEach((id) => {
    formData.append("deletedImageIds", id.toString());
  });

  return api.put(`/api/advertisements/${id}`, formData);
}

export async function updateImagesOrder(
  advertisementId: number,
  imageIds: number[],
) {
  return api.put(`/api/advertisements/${advertisementId}/images/order`, {
    imageIds,
  });
}
