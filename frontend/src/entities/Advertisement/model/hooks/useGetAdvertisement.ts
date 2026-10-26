import { useQuery } from "@tanstack/react-query";
import { getAdvertisement } from "../../api/api";

export function useGetAdvertisement(id?: number) {
  const isIdValid = id !== undefined && !isNaN(id);

  return useQuery({
    queryKey: ["advertisement", id],
    queryFn: () => getAdvertisement(id!),
    enabled: isIdValid,
  });
}
