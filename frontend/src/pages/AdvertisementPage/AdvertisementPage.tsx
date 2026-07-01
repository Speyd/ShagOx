import { useParams } from "react-router-dom";
import styles from "./AdvertisementPage.module.css";
import { useGetAdvertisements } from "@/entities/Advertisement/hooks/useGetAdvertisements";

export default function AdvertisementPage() {
  const { id } = useParams<{ id: string }>();
  const advertisements = useGetAdvertisements().data ?? [];
  const advertisement = advertisements.find((item) => item.id === Number(id));

  if (!advertisement) {
    return <div>Advertisement not found</div>;
  }

  return (
    <div className={styles.advertisementPage}>
      <h1>{advertisement.title}</h1>
      <div className={styles.imageContainer}>
        <img src={advertisement.images[0]} alt="" className={styles.image} />
      </div>
      <p>{advertisement.description}</p>
      <p>{advertisement.price}</p>
    </div>
  );
}
