import { useQueries } from "@tanstack/react-query";
import { getAttributeDictionaryValues } from "../../api/api";

export function useGetAttributeDictionaryValues(dictionaryIds: number[]) {
  const uniqueDictionaryIds = [...new Set(dictionaryIds)].sort((a, b) => a - b);

  return useQueries({
    queries: uniqueDictionaryIds.map((dictionaryId) => ({
      queryKey: ["attribute-dictionary-values", dictionaryId],
      queryFn: () => getAttributeDictionaryValues(dictionaryId),
      enabled: Number.isInteger(dictionaryId) && dictionaryId > 0,
      staleTime: 5 * 60_000,
    })),
  });
}
