import { keepPreviousData, useQuery } from "@tanstack/react-query";
import { getCatalogAdvertisements } from "../../api/api";
import type { CatalogParams } from "../types";

export function useGetCatalogAdvertisements(params: CatalogParams) {
  return useQuery({
    queryKey: ["catalog-advertisements", params],
    queryFn: () => getCatalogAdvertisements(params),
    placeholderData: keepPreviousData,
    staleTime: 30_000,
  });
}
