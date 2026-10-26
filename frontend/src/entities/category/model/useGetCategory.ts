import { useQuery } from "@tanstack/react-query";
import { getCategory } from "../api/api";

export function useGetCategory(categoryId?: number) {
  const isIdValid = categoryId !== undefined && !isNaN(categoryId);

  return useQuery({
    queryKey: ["category", categoryId],
    queryFn: () => getCategory(categoryId!),
    enabled: isIdValid,
  });
}
