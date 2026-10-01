import { useQuery } from "@tanstack/react-query";
import { useAuthStore } from "@/features/auth";
import { getBasket } from "../../api/api";

export const basketQueryKey = ["basket"] as const;

export function useGetBasket(options?: { enabled?: boolean }) {
  const user = useAuthStore((state) => state.user);

  return useQuery({
    queryKey: basketQueryKey,
    queryFn: getBasket,
    enabled: !!user && (options?.enabled ?? true),
    retry: false,
    staleTime: 1000 * 60 * 5,
  });
}
