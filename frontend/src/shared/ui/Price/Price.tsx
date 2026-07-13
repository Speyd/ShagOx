import styles from "./Price.module.css";

type PriceProps = {
  value: number;
  currency?: string;
};

export default function Price({ value, currency = "₴" }: PriceProps) {
  return (
    <span className={styles.price}>
      {value.toLocaleString("uk-UA")} {currency}
    </span>
  );
}
