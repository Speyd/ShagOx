import { useQuery } from "@tanstack/react-query";
import {
  getAttributeDictionaryValues,
  type CategoryAttributeValueResponse,
} from "../../api/api";

export function useGetAttributeDictionaryValues(categoryId?: number) {
  return useQuery<CategoryAttributeValueResponse[]>({
    queryKey: ["category-attribute-values", categoryId],
    queryFn: () => getAttributeDictionaryValues(categoryId),
    enabled: Boolean(categoryId),
    staleTime: 5 * 60 * 1000,
  });
}
