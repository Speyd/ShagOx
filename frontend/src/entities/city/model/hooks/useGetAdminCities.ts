import { useQuery } from "@tanstack/react-query";
import { getAdminCities } from "../../api/api";

export function useGetAdminCities(page: number, pageSize: number) {
  return useQuery({
    queryKey: ["cities", page, pageSize],
    queryFn: () => getAdminCities(page, pageSize),
    placeholderData: (previousData) => previousData,
  });
}
