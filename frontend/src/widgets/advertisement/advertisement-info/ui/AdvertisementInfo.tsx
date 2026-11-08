import { useState } from "react";
import { Text } from "@mantine/core";
import { useNavigate } from "react-router-dom";
import { toast } from "sonner";
import styles from "./AdvertisementInfo.module.css";
import Price from "@/shared/ui/data-display/price";
import type { Advertisement, AdvertisementVariant } from "@/shared/lib/types/advertisements";
import CountBlock from "@/shared/ui/data-display/count-block";
import Button from "@/shared/ui/buttons/button";
import { useBasketAction } from "@/features/basket/model/hooks/useBasketAction";
import { RatingSummary } from "@/entities/review";
import { VariantPicker } from "@/entities/advertisement/ui/variant-picker";
import { getDefaultVariant } from "@/entities/advertisement/lib/variants";
import { formatAttribute } from "@/entities/advertisement/lib/attributes";
import { MAX_COMPARE_PER_CATEGORY, useCompareStore, useIsInCompare } from "@/entities/compare";

type AdvertisementInfoProps = {
  advertisement: Advertisement;
};

const LOW_STOCK = 20;

const scrollToReviews = () =>
  document.getElementById("reviews")?.scrollIntoView({ behavior: "smooth", block: "start" });

export function AdvertisementInfo({ advertisement }: AdvertisementInfoProps) {
  const navigate = useNavigate();
  const isInCompare = useIsInCompare(advertisement.id);
  const addToCompare = useCompareStore((state) => state.add);
  const removeFromCompare = useCompareStore((state) => state.remove);

  const [variant, setVariant] = useState<AdvertisementVariant | undefined>(() =>
    getDefaultVariant(advertisement.variants ?? []),
  );
  const [quantity, setQuantity] = useState(1);

  const stock = variant?.stock ?? 0;
  const price = variant?.price ?? 0;
  const previousPrice = variant?.previousPrice ?? 0;
  const symbol = advertisement.currency.symbol;
  const discount = previousPrice > price ? Math.round((1 - price / previousPrice) * 100) : 0;

  const { handleAdd, isLoading } = useBasketAction(variant?.id, quantity, stock);

  const handleSelectVariant = (next: AdvertisementVariant) => {
    setVariant(next);
    setQuantity(1);
  };

  const handleCompareClick = () => {
    if (isInCompare) {
      removeFromCompare(advertisement.id);
      return;
    }

    const added = addToCompare({ id: advertisement.id, categoryId: advertisement.category.id });
    if (!added) {
      toast.error(`Можна порівнювати до ${MAX_COMPARE_PER_CATEGORY} товарів однієї категорії`);
      return;
    }

    toast.success("Товар додано до порівняння", {
      action: { label: "Перейти", onClick: () => navigate("/compare") },
    });
  };

  return (
    <div className={styles.wrapper}>
      <Text fw={400} fz={40}>
        {advertisement.title}
      </Text>

      <Text fw={400} fz={15}>
        {advertisement.description}
      </Text>

      <RatingSummary advertisementId={advertisement.id} size="lg" onReviewsClick={scrollToReviews} />

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

      <VariantPicker
        variants={advertisement.variants ?? []}
        selected={variant}
        onSelect={handleSelectVariant}
      />

      {advertisement.attributes?.length > 0 && (
        <>
          <div className="divider" />
          <div className={styles.properties}>
            {advertisement.attributes.map((attr) => (
              <div className={styles.property} key={attr.attribute.id}>
                <Text fw={400} fz={15} c="dimmed">
                  {attr.attribute.label || attr.attribute.key}:
                </Text>
                <Text fw={600} fz={17}>
                  {formatAttribute(attr)}
                </Text>
              </div>
            ))}
          </div>
        </>
      )}

      <div className="divider" />

      <div className={styles.countContainer}>
        <CountBlock value={quantity} onChange={setQuantity} max={stock} />
        {stock === 0 ? (
          <Text fz={14} fw={600} c="red">
            Немає в наявності
          </Text>
        ) : (
          stock <= LOW_STOCK && (
            <Text fz={14} fw={400}>
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
          disabled={isLoading || stock === 0 || !variant}
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
