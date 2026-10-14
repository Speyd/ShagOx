import { Text } from "@mantine/core";
import styles from "./AdvertisementInfo.module.css";
import { Star } from "lucide-react";
import { Link } from "react-router-dom";
import Price from "@/shared/ui/price";
import type { Advertisement } from "@/shared/lib/types/advertisements";
import CountBlock from "@/shared/ui/count-block";
import Button from "@/shared/ui/button";

type AdvertisementInfoProps = Pick<
  Advertisement,
  "title" | "description" | "price" | "currency" | "stock" | "properties"
>;

export default function AdvertisementInfo({
  title,
  description,
  price,
  currency,
  stock,
  properties,
}: AdvertisementInfoProps) {
  return (
    <div className={styles.wrapper}>
      <Text fw={400} fz={40}>
        {title}
      </Text>

      <Text fw={400} fz={15}>
        {description}
      </Text>

      <div className={styles.ratingContainer}>
        <div className={styles.rating}>
          {[...Array(5)].map((_, index) => (
            <Star
              key={index}
              size={30}
              fill="var(--color-base)"
              color="var(--color-base)"
            />
          ))}
          <Link
            to="/reviews"
            style={{ textDecoration: "none", color: "inherit" }}
          >
            <Text fz={15} c="var(--color-primary)">
              (367)
            </Text>
          </Link>
        </div>
      </div>

      <div className="divider" />

      <Price price={price} currency={currency.symbol} />

      <div className="divider" />

      <div className={styles.properties}>
        {Object.entries(properties ?? {}).map(([key, value]) => (
          <div className={styles.property} key={key}>
            <Text fw={400} fz={15} c="dimmed">
              {key}:
            </Text>
            <Text fw={600} fz={17}>
              {value}
            </Text>
          </div>
        ))}
      </div>

      <div className="divider" />

      <div className={styles.countContainer}>
        <CountBlock max={stock} />
        {stock <= 20 && stock > 0 && (
          <Text fz={14} fw={400}>
            Залишилось всього <b>{stock} штук!</b>
            <br /> встигни придбати
          </Text>
        )}
      </div>

      <div className={styles.buttonsContainer}>
        <Button className={styles.actionButton}>Купити</Button>
        <Button className={styles.actionButton}>Порівняти</Button>
      </div>
    </div>
  );
}
