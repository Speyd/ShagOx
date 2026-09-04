import type { Advertisement } from "@/shared/lib/types/advertisements";
import { useQuery } from "@tanstack/react-query";
import { getAdvertisement } from "../../api/api";

export function useGetAdvertisement(id: number) {
  return useQuery<Advertisement>({
    queryKey: ["advertisement", id],
    queryFn: () => getAdvertisement(id),
  });
}
