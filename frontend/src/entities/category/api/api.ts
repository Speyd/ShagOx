import { api } from "@/shared/api/api";
import type { Category } from "@/shared/lib/types/advertisements";
import type { PaginatedResponse } from "@/shared/lib/types/paginatedResponse";

export async function getAdminCategories(
  page: number,
  pageSize: number,
): Promise<PaginatedResponse<Category>> {
  const response = await api.get<PaginatedResponse<Category>>(
    "/admin/categories",
    {
      params: {
        page,
        pageSize,
      },
    },
  );
  return response.data;
}
