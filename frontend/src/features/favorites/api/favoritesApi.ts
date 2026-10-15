import { api } from "@/shared/api/api";
import type { Favorite, FavoriteCreateRequest } from "../model/types";
import type { PaginatedResponse } from "@/shared/lib/types/paginatedResponse";
import { getAdvertisement } from "@/entities/Advertisement/api/api";

export async function getFavorites(): Promise<PaginatedResponse<Favorite>> {
  const pageSize = 100;
  const firstPage = await api.get<PaginatedResponse<Favorite>>("/favorites/me", {
    params: { page: 1, pageSize },
  });
  const pages = [firstPage.data];

  for (let page = 2; page <= firstPage.data.totalPages; page += 1) {
    const response = await api.get<PaginatedResponse<Favorite>>(
      "/favorites/me",
      { params: { page, pageSize } },
    );
    pages.push(response.data);
  }

  const favorites = pages.flatMap((page) => page.items);
  const items = await Promise.all(
    favorites.map(async (favorite) => ({
      ...favorite,
      advertisement: await getAdvertisement(favorite.advertisement.id),
    })),
  );

  return {
    ...firstPage.data,
    items,
    page: 1,
    pageSize: items.length,
    totalPages: items.length === 0 ? 0 : 1,
  };
}

export async function addToFavorites(request: FavoriteCreateRequest) {
  const response = await api.post("/favorites", request);

  return response.data;
}

export async function deleteFavorite(id: number) {
  await api.delete(`/favorites/${id}`);
}