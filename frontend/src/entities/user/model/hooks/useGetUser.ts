import { useQuery } from "@tanstack/react-query";
import { getUser } from "../../api/api";
import type { User } from "@/shared/lib/types/user";

export function useGetUser(id: number) {
  return useQuery<User>({
    queryKey: ["user", id],
    queryFn: () => getUser(id),
  });
}
