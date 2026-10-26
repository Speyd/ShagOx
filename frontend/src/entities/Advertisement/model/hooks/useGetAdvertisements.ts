import { useQuery } from "@tanstack/react-query";
import { getAdvertisements } from "../../api/api";

export function useGetAdvertisements(params?: {
  page?: number;
  pageSize?: number;
}) {
  return useQuery({
    queryKey: ["advertisements", params],
    queryFn: () => getAdvertisements(params),
  });
}
