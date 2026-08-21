import AdvertisementCard from "@/entities/advertisement";
import styles from "./HomePage.module.css";
import { useGetAdvertisements } from "@/entities/advertisement/model/hooks/useGetAdvertisements";

export default function HomePage() {
  const { data, isLoading, isError } = useGetAdvertisements();

  console.log(data);

  if (isLoading) return <div>Loading...</div>;

  if (isError) return <div>Error</div>;

  if (!data) return <div>No data</div>;

  return (
    <div className={styles.homePage}>
      <h1>Головна</h1>
      <div className={styles.advertisementContainer}>
        {data.items.map((item) => (
          <AdvertisementCard key={item.id} {...item} />
        ))}
      </div>
    </div>
  );
}
