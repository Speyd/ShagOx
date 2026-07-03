import { useNavigate } from "react-router-dom";
import type { Advertisement } from "../model/types";
import styles from "./AdvertisementCard.module.css";
import DeleteAdvertisementButton from "@/features/delete-advertisement";
import { UpdateAdvertisementButton } from "@/features/update-advertisement/ui";
import Price from "@/shared/ui/Price";
import { formatDate } from "@/shared/lib/formatDate";

export default function AdvertisementCard(props: Advertisement) {
  const navigate = useNavigate();

  return (
    <div className={styles.card}>
      <div className={styles.left}>
        <div
          className={styles.imageContainer}
          onClick={() => navigate(`/advertisement/${props.id}`)}
        >
          <img src={props.images[0]} alt="" className={styles.image} />
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
        <Price value={props.price} />
        <div className={styles.buttons}>
          <UpdateAdvertisementButton id={props.id} />
          <DeleteAdvertisementButton id={props.id} />
        </div>
      </div>
    </div>
  );
}
