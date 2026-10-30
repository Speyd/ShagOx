import styles from "./AdvertisementCard.module.css";
import Price from "@/shared/ui/price";
import {
  getAdvertisementPrice,
  getAdvertisementStock,
  type Advertisement,
  type AdvertisementShort,
} from "@/shared/lib/types/advertisements";
import FavoriteButton from "@/features/favorites";
import { useBasketAction } from "@/features/basket/model/hooks/useBasketAction";
import { Button, Image, Text } from "@mantine/core";
import { Check, Plus, Star } from "lucide-react";
import { Link } from "react-router-dom";

export default function AdvertisementCard(
  props: Advertisement | AdvertisementShort,
) {
  const variantId = props.variants[0]?.id;
  const { handleAdd, isInBasket, isLoading } = useBasketAction(variantId);
  const imageUrl =
    "pictures" in props
      ? props.pictures?.[0]?.url || "/placeholder-image.png"
      : "/placeholder-image.png";

  return (
    <div className={styles.card}>
      <Link to={`/advertisement/${props.id}`} className={styles.imageWrapper}>
        <Image
          src={imageUrl}
          alt={props.title}
          fit="contain"
          className={styles.image}
        />
      </Link>

      <div className={styles.info}>
        <Link to={`/advertisement/${props.id}`} className={styles.titleLink}>
          <Text fw={600} className={styles.title}>
            {props.title}
          </Text>
        </Link>

        <Text c="dimmed" className={styles.description}>
          {props.description}
        </Text>

        <div className={styles.ratingContainer}>
          <div className={styles.rating}>
            <Star className={styles.ratingStar} />
            <Text className={styles.ratingText}>4.8</Text>
            <Text className={styles.ratingCount}>(12)</Text>
          </div>

          {getAdvertisementStock(props) > 0 ? (
            <Text className={styles.inStock}>В наявності</Text>
          ) : (
            <Text className={styles.outOfStock}>Не в наявності</Text>
          )}
        </div>

        <Price
          price={getAdvertisementPrice(props)}
          currency={"currency" in props ? props.currency?.symbol : undefined}
        />

        <div className={styles.actionButtons}>
          <Button
            onClick={handleAdd}
            loading={isLoading}
            disabled={!variantId}
            leftSection={
              isInBasket ? (
                <Check className={styles.btnIcon} />
              ) : (
                <Plus className={styles.btnIcon} />
              )
            }
            bg="var(--color-base)"
            className={styles.addButton}
          >
            {isInBasket ? "У кошику" : "У кошик"}
          </Button>
          <FavoriteButton advertisementId={props.id} />
        </div>
      </div>
    </div>
  );
}
