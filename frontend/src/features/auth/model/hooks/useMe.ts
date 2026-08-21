import { useQuery } from "@tanstack/react-query";
import { me } from "../../api/api";
import type { User } from "@/shared/lib/types/user";

export function useMe() {
  return useQuery<User>({
    queryKey: ["me"],
    queryFn: me,
    refetchOnWindowFocus: false,
    staleTime: Infinity,
    retry: false,
  });
}
