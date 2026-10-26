import { useQuery } from "@tanstack/react-query";
import { getCategoryAttributeFilterOptions } from "../../api/api";

export function useGetCategoryAttributeFilterOptions(categoryId?: number) {
  return useQuery({
    queryKey: ["category-attribute-filter-options", categoryId],
    queryFn: () => getCategoryAttributeFilterOptions(categoryId!),
    enabled: Boolean(categoryId) && categoryId! > 0,
  });
}
