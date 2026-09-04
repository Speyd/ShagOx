import { useQuery } from "@tanstack/react-query";
import { getAdminAdvertisements } from "../../api/api";

export function useGetAdminAdvertisements(page: number, pageSize: number) {
  return useQuery({
    queryKey: ["advertisements", page, pageSize],
    queryFn: () => getAdminAdvertisements(page, pageSize),
    placeholderData: (previousData) => previousData,
  });
}
