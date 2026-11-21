import { api } from "@/shared/api/api";
import type { PaginatedResponse } from "@/shared/lib/types/paginatedResponse";
import type { ProductType } from "@/shared/lib/types/product-type";

export default function getAdminProductTypes(
  page: number,
  pageSize: number,
): Promise<PaginatedResponse<ProductType>> {
  return api.get("/admin/product-types", {
    params: {
      page,
      pageSize,
    },
  });
}

export async function getAdminProductType(
  productTypeId: number,
): Promise<ProductType> {
  const response = await api.get<ProductType>(
    `/product-types/${productTypeId}`,
  );
  return response.data;
}
