import { useState } from "react";
import { Link } from "react-router-dom";
import { Button, Image, Text } from "@mantine/core";
import { Check, ShoppingCart } from "lucide-react";
import styles from "./AdvertisementCard.module.css";
import Price from "@/shared/ui/data-display/price";
import type {
  Advertisement,
  CatalogAdvertisement,
  CatalogAdvertisementVariant,
} from "@/shared/lib/types/advertisements";
import FavoriteButton from "@/features/favorites";
import { useBasketAction } from "@/features/basket/model/hooks/useBasketAction";
import { RatingSummary } from "@/entities/review";
import { getDefaultVariant } from "../../lib/variants";
import { toDisplayPrice } from "../../lib/price";
import { VariantPicker } from "../variant-picker";

type AdvertisementCardProps = Advertisement | CatalogAdvertisement;

export function AdvertisementCard(props: AdvertisementCardProps) {
  const rating =
    "reviewCount" in props
      ? { count: props.reviewCount, average: props.averageRatings }
      : undefined;
  const variants: CatalogAdvertisementVariant[] = props.variants ?? [];
  const hasVariants = variants.length > 0;
  const [variant, setVariant] = useState(() => getDefaultVariant(variants));
  const { handleAdd, isInBasket, isLoading } = useBasketAction({
    advertisementId: props.id,
    variantId: variant?.id,
    availableStock: variant?.stock ?? null,
  });

  const cover = "pictures" in props ? props.pictures[0] : props.picture;
  const imageUrl = cover?.url || "/placeholder-image.png";
  const href = `/advertisement/${props.id}`;
  const fallbackSymbol = "currency" in props ? props.currency?.symbol : undefined;
  const conversion = props.priceConversion;
  const price = toDisplayPrice(
    variant?.price ?? props.price ?? 0,
    conversion?.price,
    fallbackSymbol,
  );
  const previousPrice = toDisplayPrice(
    variant?.previousPrice ?? props.previousPrice ?? 0,
    conversion?.previousPrice ?? conversion?.price,
    fallbackSymbol,
  );
  const inStock = hasVariants ? (variant?.stock ?? 0) > 0 : true;

  return (
    <div className={styles.card}>
      <Link to={href} className={styles.imageWrapper}>
        <Image
          src={imageUrl}
          alt={props.title}
          fit="contain"
          className={styles.image}
        />
      </Link>

      <div className={styles.info}>
        <Link to={href} className={styles.titleLink}>
          <Text fw={600} className={styles.title}>
            {props.title}
          </Text>
        </Link>

        <Text c="dimmed" className={styles.description}>
          {props.description}
        </Text>

        <div className={styles.ratingContainer}>
          <RatingSummary advertisementId={props.id} rating={rating} />

          {inStock ? (
            <Text className={styles.inStock}>В наявності</Text>
          ) : (
            <Text className={styles.outOfStock}>Не в наявності</Text>
          )}
        </div>

        <div className={styles.variantPickerContainer}>
          <VariantPicker
            size="sm"
            variants={variants}
            selected={variant}
            onSelect={setVariant}
          />
        </div>

        <Price
          price={price.amount}
          oldPrice={previousPrice.amount}
          currency={price.symbol}
        />

        <div className={styles.actionButtons}>
          <Button
            onClick={handleAdd}
            loading={isLoading}
            disabled={(hasVariants && !variant) || !inStock}
            leftSection={
              isInBasket ? (
                <Check className={styles.btnIcon} />
              ) : (
                <ShoppingCart className={styles.btnIcon} />
              )
            }
            bg="var(--color-base)"
            className={styles.addButton}
            aria-label={isInBasket ? "У кошику" : "У кошик"}
          >
            <span className={styles.buttonText}>
              {isInBasket ? "У кошику" : "У кошик"}
            </span>
          </Button>
          <FavoriteButton
            variant="card"
            advertisementId={props.id}
            defaultIsFavorite={
              "isFavorite" in props ? props.isFavorite : undefined
            }
          />
        </div>
      </div>
    </div>
  );
}
