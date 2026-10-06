import { useQuery } from "@tanstack/react-query";
import { useAuthStore } from "@/features/auth";
import { getBasket } from "../../api/api";

export function useGetBasket(options?: { enabled?: boolean }) {
  const user = useAuthStore((state) => state.user);

  return useQuery({
    queryKey: ["basket", user?.id],
    queryFn: getBasket,
    enabled: !!user && (options?.enabled ?? true),
    retry: false,
    staleTime: 1000 * 30,
  });
}
