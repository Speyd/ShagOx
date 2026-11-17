import { api } from "@/shared/api/api";
import type { Currency } from "@/shared/lib/types/currency";
import type { PaginatedResponse } from "@/shared/lib/types/paginatedResponse";

export async function getCurrencies(): Promise<Currency[]> {
  const { data } = await api.get<PaginatedResponse<Currency>>("/currencies", {
    params: { page: 1, pageSize: 100 },
  });
  return data.items;
}
