import { useQuery } from "@tanstack/react-query";
import { getAdvertisements } from "../../api/api";

export function useGetAdvertisements() {
  return useQuery({
    queryKey: ["advertisements"],
    queryFn: () => getAdvertisements(),
  });
}
