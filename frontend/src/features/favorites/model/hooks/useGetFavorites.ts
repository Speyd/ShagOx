import { useQuery } from "@tanstack/react-query";
import { getFavorites } from "../../api/favoritesApi";
import { useAuthStore } from "@/features/auth";

export function useGetFavorites(page = 1, pageSize = 12) {
  const user = useAuthStore((state) => state.user);

  return useQuery({
    queryKey: ["favorites", user?.id, page, pageSize],
    queryFn: () => getFavorites(page, pageSize),
    enabled: !!user?.id,
  });
}
