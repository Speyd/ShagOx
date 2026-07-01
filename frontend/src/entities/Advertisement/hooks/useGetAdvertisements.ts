import { useQuery } from "@tanstack/react-query";
import { getAdvertisements } from "../model/api";
import type { Advertisement } from "../model/types";

export function useGetAdvertisements() {
  return useQuery<Advertisement[]>({
    queryKey: ["advertisements"],
    queryFn: getAdvertisements,
  });
}
