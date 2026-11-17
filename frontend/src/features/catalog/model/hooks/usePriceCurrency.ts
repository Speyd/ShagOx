import { useQuery } from "@tanstack/react-query";
import { getCatalogAdvertisements } from "@/entities/advertisement/api/api";

export type PriceCurrency = {
  symbol: string;
  coefficient: number;
};

const DEFAULT_CURRENCY: PriceCurrency = { symbol: "₴", coefficient: 1 };

export function usePriceCurrency(categoryId?: number): PriceCurrency {
  const { data } = useQuery({
    queryKey: ["catalog-price-currency", categoryId],
    queryFn: () => getCatalogAdvertisements({ categoryId, page: 1, pageSize: 1 }),
    enabled: categoryId !== undefined,
    staleTime: 5 * 60 * 1000,
    select: (res): PriceCurrency => {
      const conversion = res.items[0]?.priceConversion?.price;
      if (!conversion) return DEFAULT_CURRENCY;
      if (!conversion.to) {
        return { symbol: conversion.from.symbol, coefficient: 1 };
      }
      return { symbol: conversion.to.symbol, coefficient: conversion.coefficient };
    },
  });

  return data ?? DEFAULT_CURRENCY;
}
