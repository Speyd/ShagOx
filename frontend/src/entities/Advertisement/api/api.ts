import { api } from "@/shared/api/api";
import type { Advertisement } from "@/shared/lib/types/advertisements";
import type { PaginatedResponse } from "@/shared/lib/types/paginatedResponse";

export async function getAdvertisements(): Promise<
  PaginatedResponse<Advertisement>
> {
  const response =
    await api.get<PaginatedResponse<Advertisement>>("/advertisements");

  return response.data;
}

export async function getAdminAdvertisements(
  page: number,
  pageSize: number,
): Promise<PaginatedResponse<Advertisement>> {
  const { data } = await api.get<PaginatedResponse<Advertisement>>(
    "/advertisements",
    {
      params: {
        page,
        pageSize,
      },
    },
  );

  return data;
}

export async function getAdvertisement(id: number) {
  const response = await api.get(`/advertisements/${id}`);
  return response.data;
}
