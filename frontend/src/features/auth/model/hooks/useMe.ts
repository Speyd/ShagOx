import { useQuery } from "@tanstack/react-query";
import type { User } from "../types";
import { me } from "../../api/api";

export function useMe() {
  return useQuery<User>({
    queryKey: ["me"],
    queryFn: me,
    refetchOnWindowFocus: false,
    staleTime: Infinity,
    retry: false,
  });
}
