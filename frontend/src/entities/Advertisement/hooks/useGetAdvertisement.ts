import { useQuery } from "@tanstack/react-query";
import type { Advertisement } from "../model/types";
import { getAdvertisement } from "../model/api";

export function useGetAdvertisement(id: number) {
  return useQuery<Advertisement>({
    queryKey: ["advertisement", id],
    queryFn: () => getAdvertisement(id),
  });
}
