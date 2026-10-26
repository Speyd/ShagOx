import { useQuery } from "@tanstack/react-query";
import { getAttributeDefinitions } from "../../api/api";

export function useGetAttributeDefinitions(categoryId?: number) {
  return useQuery({
    queryKey: ["attribute-definitions", categoryId],
    queryFn: () => getAttributeDefinitions(categoryId!),
    enabled: Boolean(categoryId) && categoryId! > 0,
  });
}
