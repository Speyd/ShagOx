import { api } from "@/shared/api/api";
import type { CreateAdvertisementDto } from "./types";

export async function createAdvertisement(data: CreateAdvertisementDto) {
  const formData = new FormData();

  formData.append("title", data.title);
  formData.append("description", data.description);
  formData.append("price", data.price.toString());
  formData.append("currencyId", data.currencyId.toString());
  formData.append("sellerId", data.sellerId.toString());
  formData.append("categoryId", data.categoryId.toString());
  formData.append("conditionId", data.conditionId.toString());

  formData.append("popularity", "0");

  data.images.forEach((file) => {
    formData.append("images", file);
  });

  for (const [key, value] of Object.entries(data.properties)) {
    formData.append(`properties[${key}]`, value);
  }

  const response = await api.post("/api/advertisements", formData);

  return response.data;
}
