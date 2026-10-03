import IconButton from "@/shared/ui/icon-button";
import { useCreateBasketItem } from "../model/hooks/useCreateBasketItem";
import { useGetBasket } from "../model/hooks/useGetBasket";
import { createBasket } from "../api/api";
import { useAuthStore } from "@/features/auth";
import styles from "./AddToBasket.module.css";
import { ShoppingCart } from "lucide-react";
import { toast } from "sonner";

type AddToBasketProps = {
  advertisementVariantId: number;
  quantity?: number;
};

export default function AddToBasket({
  advertisementVariantId,
  quantity = 1,
}: AddToBasketProps) {
  const addToBasket = useCreateBasketItem();
  const { data: basket, refetch: refetchBasket } = useGetBasket({
    enabled: false,
  });
  const user = useAuthStore((state) => state.user);

  const handleClick = async () => {
    if (!user) {
      toast.error("Увійдіть, щоб додати товар до кошика");
      return;
    }

    try {
      const currentBasket = basket ?? (await refetchBasket()).data;
      const basketId = currentBasket?.id ?? (await createBasket()).id;
      addToBasket.mutate({ basketId, quantity, advertisementVariantId });
    } catch {
      toast.error("Не вдалося створити кошик");
    }
  };

  const isPending = addToBasket.isPending;
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
