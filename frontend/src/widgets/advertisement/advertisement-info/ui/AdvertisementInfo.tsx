import { useState } from "react";
import { Text } from "@mantine/core";
import { useNavigate } from "react-router-dom";
import { toast } from "sonner";
import styles from "./AdvertisementInfo.module.css";
import Price from "@/shared/ui/data-display/price";
import type {
  Advertisement,
  AdvertisementVariant,
} from "@/shared/lib/types/advertisements";
import CountBlock from "@/shared/ui/data-display/count-block";
import Button from "@/shared/ui/buttons/button";
import { useBasketAction } from "@/features/basket/model/hooks/useBasketAction";
import { RatingSummary } from "@/entities/review";
import { VariantPicker } from "@/entities/advertisement/ui/variant-picker";
import { getDefaultVariant } from "@/entities/advertisement/lib/variants";
import { toDisplayPrice } from "@/entities/advertisement/lib/price";
import { formatAttribute } from "@/entities/advertisement/lib/attributes";
import {
  MAX_COMPARE_PER_CATEGORY,
  useCompareStore,
  useIsInCompare,
} from "@/entities/compare";

type AdvertisementInfoProps = {
  advertisement: Advertisement;
};

const LOW_STOCK = 20;

const scrollToReviews = () =>
  document
    .getElementById("reviews")
    ?.scrollIntoView({ behavior: "smooth", block: "start" });

export function AdvertisementInfo({ advertisement }: AdvertisementInfoProps) {
  const navigate = useNavigate();
  const isInCompare = useIsInCompare(advertisement.id);
  const addToCompare = useCompareStore((state) => state.add);
  const removeFromCompare = useCompareStore((state) => state.remove);

  const [variant, setVariant] = useState<AdvertisementVariant | undefined>(() =>
    getDefaultVariant(advertisement.variants ?? []),
  );
  const [quantity, setQuantity] = useState(1);

  const hasVariants = (advertisement.variants ?? []).length > 0;
  const stock = hasVariants ? (variant?.stock ?? 0) : null;
  const conversion = variant?.priceConversion ?? advertisement.priceConversion;
  const fallbackSymbol = advertisement.currency?.symbol;
  const { amount: price, symbol } = toDisplayPrice(
    variant?.price ?? advertisement.price ?? 0,
    conversion?.price,
    fallbackSymbol,
  );
  const { amount: previousPrice } = toDisplayPrice(
    variant?.previousPrice ?? advertisement.previousPrice ?? 0,
    conversion?.previousPrice ?? conversion?.price,
    fallbackSymbol,
  );
  const discount =
    previousPrice > price ? Math.round((1 - price / previousPrice) * 100) : 0;
  const isOutOfStock = stock === 0;

  const { handleAdd, isLoading } = useBasketAction({
    advertisementId: advertisement.id,
    variantId: variant?.id,
    quantity,
    availableStock: stock,
  });

  const handleSelectVariant = (next: AdvertisementVariant) => {
    setVariant(next);
    setQuantity(1);
  };

  const handleCompareClick = () => {
    if (isInCompare) {
      removeFromCompare(advertisement.id);
      return;
    }

    const added = addToCompare({
      id: advertisement.id,
      categoryId: advertisement.category.id,
    });
    if (!added) {
      toast.error(
        `Можна порівнювати до ${MAX_COMPARE_PER_CATEGORY} товарів однієї категорії`,
      );
      return;
    }

    toast.success("Товар додано до порівняння", {
      action: { label: "Перейти", onClick: () => navigate("/compare") },
    });
  };

  return (
    <div className={styles.wrapper}>
      <Text className={styles.title}>
        {advertisement.title}
      </Text>

      <Text className={styles.description}>
        {advertisement.description}
      </Text>

      <RatingSummary
        advertisementId={advertisement.id}
        size="lg"
        onReviewsClick={scrollToReviews}
      />

      <div className="divider" />

      <div className={styles.priceBlock}>
        <Price price={price} currency={symbol} />
        {discount > 0 && (
          <>
            <span className={styles.oldPrice}>
              {previousPrice.toLocaleString("uk-UA")} {symbol}
            </span>
            <span className={styles.discount}>−{discount}%</span>
          </>
        )}
      </div>

      <div className="divider" />

      {advertisement.variants?.length > 0 && (
        <>
          <VariantPicker
            variants={advertisement.variants ?? []}
            selected={variant}
            onSelect={handleSelectVariant}
          />
        </>
      )}

      {advertisement.attributes?.length > 0 && (
        <>
          <div className={styles.properties}>
            {advertisement.attributes.map((attr) => (
              <div className={styles.property} key={attr.attribute.id}>
                <Text className={styles.propertyLabel} c="dimmed">
                  {attr.attribute.label || attr.attribute.key}:
                </Text>
                <Text className={styles.propertyValue}>
                  {formatAttribute(attr)}
                </Text>
              </div>
            ))}
          </div>
        </>
      )}

      <div className="divider" />

      <div className={styles.countContainer}>
        <CountBlock
          value={quantity}
          onChange={setQuantity}
          max={stock ?? undefined}
        />
        {isOutOfStock ? (
          <Text className={styles.stockOut} c="red">
            Немає в наявності
          </Text>
        ) : (
          stock !== null &&
          stock <= LOW_STOCK && (
            <Text className={styles.stockLow}>
              Залишилось всього <b>{stock} шт.</b>
              <br /> встигни придбати
            </Text>
          )
        )}
      </div>

      <div className={styles.buttonsContainer}>
        <Button
          className={styles.actionButton}
          onClick={handleAdd}
          disabled={isLoading || isOutOfStock || (hasVariants && !variant)}
        >
          {isLoading ? "Додаємо..." : "Купити"}
        </Button>
        <Button className={styles.actionButton} onClick={handleCompareClick}>
          {isInCompare ? "У порівнянні" : "Порівняти"}
        </Button>
      </div>
    </div>
  );
}
