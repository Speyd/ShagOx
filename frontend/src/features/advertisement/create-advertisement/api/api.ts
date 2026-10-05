import { api } from "@/shared/api/api";
import type { AdvertisementDto } from "@/features/advertisement/advertisement-form/model/types";

export type CreateAdvertisementDto = AdvertisementDto;

export async function createAdvertisement(data: AdvertisementDto) {
  const formData = new FormData();

  formData.append("title", data.title);
  formData.append("description", data.description);
  formData.append("popularity", data.popularity.toString());
  formData.append("currencyId", data.currencyId.toString());
  formData.append("categoryId", data.categoryId.toString());
  formData.append("conditionId", data.conditionId.toString());
  formData.append("attributes", JSON.stringify(data.attributes ?? {}));
  formData.append("variants", JSON.stringify(data.variants ?? []));

  data.images.forEach((file: File) => {
    formData.append("images", file);
  });

  const response = await api.post("/advertisements", formData);

  return response.data;
}
