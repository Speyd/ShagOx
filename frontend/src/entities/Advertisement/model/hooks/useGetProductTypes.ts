import { useQuery } from "@tanstack/react-query";
import { getProductTypes } from "../../api/api";

export function useGetProductTypes() {
  return useQuery({
    queryKey: ["product-types"],
    queryFn: () => getProductTypes(),
  });
}
