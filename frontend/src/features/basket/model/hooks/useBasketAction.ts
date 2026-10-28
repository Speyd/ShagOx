import { useRef, useState, type MouseEvent } from "react";
import { toast } from "sonner";
import { useAuthStore } from "@/features/auth";
import { createBasket } from "../../api/api";
import { useCreateBasketItem } from "./useCreateBasketItem";
import { useUpdateBasketItem } from "./useUpdateBasketItem";
import { useGetBasket } from "./useGetBasket";
import { getBasketItemStock } from "../types";

export function useBasketAction(
  variantId?: number,
  quantity = 1,
  availableStock?: number,
) {
  const { data: basket, refetch } = useGetBasket();
  const addToBasket = useCreateBasketItem();
  const updateBasketItem = useUpdateBasketItem();
  const user = useAuthStore((state) => state.user);
  const [isAdding, setIsAdding] = useState(false);
  const pendingRef = useRef(false);

  const isInBasket = Boolean(
    variantId &&
      basket?.basketItems.some(
      (item) => item.advertisementVariantId === variantId,
      ),
  );

  const handleAdd = async (event: MouseEvent<HTMLButtonElement>) => {
    event.preventDefault();
    event.stopPropagation();

    if (pendingRef.current) return;

    if (!user) {
      toast.error("Увійдіть, щоб додати товар до кошика");
      return;
    }

    if (!variantId) {
      toast.error("Варіант товару не знайдено");
      return;
    }

    if (availableStock !== undefined && availableStock <= 0) {
      toast.error("Товару немає в наявності");
      return;
    }

    pendingRef.current = true;
    setIsAdding(true);

    let mutationStarted = false;
    try {
      const basketResult = await refetch();
      if (basketResult.isError) {
        toast.error("Не вдалося завантажити кошик");
        return;
      }

      const currentBasket = basketResult.data ?? null;
      const existingItem = currentBasket?.basketItems.find(
        (item) => item.advertisementVariantId === variantId,
      );

      if (existingItem) {
        const stock = getBasketItemStock(existingItem);
        const nextQuantity = Math.min(existingItem.quantity + quantity, stock);

        if (nextQuantity <= existingItem.quantity) {
          toast.error("У кошику вже максимальна доступна кількість");
          return;
        }

        mutationStarted = true;
        await updateBasketItem.mutateAsync({
          id: existingItem.id,
          data: { quantity: nextQuantity },
        });
        return;
      }

      const basketId = currentBasket?.id ?? (await createBasket()).id;
      const itemQuantity =
        availableStock === undefined
          ? quantity
          : Math.min(quantity, availableStock);

      mutationStarted = true;
      await addToBasket.mutateAsync({
        basketId,
        quantity: itemQuantity,
        advertisementVariantId: variantId,
      });
    } catch {
      if (!mutationStarted) {
        toast.error("Не вдалося підготувати кошик");
      }
    } finally {
      pendingRef.current = false;
      setIsAdding(false);
    }
  };

  return {
    isInBasket,
    isLoading:
      isAdding ||
      addToBasket.isPending ||
      updateBasketItem.isPending,
    handleAdd,
  };
}
