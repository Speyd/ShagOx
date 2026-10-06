import { keepPreviousData, useQuery } from "@tanstack/react-query";
import { getCatalogAdvertisements, type CatalogParams } from "../../api/api";

export function useGetCatalogAdvertisements(params: CatalogParams) {
  return useQuery({
    queryKey: ["catalog-advertisements", params],
    queryFn: () => getCatalogAdvertisements(params),
    placeholderData: keepPreviousData,
    staleTime: 30_000,
  });
}
