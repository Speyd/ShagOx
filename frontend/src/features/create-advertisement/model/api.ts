import { api } from "@/shared/api/api";
import type { CreateAdvertisementDto } from "./types";

export async function createAdvertisement(data: CreateAdvertisementDto) {
  const response = await api.post(`/api/advertisements`, data);
  return response.data;
}
