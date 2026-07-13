import { api } from "@/shared/api/api";

export async function deleteAdvertisement(id: number) {
  const response = await api.delete(`/api/advertisements/${id}`);
  return response.data;
}
