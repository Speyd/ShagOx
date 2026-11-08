import { useQuery } from "@tanstack/react-query";
import { getCategoryFacets } from "../../api/api";

export function useGetCategoryFacets(categoryId?: number) {
  return useQuery({
    queryKey: ["category-facets", categoryId],
    queryFn: () => getCategoryFacets(categoryId!),
    enabled: Boolean(categoryId),
    staleTime: 60_000,
  });
}
