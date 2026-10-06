import IconButton from "@/shared/ui/icon-button";
import { useCreateBasketItem } from "../model/hooks/useCreateBasketItem";
import { useGetBasket } from "../model/hooks/useGetBasket";
import { createBasket } from "../api/api";
import { useAuthStore } from "@/features/auth";
import styles from "./AddToBasket.module.css";
import { ShoppingCart } from "lucide-react";
import { toast } from "sonner";
import { useRef } from "react";
import { useUpdateBasketItem } from "../model/hooks/useUpdateBasketItem";
import { getBasketItemStock } from "../model/types";

type AddToBasketProps = {
  advertisementVariantId: number;
  quantity?: number;
};

export default function AddToBasket({
  advertisementVariantId,
  quantity = 1,
}: AddToBasketProps) {
  const addToBasket = useCreateBasketItem();
  const updateBasketItem = useUpdateBasketItem();
  const { data: basket, refetch: refetchBasket } = useGetBasket({
    enabled: false,
  });
  const user = useAuthStore((state) => state.user);
  const pendingRef = useRef(false);

  const handleClick = async () => {
    if (pendingRef.current) return;

    if (!user) {
      toast.error("Увійдіть, щоб додати товар до кошика");
      return;
    }

    pendingRef.current = true;
    try {
      const currentBasket = basket ?? (await refetchBasket()).data;
      const basketId = currentBasket?.id ?? (await createBasket()).id;
      const existingItem = currentBasket?.basketItems.find(
        (item) => item.advertisementVariantId === advertisementVariantId,
      );

      if (existingItem) {
        const stock = getBasketItemStock(existingItem);
        const nextQuantity = Math.min(
          existingItem.quantity + quantity,
          stock || existingItem.quantity + quantity,
        );

        await updateBasketItem.mutateAsync({
          id: existingItem.id,
          data: { quantity: nextQuantity },
        });
      } else {
        await addToBasket.mutateAsync({
          basketId,
          quantity,
          advertisementVariantId,
        });
      }
    } catch {
      toast.error("Не вдалося додати товар до кошика");
    } finally {
      pendingRef.current = false;
    }
  };

  const isPending = addToBasket.isPending || updateBasketItem.isPending;
  if (isPending) {
    return (
      <IconButton disabled className={styles.icon}>
        <ShoppingCart size={24} strokeWidth={2} />
      </IconButton>
    );
  }

  return (
    <IconButton
      onClick={handleClick}
      disabled={isPending}
      className={styles.icon}
      aria-label="Додати товар до кошика"
      title="Додати товар до кошика"
    >
      <ShoppingCart
        size={24}
        strokeWidth={2}
        color="#374151"
      />
    </IconButton>
  );
}
