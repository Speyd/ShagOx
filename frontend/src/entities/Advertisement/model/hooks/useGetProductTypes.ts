import { useQuery } from "@tanstack/react-query";
import { getProductTypes } from "../../api/api";

export function useGetProductTypes() {
  return useQuery({
    queryKey: ["product-types"],
    queryFn: getProductTypes,
    staleTime: 1000 * 60 * 10,
    retry: false,
  });
}