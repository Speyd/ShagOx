import styles from "./AdvertisementCard.module.css";
import Price from "@/shared/ui/price";
import {
  getAdvertisementPrice,
  getAdvertisementStock,
  type Advertisement,
  type AdvertisementShort,
} from "@/shared/lib/types/advertisements";
import FavoriteButton from "@/features/favorites";

import { Plus, Star } from "lucide-react";

import { Link } from "react-router-dom";
import { toast } from "sonner";

export default function AdvertisementCard(
  props: Advertisement | AdvertisementShort,
) {
  const imageUrl =
    "images" in props
      ? props.images?.[0]?.url || "/placeholder-image.png"
      : "/placeholder-image.png";

  const handleAddToCart = (e: React.MouseEvent) => {
    e.preventDefault();
    e.stopPropagation();
    toast.success(`"${props.title}" додано до кошика!`);
  };

  const displayTitle = props.title;
  const displaySubtitle = props.description || "Pro max";

  return (
    <div className={styles.card}>
      <Link to={`/advertisement/${props.id}`} className={styles.imageWrapper}>
        <img
          src={imageUrl}
          alt={props.title}
          className={styles.image}
          loading="lazy"
        />
      </Link>

      <div className={styles.info}>
        <Link to={`/advertisement/${props.id}`} className={styles.titleLink}>
          <h3 className={styles.title} title={displayTitle}>
            {displayTitle}
          </h3>
          <p className={styles.subtitle} title={displaySubtitle}>
            {displaySubtitle}
          </p>
        </Link>

        <div className={styles.ratingContainer}>
          <div className={styles.rating}>
            <Star size={13} className={styles.ratingStar} />
            <span>5.0 (10 Reviews)</span>
          </div>

          {getAdvertisementStock(props) > 0 ? (
            <span className={styles.inStock}>В наявності</span>
          ) : (
            <span className={styles.outOfStock}>Out of Stock</span>
          )}
        </div>

        <Price
          price={getAdvertisementPrice(props)}
          currency={"currency" in props ? props.currency?.symbol : undefined}
        />

        <div className={styles.actionButtons}>
          <button
            type="button"
            className={styles.addButton}
            onClick={handleAddToCart}
          >
            <Plus size={15} strokeWidth={2.5} />
            <span>Add To Cart</span>
          </button>

          <FavoriteButton advertisementId={props.id} variant="card" />
        </div>
      </div>
    </div>
  );
}
