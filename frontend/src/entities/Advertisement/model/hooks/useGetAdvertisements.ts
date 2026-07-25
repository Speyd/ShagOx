import type { Advertisement } from "@/types/advertisements";
import { useQuery } from "@tanstack/react-query";
import { getAdvertisements } from "../../api/api";

export function useGetAdvertisements() {
  return useQuery<Advertisement[]>({
    queryKey: ["advertisements"],
    queryFn: getAdvertisements,
  });
}
