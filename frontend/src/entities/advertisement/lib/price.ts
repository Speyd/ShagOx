import type { CurrencyConvert } from "@/shared/lib/types/advertisements";

export type DisplayPrice = {
  amount: number;
  symbol: string | undefined;
};

const roundMoney = (value: number) => Math.round(value * 100) / 100;

export function toDisplayPrice(
  amount: number,
  conversion: CurrencyConvert | null | undefined,
  fallbackSymbol?: string,
): DisplayPrice {
  if (!conversion?.to) {
    return { amount, symbol: conversion?.from.symbol ?? fallbackSymbol };
  }

  return {
    amount: roundMoney(amount * conversion.coefficient),
    symbol: conversion.to.symbol,
  };
}
