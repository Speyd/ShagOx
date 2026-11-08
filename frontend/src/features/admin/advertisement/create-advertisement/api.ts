import { api } from "@/shared/api/api";
import type { CreateResult } from "@/shared/api/crud";
import { buildAdvertisementFormData } from "@/features/advertisement/create-advertisement/api/api";
import type { CreateAdvertisementPayload } from "@/features/advertisement/create-advertisement";

export async function adminCreateAdvertisement(
  sellerId: number,
  payload: CreateAdvertisementPayload,
) {
  const { data } = await api.post<CreateResult>(
    `/admin/advertisements/${sellerId}`,
    buildAdvertisementFormData(payload),
  );
  return data;
}
