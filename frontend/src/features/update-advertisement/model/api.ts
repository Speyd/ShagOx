import { api } from "@/shared/api/api";
import type { UpdateAdvertisementDto } from "./types";

export async function updateAdvertisement(
  id: number,
  data: UpdateAdvertisementDto,
) {
  const response = await api.patch(`/api/advertisements/${id}`, data);

  return response.data;
}
