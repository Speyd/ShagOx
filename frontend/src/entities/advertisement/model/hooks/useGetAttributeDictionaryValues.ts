import { useQuery } from "@tanstack/react-query";
import { getAttributeDictionaryValues } from "../../api/api";

export function useGetAttributeDictionaryValues(categoryId?: number) {
  return useQuery({
    queryKey: ["category-attribute-values", categoryId],
    queryFn: () => getAttributeDictionaryValues(categoryId),
    enabled: Boolean(categoryId),
    staleTime: 5 * 60 * 1000,
  });
}
