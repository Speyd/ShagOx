import { api } from "@/shared/api/api";
import type { UpdateAdvertisementRequestDto } from "../model/types";

export async function updateAdvertisement(
  id: number,
  data: UpdateAdvertisementRequestDto,
) {
  const formData = new FormData();

  if (data.title) formData.append("Title", data.title);
  if (data.description) formData.append("Description", data.description);

  if (data.variants) {
    formData.append(
      "Variants",
      JSON.stringify(
        Object.fromEntries(
          Object.entries(data.variants).map(([variantId, variant]) => [
            variantId,
            { Price: variant.price, Stock: variant.stock },
          ]),
        ),
      ),
    );
  }

  data.pictures?.forEach((picture, index) => {
    if (picture.id != null) {
      formData.append(`Pictures[${index}].Id`, String(picture.id));
    }
    if (picture.file) {
      formData.append(`Pictures[${index}].File`, picture.file);
    }
    formData.append(`Pictures[${index}].Order`, String(picture.order));
    formData.append(`Pictures[${index}].IsDeleted`, String(picture.isDeleted));
  });

  return api.put(`/advertisements/${id}`, formData);
}
