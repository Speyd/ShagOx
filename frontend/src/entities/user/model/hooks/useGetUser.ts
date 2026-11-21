import { useQuery } from "@tanstack/react-query";
import { getUser } from "../../api/api";
import type { User } from "@/shared/lib/types/user";

export function useGetUser(id: number, enabled = true) {
  return useQuery<User>({
    queryKey: ["user", id],
    queryFn: () => getUser(id),
    enabled: enabled && id > 0,
    staleTime: 1000 * 60 * 5,
  });
}
