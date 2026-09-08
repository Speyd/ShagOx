import { api } from "@/shared/api/api";
import type { Category } from "@/shared/lib/types/category";
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

export async function getAdminCategory(id: number): Promise<Category> {
  const response = await api.get<Category>(`/categories/${id}`);
  return response.data;
}
