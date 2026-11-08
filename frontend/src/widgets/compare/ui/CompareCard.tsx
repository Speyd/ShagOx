import { Link } from "react-router-dom";
import { X } from "lucide-react";
import type { Advertisement } from "@/shared/lib/types/advertisements";
import { formatPrice, getCategoryLabel, getPriceRange } from "../lib/buildCompareSections";
import styles from "./CompareBoard.module.css";

type CompareCardProps = {
  advertisement: Advertisement;
  onRemove: (id: number) => void;
};

export function CompareCard({ advertisement, onRemove }: CompareCardProps) {
  const { min, max } = getPriceRange(advertisement);
  const previousPrice = advertisement.variants.find((v) => v.price === min)?.previousPrice ?? 0;
  const symbol = advertisement.currency?.symbol;
  const image = advertisement.pictures?.[0]?.url;
  const href = `/advertisement/${advertisement.id}`;

  return (
    <article className={styles.card}>
      <button
        type="button"
        className={styles.removeButton}
        onClick={() => onRemove(advertisement.id)}
        aria-label="Прибрати з порівняння"
      >
        <X size={16} />
      </button>

      <Link to={href} className={styles.imageBox}>
        {image ? (
          <img src={image} alt={advertisement.title} className={styles.image} />
        ) : (
          <span className={styles.noImage}>Немає фото</span>
        )}
      </Link>

      <div className={styles.cardInfo}>
        <span className={styles.category}>{getCategoryLabel(advertisement.category)}</span>
        <Link to={href} className={styles.title}>
          {advertisement.title}
        </Link>
        <div className={styles.priceRow}>
          <span className={styles.price}>
            {min !== max && "від "}
            {formatPrice(min, symbol)}
          </span>
          {previousPrice > min && (
            <span className={styles.oldPrice}>{formatPrice(previousPrice, symbol)}</span>
          )}
        </div>
      </div>
    </article>
  );
}
