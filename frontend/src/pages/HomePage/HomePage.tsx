import AdvertisementCard from "@/entities/Advertisement";
import styles from "./HomePage.module.css";
import { useGetAdvertisements } from "@/entities/Advertisement/hooks/useGetAdvertisements";

export default function HomePage() {
  const advertisements = useGetAdvertisements();

  return (
    <div className={styles.homePage}>
      <h1>HomePage</h1>
      <div className={styles.advertisementContainer}>
        {advertisements.data?.map((item) => (
          <AdvertisementCard key={item.id} {...item} />
        ))}
      </div>
    </div>
  );
}
