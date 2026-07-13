import AdvertisementCard from "@/entities/Advertisement";
import styles from "./HomePage.module.css";
import { useGetAdvertisements } from "@/entities/Advertisement/hooks/useGetAdvertisements";

export default function HomePage() {
  const { data: advertisements, isLoading, isError } = useGetAdvertisements();

  if (isLoading) return <div>Loading...</div>;

  if (isError) return <div>Error</div>;

  return (
    <div className={styles.homePage}>
      <h1>HomePage</h1>
      <div className={styles.advertisementContainer}>
        {advertisements?.map((item) => (
          <AdvertisementCard key={item.id} {...item} />
        ))}
      </div>
    </div>
  );
}
