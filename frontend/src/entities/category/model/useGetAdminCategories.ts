import { useQuery } from "@tanstack/react-query";
import { getAdminCategories } from "../api/api";

export function useGetAdminCategories(page: number, pageSize: number) {
  return useQuery({
    queryKey: ["categories", page, pageSize],
    queryFn: () => getAdminCategories(page, pageSize),
    placeholderData: (previousData) => previousData,
  });
}
