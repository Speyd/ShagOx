import { useQuery } from "@tanstack/react-query";
import { getSessions } from "../../api/api";

export function useGetSessions(params?: { page?: number; pageSize?: number }) {
  return useQuery({
    queryKey: ["sessions", params],
    queryFn: () => getSessions(params),
  });
}
