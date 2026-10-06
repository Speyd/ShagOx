import {
  debounce,
  parseAsBoolean,
  parseAsInteger,
  parseAsNativeArrayOf,
  parseAsString,
  parseAsStringEnum,
  useQueryStates,
} from "nuqs";
import { useCallback } from "react";

const catalogFilterParsers = {
  search: parseAsString,
  categoryIds: parseAsNativeArrayOf(parseAsInteger).withDefault([]),
  inStock: parseAsBoolean,
  memory: parseAsNativeArrayOf(parseAsString).withDefault([]),
  colors: parseAsNativeArrayOf(parseAsString).withDefault([]),
  minPrice: parseAsInteger,
  maxPrice: parseAsInteger,
  sortBy: parseAsString.withDefault("createdAt"),
  sortOrder: parseAsStringEnum(["asc", "desc"] as const).withDefault("desc"),
  page: parseAsInteger.withDefault(1),
};

type CatalogFilterValues = {
  search: string | null;
  categoryIds: number[];
  inStock: boolean | null;
  memory: string[];
  colors: string[];
  minPrice: number | null;
  maxPrice: number | null;
  sortBy: string;
  sortOrder: "asc" | "desc";
  page: number;
};

type CatalogFilterUpdates = Partial<
  Omit<CatalogFilterValues, "page">
> & {
  page?: number;
};

export function useCatalogFilters() {
  const [filters, setFilters] = useQueryStates(catalogFilterParsers, {
    urlKeys: {
      categoryIds: "category",
    },
    limitUrlUpdates: debounce(250),
  });

  const updateParams = useCallback(
    (newParams: CatalogFilterUpdates) => {
      void setFilters({
        ...newParams,
        page: "page" in newParams ? newParams.page : 1,
      });
    },
    [setFilters],
  );

  const clearFilters = useCallback(
    () => {
      void setFilters({
        categoryIds: null,
        inStock: null,
        memory: null,
        colors: null,
        minPrice: null,
        maxPrice: null,
        sortBy: null,
        sortOrder: null,
        page: null,
      });
    },
    [setFilters],
  );

  return { filters, updateParams, clearFilters };
}
