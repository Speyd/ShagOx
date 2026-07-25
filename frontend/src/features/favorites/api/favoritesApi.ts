import { api } from "@/shared/api/api";
import type { FavoriteCreateRequest } from "../model/types";

export async function getFavorites() {
  const response = await api.get("/favorite/me");
  return response.data;
}

export async function addToFavorites(request: FavoriteCreateRequest) {
  const response = await api.post("/favorite", request);

  return response.data;
}

export async function deleteFavorite(id: number) {
  await api.delete(`/favorite/${id}`);
}
