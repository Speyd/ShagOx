import { useMemo } from "react";
import { useQuery } from "@tanstack/react-query";
import { getConditions, getCurrencies, getStatuses } from "../../api/api";
import type { LookupDto, SelectOption } from "../types";

const STALE_TIME = 10 * 60 * 1000;
const DEFAULT_CURRENCY_CODE = "UAH";
// Порядок важливий: каталог використовує "published"
const PUBLISH_STATUS_CODES = ["published", "active"];

const toOptions = (items: LookupDto[] = []): SelectOption[] =>
  items.map((item) => ({ value: String(item.id), label: item.label || item.code }));

/** Довідники, потрібні для створення оголошення: стани, валюти, статуси. */
export function useFormLookups() {
  const conditions = useQuery({
    queryKey: ["conditions"],
    queryFn: getConditions,
    staleTime: STALE_TIME,
  });
  const currencies = useQuery({
    queryKey: ["currencies"],
    queryFn: getCurrencies,
    staleTime: STALE_TIME,
  });
  const statuses = useQuery({
    queryKey: ["statuses"],
    queryFn: getStatuses,
    staleTime: STALE_TIME,
  });

  return useMemo(() => {
    const currencyList = currencies.data ?? [];
    const statusList = statuses.data ?? [];

    const defaultCurrency =
      currencyList.find((c) => c.code === DEFAULT_CURRENCY_CODE) ??
      currencyList[0];

    const publishStatus =
      PUBLISH_STATUS_CODES.map((code) =>
        statusList.find((s) => s.code.toLowerCase() === code),
      ).find(Boolean) ?? statusList[0];

    return {
      conditionOptions: toOptions(conditions.data),
      currencyOptions: currencyList.map((c) => ({
        value: String(c.id),
        label: c.code,
      })),
      currencySymbols: Object.fromEntries(
        currencyList.map((c) => [String(c.id), c.symbol]),
      ),
      defaultCurrencyId: defaultCurrency ? String(defaultCurrency.id) : null,
      publishStatusId: publishStatus?.id ?? null,
    };
  }, [conditions.data, currencies.data, statuses.data]);
}
