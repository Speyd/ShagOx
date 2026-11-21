import { useQueries } from "@tanstack/react-query";
import { getAdvertisement } from "@/entities/advertisement/api/api";
import type { Advertisement } from "@/shared/lib/types/advertisements";

/** Актуальні дані товарів для порівняння (кеш спільний зі сторінкою товару). */
export function useCompareAdvertisements(ids: number[]) {
  return useQueries({
    queries: ids.map((id) => ({
      queryKey: ["advertisement", id],
      queryFn: () => getAdvertisement(id),
      staleTime: 60_000,
    })),
    combine: (results) => ({
      items: results
        .map((r) => r.data)
        .filter((a): a is Advertisement => Boolean(a)),
      isLoading: results.some((r) => r.isLoading),
    }),
  });
}
