import { api } from "./api";
import type { PaginatedResponse } from "@/shared/lib/types/paginatedResponse";

export type EntityStatus =
  | "Active"
  | "Hidden"
  | "Deleted"
  | "ActiveAndHidden"
  | "All";

export type BaseEntity = {
  id: number;
  entityStatus?: EntityStatus;
};

export type ListParams = {
  page?: number;
  pageSize?: number;
  status?: EntityStatus;
  filters?: Record<string, string | number | boolean | undefined | null>;
};

function cleanFilters(filters: ListParams["filters"] = {}) {
  return Object.fromEntries(
    Object.entries(filters).filter(
      ([, value]) => value !== undefined && value !== null && value !== "",
    ),
  );
}

export async function fetchPage<T>(
  path: string,
  { page = 1, pageSize = 20, status = "Active", filters }: ListParams = {},
): Promise<PaginatedResponse<T>> {
  const activeFilters = cleanFilters(filters);
  const hasFilters = Object.keys(activeFilters).length > 0;

  const { data } = await api.get<PaginatedResponse<T>>(
    hasFilters ? `${path}/search` : path,
    { params: { page, pageSize, status, ...activeFilters } },
  );

  return data;
}

export async function fetchAll<T>(
  path: string,
  params: Omit<ListParams, "page" | "pageSize"> = {},
  pageSize = 1000,
): Promise<T[]> {
  const result = await fetchPage<T>(path, { ...params, page: 1, pageSize });
  return result.items;
}

export async function fetchById<T>(path: string, id: number) {
  // Бекенд інвалідує кеш саме для статусу Active, тож читаємо з ним,
  // щоб після редагування отримувати свіжі дані.
  const { data } = await api.get<T>(`${path}/${id}`, {
    params: { status: "Active" },
  });
  return data;
}

export type CreateResult = { id: number };

export async function createEntity<TBody>(path: string, body: TBody) {
  const { data } = await api.post<CreateResult>(path, body);
  return data;
}

export async function updateEntity<TBody>(path: string, id: number, body: TBody) {
  const { data } = await api.put(`${path}/${id}`, body);
  return data;
}

/**
 * М'яке видалення (EntityStatus = Deleted): запис зникає з адмінки та сайту,
 * але не ламає пов'язані дані (замовлення, оголошення), які посилаються на нього
 * через зовнішні ключі з Restrict. Унікальні індекси в БД враховують лише активні
 * записи, тож код/назву можна використати повторно.
 */
export async function deleteEntity(path: string, id: number) {
  await api.patch(`${path}/${id}/status`, {
    changeStatus: "Deleted" satisfies EntityStatus,
    findStatus: "Active" satisfies EntityStatus,
  });
}
