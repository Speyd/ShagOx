import { useQuery } from "@tanstack/react-query";
import type { User } from "../model/types";
import { me } from "../api/me";

export function useMe() {
  return useQuery<User>({
    queryKey: ["me"],
    queryFn: me,
    retry: false,
  });
}
