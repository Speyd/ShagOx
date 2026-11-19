import { useQuery } from "@tanstack/react-query";
import { getCatalogShowcase, type CatalogSort } from "../../api/api";

export function useGetCatalogShowcase(sort: CatalogSort, pageSize = 12) {
  return useQuery({
    queryKey: ["catalog-showcase", sort, pageSize],
    queryFn: () => getCatalogShowcase(sort, pageSize),
    staleTime: 5 * 60 * 1000,
  });
}
