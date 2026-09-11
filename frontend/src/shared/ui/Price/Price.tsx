import styles from "./Price.module.css";

type PriceProps = {
  price: number;
  currency?: string;
};

export default function Price({ price, currency = "₴" }: PriceProps) {
  return (
    <span className={styles.price}>
      {price.toLocaleString("uk-UA")} {currency}
    </span>
  );
}
