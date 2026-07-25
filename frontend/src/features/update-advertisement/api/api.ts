import { api } from "@/shared/api/api";
import type { UpdateAdvertisementRequestDto } from "./types";

export async function updateAdvertisement(
  id: number,
  data: UpdateAdvertisementRequestDto,
) {
  const formData = new FormData();

  if (data.title) formData.append("title", data.title);

  if (data.description) formData.append("description", data.description);

  if (data.price != null) formData.append("price", data.price.toString());

  for (const [key, value] of Object.entries(data.properties ?? {})) {
    formData.append(`properties[${key}]`, value);
  }
  
  data.images.forEach((image, index) => {
    if (image.id != null) {
      formData.append(`images[${index}].id`, image.id.toString());
    }

    if (image.file) {
      formData.append(`images[${index}].file`, image.file);
    }

    formData.append(`images[${index}].order`, image.order.toString());

    formData.append(
      `images[${index}].isDeleted`,
      String(image.isDeleted ?? false),
    );
  });

  return api.put(`/advertisements/${id}`, formData);
}
