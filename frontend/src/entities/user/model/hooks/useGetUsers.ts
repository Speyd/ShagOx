import { useQuery } from "@tanstack/react-query";
import { getUsers } from "../../api/api";

export function useGetUsers(page: number, pageSize: number) {
  return useQuery({
    queryKey: ["users", page, pageSize],
    queryFn: () => getUsers(page, pageSize),
    placeholderData: (previousData) => previousData,
  });
}
