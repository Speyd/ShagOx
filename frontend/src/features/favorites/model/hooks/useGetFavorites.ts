import { useQuery } from "@tanstack/react-query";
import { getFavorites } from "../../api/favoritesApi";
import type { Favorite } from "../types";
import { useAuthStore } from "@/features/auth/store/useAuthStore";

export function useGetFavorites() {
  const user = useAuthStore((state) => state.user);
  return useQuery<Favorite[]>({
    queryKey: ["favorites"],
    queryFn: getFavorites,
    enabled: !!user,
    retry: false,
  });
}
