import { api } from "@/shared/api/api";
import type { UpdateAdvertisementRequestDto } from "../model/types";

export async function updateAdvertisement(
  id: number,
  data: UpdateAdvertisementRequestDto,
) {
  const formData = new FormData();

  if (data.title) formData.append("title", data.title);

  if (data.description) formData.append("description", data.description);

  if (data.popularity != null)
    formData.append("popularity", data.popularity.toString());
  if (data.currencyId != null)
    formData.append("currencyId", data.currencyId.toString());
  if (data.conditionId != null)
    formData.append("conditionId", data.conditionId.toString());
  if (data.categoryId != null)
    formData.append("categoryId", data.categoryId.toString());
  if (data.buyerId != null) formData.append("buyerId", data.buyerId.toString());
  if (data.attributes != null)
    formData.append("attributes", JSON.stringify(data.attributes));
  if (data.variants != null)
    formData.append("variants", JSON.stringify(data.variants));

  data.images?.forEach((image, index) => {
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
