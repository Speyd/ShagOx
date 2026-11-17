import { useMemo, useState } from "react";
import { useDebouncedValue } from "@mantine/hooks";
import { useGetAttributeDictionaryValues } from "@/entities/advertisement/model/hooks/useGetAttributeDictionaryValues";
import { useGetCategoryFacets } from "@/entities/advertisement/model/hooks/useGetCategoryFacets";
import {
  buildFilterGroups,
  countActiveFilters,
  EMPTY_FILTERS,
  toSearchParams,
  type FilterState,
} from "../filterGroups";
import { usePriceCurrency } from "./usePriceCurrency";

export function useCatalogFilters(categoryId?: number) {
  const { data: attributes, isLoading: isLoadingAttributes } =
    useGetAttributeDictionaryValues(categoryId);
  const { data: facets, isLoading: isLoadingFacets } = useGetCategoryFacets(categoryId);

  const [stored, setStored] = useState({ categoryId, value: EMPTY_FILTERS });
  const state = stored.categoryId === categoryId ? stored.value : EMPTY_FILTERS;
  const [debouncedState] = useDebouncedValue(state, 400);

  const groups = useMemo(
    () => buildFilterGroups(attributes ?? [], facets?.attributes ?? []),
    [attributes, facets],
  );

  const priceCurrency = usePriceCurrency(categoryId);
  const { coefficient } = priceCurrency;

  const priceBounds = useMemo<[number, number] | null>(() => {
    if (facets?.minPrice == null || facets.maxPrice == null) return null;
    if (facets.minPrice === facets.maxPrice) return null;
    return [
      Math.floor(facets.minPrice * coefficient),
      Math.ceil(facets.maxPrice * coefficient),
    ];
  }, [facets, coefficient]);

  const update = (change: (current: FilterState) => FilterState) =>
    setStored({ categoryId, value: change(state) });

  const setOptions = (key: string, values: string[]) =>
    update((s) => {
      const options = { ...s.options, [key]: values };
      if (values.length === 0) delete options[key];
      return { ...s, options };
    });

  const setRange = (key: string, range: [number, number] | null) =>
    update((s) => {
      const ranges = { ...s.ranges };
      if (range) ranges[key] = range;
      else delete ranges[key];
      return { ...s, ranges };
    });

  const setPrice = (price: [number, number] | null) => update((s) => ({ ...s, price }));

  const reset = () => update(() => EMPTY_FILTERS);

  const searchParams = useMemo(() => {
    const params = toSearchParams(debouncedState, groups);
    if (!debouncedState.price) return params;

    const [from, to] = debouncedState.price;
    return {
      ...params,
      minPrice: Math.floor(from / coefficient),
      maxPrice: Math.ceil(to / coefficient),
    };
  }, [debouncedState, groups, coefficient]);

  return {
    groups,
    priceBounds,
    priceCurrency,
    state,
    setOptions,
    setRange,
    setPrice,
    reset,
    activeCount: countActiveFilters(state),
    searchParams,
    isLoading: isLoadingAttributes || isLoadingFacets,
  };
}

export type CatalogFilters = ReturnType<typeof useCatalogFilters>;
