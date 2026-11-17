import { useQuery } from "@tanstack/react-query";
import { getCurrencies } from "../../api/api";

export function useGetCurrencies() {
  return useQuery({
    queryKey: ["currencies"],
    queryFn: getCurrencies,
  });
}
