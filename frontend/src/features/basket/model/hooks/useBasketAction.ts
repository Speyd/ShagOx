import type { MouseEvent } from "react";
import { toast } from "sonner";
import { useAuthStore } from "@/features/auth";
import { createBasket } from "../../api/api";
import { useCreateBasketItem } from "./useCreateBasketItem";
import { useGetBasket } from "./useGetBasket";

export function useBasketAction(variantId?: number) {
  const { data: basket } = useGetBasket();
  const addToBasket = useCreateBasketItem();
  const user = useAuthStore((state) => state.user);

  const isInBasket = Boolean(
    variantId &&
      basket?.basketItems.some((item) =>
        item.advertisement.variants.some((variant) => variant.id === variantId),
      ),
  );

  const handleAdd = async (event: MouseEvent<HTMLButtonElement>) => {
    event.preventDefault();
    event.stopPropagation();

    if (!user) {
      toast.error("Увійдіть, щоб додати товар до кошика");
      return;
    }

    if (!variantId) {
      toast.error("Варіант товару не знайдено");
      return;
    }

    if (isInBasket) {
      return;
    }

    let basketId: number;
    try {
      basketId = basket?.id ?? (await createBasket()).id;
    } catch {
      toast.error("Не вдалося створити кошик");
      return;
    }

    try {
      await addToBasket.mutateAsync({
        basketId,
        quantity: 1,
        advertisementVariantId: variantId,
      });
    } catch {
      return;
    }
  };

  return {
    isInBasket,
    isLoading: addToBasket.isPending,
    handleAdd,
  };
}
