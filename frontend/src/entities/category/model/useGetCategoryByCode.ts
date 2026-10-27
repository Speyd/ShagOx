import { useQuery } from "@tanstack/react-query";
import { getCategoryByCode } from "../api/api";

export function useGetCategoryByCode(code?: string) {
  return useQuery({
    queryKey: ["category", "code", code],
    queryFn: () => getCategoryByCode(code!),
    enabled: Boolean(code),
  });
}
