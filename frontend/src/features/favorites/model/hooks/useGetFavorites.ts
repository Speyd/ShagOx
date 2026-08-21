import { useQuery } from "@tanstack/react-query";
import { getFavorites } from "../../api/favoritesApi";
import { useAuthStore } from "@/features/auth/store/useAuthStore";

export function useGetFavorites() {
  const user = useAuthStore((state) => state.user);
  return useQuery({
    queryKey: ["favorites"],
    queryFn: getFavorites,
    enabled: !!user,
    retry: false,
  });
}
