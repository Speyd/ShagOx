import { useQuery } from "@tanstack/react-query";
import { getAllFavorites, getFavorites } from "../../api/favoritesApi";
import { useAuthStore } from "@/features/auth";

const FAVORITES_STALE_TIME = 5 * 60 * 1000;

export function useGetFavorites(page = 1, pageSize = 12) {
  const user = useAuthStore((state) => state.user);

  return useQuery({
    queryKey: ["favorites", user?.id, page, pageSize],
    queryFn: () => getFavorites(page, pageSize),
    enabled: !!user?.id,
    staleTime: FAVORITES_STALE_TIME,
  });
}

export function useFavoriteIds() {
  const user = useAuthStore((state) => state.user);

  return useQuery({
    queryKey: ["favorites", user?.id, "all"],
    queryFn: getAllFavorites,
    enabled: !!user?.id,
    staleTime: FAVORITES_STALE_TIME,
    select: (favorites) =>
      new Map(
        favorites.map((favorite) => [favorite.advertisement.id, favorite.id]),
      ),
  });
}
