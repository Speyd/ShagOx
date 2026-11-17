import { useRef, useState, type MouseEvent } from "react";
import { toast } from "sonner";
import { useAuthStore } from "@/features/auth";
import { createBasket } from "../../api/api";
import { useCreateBasketItem } from "./useCreateBasketItem";
import { useUpdateBasketItem } from "./useUpdateBasketItem";
import { useGetBasket } from "./useGetBasket";
import { getBasketItemStock, isSameBasketProduct } from "../types";

type BasketActionOptions = {
  advertisementId: number;
  variantId?: number | null;
  quantity?: number;
  availableStock?: number | null;
};

export function useBasketAction({
  advertisementId,
  variantId = null,
  quantity = 1,
  availableStock = null,
}: BasketActionOptions) {
  const { data: basket, refetch } = useGetBasket();
  const addToBasket = useCreateBasketItem();
  const updateBasketItem = useUpdateBasketItem();
  const user = useAuthStore((state) => state.user);
  const [isAdding, setIsAdding] = useState(false);
  const pendingRef = useRef(false);

  const productKey = { advertisementId, advertisementVariantId: variantId };

  const isInBasket = Boolean(
    basket?.basketItems.some((item) => isSameBasketProduct(item, productKey)),
  );

  const handleAdd = async (event: MouseEvent<HTMLButtonElement>) => {
    event.preventDefault();
    event.stopPropagation();

    if (pendingRef.current) return;

    if (!user) {
      toast.error("Увійдіть, щоб додати товар до кошика");
      return;
    }

    if (availableStock !== null && availableStock <= 0) {
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
      const existingItem = currentBasket?.basketItems.find((item) =>
        isSameBasketProduct(item, productKey),
      );

      if (existingItem) {
        const stock = getBasketItemStock(existingItem);
        const wanted = existingItem.quantity + quantity;
        const nextQuantity = stock === null ? wanted : Math.min(wanted, stock);

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

      const parentId = currentBasket?.id ?? (await createBasket()).id;
      const itemQuantity =
        availableStock === null ? quantity : Math.min(quantity, availableStock);

      mutationStarted = true;
      await addToBasket.mutateAsync({
        parentId,
        advertisementId,
        advertisementVariantId: variantId,
        quantity: itemQuantity,
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
    isLoading: isAdding || addToBasket.isPending || updateBasketItem.isPending,
    handleAdd,
  };
}
