import { useQuery } from "@tanstack/react-query";
import { getCategories } from "../../api/api";

export function useGetCategories(productTypeId?: number) {
  return useQuery({
    queryKey: ["categories", productTypeId],
    queryFn: () => getCategories(productTypeId),
    enabled: productTypeId === undefined || !isNaN(productTypeId),
    staleTime: 1000 * 60 * 5,
    retry: false,
  });
}
