import { keepPreviousData, useQuery } from "@tanstack/react-query";
import { getSellerAdvertisements } from "../../api/api";

export const SELLER_ADVERTISEMENTS_KEY = "seller-advertisements";

export function useGetSellerAdvertisements(
  sellerId: number | undefined,
  page: number,
  pageSize = 10,
) {
  return useQuery({
    queryKey: [SELLER_ADVERTISEMENTS_KEY, sellerId, page, pageSize],
    queryFn: () => getSellerAdvertisements(sellerId!, page, pageSize),
    enabled: sellerId != null,
    placeholderData: keepPreviousData,
  });
}
