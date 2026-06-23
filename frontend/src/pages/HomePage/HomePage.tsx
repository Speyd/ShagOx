import advertisements from "@/data/advertisements.json";
import AdvertisementCard from "@/entities/Advertisement";
import styles from "./HomePage.module.css";

export default function HomePage() {
  return (
    <div className={styles.homePage}>
      <h1>HomePage</h1>
      <div className={styles.advertisementContainer}>
        {advertisements.map((advertisement) => (
          <AdvertisementCard {...advertisement} />
        ))}
      </div>
    </div>
  );
}
