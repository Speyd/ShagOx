import { api } from "@/shared/api/api";
import type { CreateAdvertisementPayload } from "../model/types";

export type CreateAdvertisementResponse = {
  id: number;
  createdAt: string;
};

export async function createAdvertisement(
  data: CreateAdvertisementPayload,
): Promise<CreateAdvertisementResponse> {
  const formData = new FormData();

  formData.append("title", data.title.trim());
  formData.append("description", data.description.trim());
  formData.append("popularity", (data.popularity ?? 0).toString());
  formData.append("currencyId", (data.currencyId ?? 1).toString());
  formData.append("categoryId", data.categoryId.toString());
  formData.append("conditionId", data.conditionId.toString());
  formData.append("statusId", (data.statusId ?? 0).toString());

  // JSON attributes
  formData.append("attributes", JSON.stringify(data.attributes ?? {}));
  formData.append("variants", JSON.stringify(data.variants ?? []));

  // Append images both as "pictures" (C# model binding) and "images" (fallback)
  data.pictures.forEach((file: File) => {
    formData.append("pictures", file);
    formData.append("images", file);
  });

  const response = await api.post<CreateAdvertisementResponse>(
    "/advertisements",
    formData,
    {
      headers: {
        "Content-Type": "multipart/form-data",
      },
    },
  );

  return response.data;
}
