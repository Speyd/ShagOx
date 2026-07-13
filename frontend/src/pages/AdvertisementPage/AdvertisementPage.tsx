import { useParams } from "react-router-dom";
import styles from "./AdvertisementPage.module.css";
import DeleteAdvertisementButton from "@/features/delete-advertisement";
import { useGetAdvertisement } from "@/entities/Advertisement/hooks/useGetAdvertisement";
import { UpdateAdvertisementButton } from "@/features/update-advertisement/ui";
import Price from "@/shared/ui/Price";

export default function AdvertisementPage() {
  const { id } = useParams<{ id: string }>();

  const { data: advertisement, isLoading } = useGetAdvertisement(Number(id));

  if (isLoading) {
    return <div>Loading...</div>;
  }

  if (!advertisement) {
    return <div>Advertisement not found</div>;
  }

  return (
    <div className={styles.advertisementPage}>
      <div className={styles.left}>
        <h1>{advertisement.title}</h1>
        <div className={styles.imageContainer}>
          <img
            src={advertisement.images[0].url}
            alt=""
            className={styles.image}
          />
        </div>
        <p>{advertisement.description}</p>
        <div>
          <h2>Додаткова інформація</h2>
          <ul>
            {Object.entries(advertisement.properties).map(([key, value]) => (
              <li key={key}>
                <strong>{key}:</strong> {value}
              </li>
            ))}
          </ul>
        </div>
      </div>

      <div className={styles.right}>
        <div className={styles.buttons}>
          <UpdateAdvertisementButton id={advertisement.id} />
          <DeleteAdvertisementButton id={advertisement.id} />
        </div>
        <Price value={advertisement.price} />
      </div>
    </div>
  );
}
