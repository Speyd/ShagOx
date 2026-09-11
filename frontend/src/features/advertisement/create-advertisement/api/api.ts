import { api } from "@/shared/api/api";
import type { AdvertisementDto } from "@/features/advertisement/advertisement-form/model/types";

export type CreateAdvertisementDto = AdvertisementDto;

export async function createAdvertisement(data: AdvertisementDto) {
  const formData = new FormData();

  formData.append("title", data.title);
  formData.append("description", data.description);
  formData.append("price", data.price.toString());
  formData.append("currencyId", data.currencyId.toString());
  formData.append("sellerId", data.sellerId.toString());
  formData.append("categoryId", data.categoryId.toString());
  formData.append("conditionId", data.conditionId.toString());
  formData.append("stock", (data.stock ?? 1).toString());

  formData.append("popularity", "0");

  data.images.forEach((file: File) => {
    formData.append("images", file);
  });

  for (const [key, value] of Object.entries(data.properties ?? {})) {
    formData.append(`properties[${key}]`, String(value));
  }

  const response = await api.post("/advertisements", formData);

  return response.data;
}
