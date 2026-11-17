import { Text } from "@mantine/core";
import styles from "./Price.module.css";

type PriceProps = {
  price?: number | null;
  oldPrice?: number | null;
  currency?: string;
};

export default function Price({ price, oldPrice, currency = "₴" }: PriceProps) {
  const normalizedPrice = typeof price === "number" ? price : 0;
  const normalizedOldPrice = typeof oldPrice === "number" ? oldPrice : 0;
  const hasDiscount = normalizedOldPrice > normalizedPrice;

  return (
    <div className={styles.priceWrapper}>
      <Text className={styles.price} fw={700}>
        {normalizedPrice.toLocaleString("uk-UA")} {currency}
      </Text>

      {hasDiscount && (
        <Text className={styles.oldPrice} td="line-through">
          {normalizedOldPrice.toLocaleString("uk-UA")} {currency}
        </Text>
      )}
    </div>
  );
}
