import { api } from "@/shared/api/api";
import type { Favorite, FavoriteCreateRequest } from "../model/types";
import type { PaginatedResponse } from "@/shared/lib/types/paginatedResponse";

export async function getFavorites(
  page = 1,
  pageSize = 12,
): Promise<PaginatedResponse<Favorite>> {
  const response = await api.get<PaginatedResponse<Favorite>>("/favorites/me", {
    params: { page, pageSize },
  });

  return response.data;
}

const ALL_FAVORITES_PAGE_SIZE = 100;

export async function getAllFavorites(): Promise<Favorite[]> {
  const first = await getFavorites(1, ALL_FAVORITES_PAGE_SIZE);
  const rest = await Promise.all(
    Array.from({ length: first.totalPages - 1 }, (_, index) =>
      getFavorites(index + 2, ALL_FAVORITES_PAGE_SIZE),
    ),
  );

  return [first, ...rest].flatMap((page) => page.items);
}

export async function addToFavorites(request: FavoriteCreateRequest) {
  const response = await api.post("/favorites", request);

  return response.data;
}

export async function deleteFavorite(id: number) {
  await api.delete(`/favorites/${id}`);
}
