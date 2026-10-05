import { api } from "@/shared/api/api";
import type { Favorite, FavoriteCreateRequest } from "../model/types";
import type { PaginatedResponse } from "@/shared/lib/types/paginatedResponse";
import { getAdvertisement } from "@/entities/Advertisement/api/api";

export async function getFavorites(): Promise<PaginatedResponse<Favorite>> {
  const response = await api.get<PaginatedResponse<Favorite>>("/favorites/me");
  const items = await Promise.all(
    response.data.items.map(async (favorite) => ({
      ...favorite,
      advertisement: await getAdvertisement(favorite.advertisement.id),
    })),
  );

  return {
    ...response.data,
    items,
  };
}

export async function addToFavorites(request: FavoriteCreateRequest) {
  const response = await api.post("/favorites", request);

  return response.data;
}

export async function deleteFavorite(id: number) {
  await api.delete(`/favorites/${id}`);
}
