import { useQuery } from "@tanstack/react-query";
import { getAdminCategory } from "../api/api";

export function useGetAdminCategory(id: number) {
  return useQuery({
    queryKey: ["admin-category", id],
    queryFn: () => getAdminCategory(id),
  });
}
