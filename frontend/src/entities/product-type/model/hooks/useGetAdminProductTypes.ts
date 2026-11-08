import { keepPreviousData, useQuery } from "@tanstack/react-query";
import getAdminProductTypes from "../../api/api";

export function useGetAdminProductTypes(page: number, pageSize: number) {
  return useQuery({
    queryKey: ["adminProductTypes", page, pageSize],
    queryFn: () => getAdminProductTypes(page, pageSize),
    placeholderData: keepPreviousData,
  });
}
