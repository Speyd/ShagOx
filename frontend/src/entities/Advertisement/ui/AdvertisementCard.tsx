import styles from "./AdvertisementCard.module.css";
import Price from "@/shared/ui/price";
import {
  getAdvertisementPrice,
  getAdvertisementStock,
  type Advertisement,
  type AdvertisementShort,
} from "@/shared/lib/types/advertisements";
import FavoriteButton from "@/features/favorites";
import { useBasketAction } from "@/features/basket/model/hooks/useBasketAction";

import { Check, Plus, Star } from "lucide-react";
import { Link } from "react-router-dom";

export default function AdvertisementCard(
  props: Advertisement | AdvertisementShort,
) {
  const targetVariantId = props.variants?.[0]?.id;
  const stock = getAdvertisementStock(props);
  const { handleAdd, isLoading, isInBasket } =
    useBasketAction(targetVariantId, 1, stock);

  const imageUrl =
    "images" in props
      ? props.images?.[0]?.url || "/placeholder-image.png"
      : "/placeholder-image.png";

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
            className={`${styles.addButton} ${isInBasket ? styles.inBasket : ""}`}
            onClick={handleAdd}
            disabled={isLoading || isInBasket || stock <= 0}
          >
            {isInBasket ? (
              <>
                <Check size={15} strokeWidth={2.5} />
                <span>У кошику</span>
              </>
            ) : (
              <>
                <Plus size={15} strokeWidth={2.5} />
                <span>{isLoading ? "Додавання..." : "Додати в кошик"}</span>
              </>
            )}
          </button>

          <FavoriteButton advertisementId={props.id} variant="card" />
        </div>
      </div>
    </div>
  );
}
