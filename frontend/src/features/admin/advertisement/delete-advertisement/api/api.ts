import { api } from "@/shared/api/api";

export async function adminDeleteAdvertisement(id: number) {
  const response = await api.delete(`/admin/advertisements/${id}`);
  return response.data;
}
