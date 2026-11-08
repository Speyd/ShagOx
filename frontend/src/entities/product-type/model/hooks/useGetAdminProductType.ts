import { useQuery } from "@tanstack/react-query";
import { getAdminProductType } from "../../api/api";

export function useGetAdminProductType(productTypeId: number) {
  return useQuery({
    queryKey: ["adminProductType", productTypeId],
    queryFn: () => getAdminProductType(productTypeId),
    enabled: Boolean(productTypeId),
  });
}
