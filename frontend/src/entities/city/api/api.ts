import { api } from "@/shared/api/api";
import type { City } from "@/shared/lib/types/city";
import type { PaginatedResponse } from "@/shared/lib/types/paginatedResponse";

export async function getAdminCities(
  page: number,
  pageSize: number,
): Promise<PaginatedResponse<City>> {
  const { data } = await api.get<PaginatedResponse<City>>("admin/cities", {
    params: {
      page,
      pageSize,
    },
  });
  return data;
}
