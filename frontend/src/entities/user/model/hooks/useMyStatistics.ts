import { useQuery } from "@tanstack/react-query";
import { getMyStatistics } from "../../api/api";

export function useMyStatistics() {
  return useQuery({
    queryKey: ["users", "me", "statistics"],
    queryFn: getMyStatistics,
  });
}
