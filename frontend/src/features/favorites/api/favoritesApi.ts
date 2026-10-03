import { api } from "@/shared/api/api";
import type { Favorite, FavoriteCreateRequest } from "../model/types";
import type { PaginatedResponse } from "@/shared/lib/types/paginatedResponse";

export async function getFavorites(): Promise<PaginatedResponse<Favorite>> {
  const response = await api.get<PaginatedResponse<Favorite>>("/favorites/me");
  return response.data;
}

export async function addToFavorites(request: FavoriteCreateRequest) {
  const response = await api.post("/favorites", request);

  return response.data;
}

export async function deleteFavorite(id: number) {
  await api.delete(`/favorites/${id}`);
}
