import { useNavigate } from "react-router-dom";
import styles from "./AdvertisementCard.module.css";
import Price from "@/shared/ui/price";
import { formatDate } from "@/shared/lib/formatDate";
import type { Advertisement } from "@/shared/lib/types/advertisements";
import FavoriteButton from "@/features/favorites";
import DeleteAdvertisementButton from "@/features/advertisement/delete-advertisement";
import UpdateAdvertisementButton from "@/features/advertisement/update-advertisement/ui/UpdateAdvertisementButton";
import { useAuthStore } from "@/features/auth/store/useAuthStore";

export default function AdvertisementCard(props: Advertisement) {
  const navigate = useNavigate();

  const user = useAuthStore((state) => state.user);

  const isOwner = user?.id === props.seller.id;

  return (
    <div className={styles.card}>
      <div className={styles.left}>
        <div
          className={styles.imageContainer}
          onClick={() => navigate(`/advertisement/${props.id}`)}
        >
          <img
            src={props.images[0]?.url ?? "/placeholder.png"}
            alt={props.title}
            className={styles.image}
          />
        </div>

        <div className={styles.info}>
          <div className={styles.infoTitle}>
            <h2>{props.title}</h2>
            <p>{props.description}</p>
          </div>

          <p>{formatDate(props.createdAt)}</p>
        </div>
      </div>

      <div className={styles.right}>
        <Price value={props.price} currency={props.currency.symbol} />
        <div className={styles.buttons}>
          {isOwner && <UpdateAdvertisementButton id={props.id} />}
          {isOwner && <DeleteAdvertisementButton id={props.id} />}
          <FavoriteButton advertisementId={props.id} />
        </div>
      </div>
    </div>
  );
}
