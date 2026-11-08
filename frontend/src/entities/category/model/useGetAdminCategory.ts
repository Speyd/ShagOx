import { useQuery } from "@tanstack/react-query";
import { getAdminCategory } from "../api/api";
import { useGetAdminProductType } from "@/entities/product-type/model/hooks/useGetAdminProductType";

export function useGetAdminCategory(id: number) {
  const categoryQuery = useQuery({
    queryKey: ["admin-category", id],
    queryFn: () => getAdminCategory(id),
    enabled: Boolean(id),
  });

  const productTypeId = categoryQuery.data?.productTypeId;

  const productTypeQuery = useGetAdminProductType(productTypeId ?? 0);

  return {
    category: categoryQuery.data
      ? {
          ...categoryQuery.data,
          productType: productTypeQuery.data ?? null,
        }
      : null,
    isLoading:
      categoryQuery.isLoading ||
      (Boolean(productTypeId) && productTypeQuery.isLoading),
    isError: categoryQuery.isError || productTypeQuery.isError,
  };
}
