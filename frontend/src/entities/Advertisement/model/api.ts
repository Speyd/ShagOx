import { api } from "@/shared/api/api";

export async function getAdvertisements() {
  const response = await api.get(`/api/advertisements`);
  return response.data;
}

export async function getAdvertisement(id: number) {
  const response = await api.get(`/api/advertisements/${id}`);
  return response.data;
}
