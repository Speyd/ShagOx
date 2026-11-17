import { useGetBasket } from "./useGetBasket";

export function useBasketItemsCount() {
  const { data: basket } = useGetBasket();
  return basket?.basketItems.length ?? 0;
}
