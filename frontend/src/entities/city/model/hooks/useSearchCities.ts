import { keepPreviousData, useQuery } from "@tanstack/react-query";
import { useDebouncedValue } from "@mantine/hooks";
import { searchCities } from "../../api/api";

export function useSearchCities(query: string) {
  const [debouncedQuery] = useDebouncedValue(query.trim(), 350);
  const enabled = debouncedQuery.length >= 2;

  return useQuery({
    queryKey: ["cities", "search", debouncedQuery],
    queryFn: () => searchCities(debouncedQuery),
    enabled,
    placeholderData: enabled ? keepPreviousData : undefined,
    select: (res) => res.items,
  });
}
