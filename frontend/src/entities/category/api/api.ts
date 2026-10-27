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

export async function getCategory(categoryId: number): Promise<Category> {
  const response = await api.get<Category>("/category", {
    params: {
      categoryId,
    },
  });
  return response.data;
}

export async function getCategoryByCode(
  code: string,
): Promise<Category | null> {
  const response = await api.get<PaginatedResponse<Category>>(
    "/categories/search",
    {
      params: {
        code: code,
        page: 1,
        pageSize: 1,
      },
    },
  );

  return response.data.items[0] ?? null;
}
