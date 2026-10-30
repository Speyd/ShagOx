import { useState } from "react";
import { Text } from "@mantine/core";
import styles from "./AdvertisementInfo.module.css";
import { Star } from "lucide-react";
import { Link } from "react-router-dom";
import Price from "@/shared/ui/price";
import {
  formatAttributeValue,
  type Advertisement,
} from "@/shared/lib/types/advertisements";
import CountBlock from "@/shared/ui/count-block";
import Button from "@/shared/ui/button";
import { useBasketAction } from "@/features/basket/model/hooks/useBasketAction";

type AdvertisementInfoProps = { advertisement: Advertisement };

export default function AdvertisementInfo({
  advertisement,
}: AdvertisementInfoProps) {
  const [quantity, setQuantity] = useState(1);
  const variant = advertisement.variants?.[0];
  const attributes = new Map<string, string>();

  Object.entries(advertisement.properties ?? {}).forEach(([key, value]) => {
    const text = formatAttributeValue(value);
    if (text) attributes.set(key, text);
  });


  const stock = variant?.stock ?? advertisement.stock ?? 0;
  const { handleAdd, isLoading } = useBasketAction(
    variant?.id,
    quantity,
    stock,
  );

  return (
    <div className={styles.wrapper}>
      <Text fw={400} fz={40}>
        {advertisement.title}
      </Text>

      <Text fw={400} fz={15}>
        {advertisement.description}
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

      <Price
        price={advertisement.price}
        currency={advertisement.currency.symbol}
      />

      <div className="divider" />

      <div className={styles.properties}>
        {[...attributes.entries()].map(([key, value]) => (
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
        <CountBlock
          value={quantity}
          onChange={setQuantity}
          max={stock}
        />
        {stock <= 20 && stock > 0 && (
          <Text fz={14} fw={400}>
            Залишилось всього <b>{stock} штук!</b>
            <br /> встигни придбати
          </Text>
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
        <Button className={styles.actionButton}>Порівняти</Button>
      </div>
    </div>
  );
}