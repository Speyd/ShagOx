import { useNavigate } from "react-router-dom";
import type { Advertisement } from "../model/types";
import styles from "./AdvertisementCard.module.css";

export default function AdvertisementCard(props: Advertisement) {
  const navigate = useNavigate();

  return (
    <div className={styles.card}>
      <div className={styles.content}>
        <div
          className={styles.imageContainer}
          onClick={() => navigate(`/advertisement/${props.id}`)}
        >
          <img src={props.images[0]} alt="" className={styles.image} />
        </div>

        <div className={styles.info}>
          <h2>
            {props.title}, {props.id}
          </h2>

          <div className={styles.conditionContainer}>
            <p className={styles.conditionText}>{props.condition}</p>
          </div>

          <p>{props.category}</p>
        </div>
      </div>

      <div className={styles.price}>{props.price}</div>
    </div>
  );
}
