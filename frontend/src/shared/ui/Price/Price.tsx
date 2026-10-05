import styles from "./Price.module.css";

type PriceProps = {
  price?: number | null;
  currency?: string;
};

export default function Price({ price, currency = "₴" }: PriceProps) {
  const normalizedPrice = typeof price === "number" ? price : 0;

  return (
    <span className={styles.price}>
      {normalizedPrice.toLocaleString("uk-UA")} {currency}
    </span>
  );
}
