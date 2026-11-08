import { useState } from "react";
import { Link } from "react-router-dom";
import { Button, Image, Text } from "@mantine/core";
import { Check, Plus } from "lucide-react";
import styles from "./AdvertisementCard.module.css";
import Price from "@/shared/ui/data-display/price";
import type { Advertisement } from "@/shared/lib/types/advertisements";
import FavoriteButton from "@/features/favorites";
import { useBasketAction } from "@/features/basket/model/hooks/useBasketAction";
import { RatingSummary } from "@/entities/review";
import { getDefaultVariant } from "../../lib/variants";
import { VariantPicker } from "../variant-picker";

export function AdvertisementCard(props: Advertisement) {
  const [variant, setVariant] = useState(() =>
    getDefaultVariant(props.variants ?? []),
  );
  const { handleAdd, isInBasket, isLoading } = useBasketAction(variant?.id);

  const imageUrl = props.pictures?.[0]?.url || "/placeholder-image.png";
  const href = `/advertisement/${props.id}`;
  const price = variant?.price ?? 0;
  const previousPrice = variant?.previousPrice ?? 0;
  const inStock = (variant?.stock ?? 0) > 0;

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
          <RatingSummary advertisementId={props.id} />

          {inStock ? (
            <Text className={styles.inStock}>В наявності</Text>
          ) : (
            <Text className={styles.outOfStock}>Не в наявності</Text>
          )}
        </div>

        <div className={styles.variantPickerContainer}>
          <VariantPicker
            size="sm"
            variants={props.variants ?? []}
            selected={variant}
            onSelect={setVariant}
          />
        </div>

        <div className={styles.priceRow}>
          <Price price={price} currency={props.currency?.symbol} />
          {previousPrice > price && (
            <span className={styles.oldPrice}>
              {previousPrice.toLocaleString("uk-UA")}{" "}
              {props.currency?.symbol ?? "₴"}
            </span>
          )}
        </div>

        <div className={styles.actionButtons}>
          <Button
            onClick={handleAdd}
            loading={isLoading}
            disabled={!variant || !inStock}
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
