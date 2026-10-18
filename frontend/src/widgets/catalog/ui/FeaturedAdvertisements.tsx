import { useGetAdvertisements } from "@/entities/advertisement/model/hooks/useGetAdvertisements";
import styles from "./FeaturedAdvertisements.module.css";
import AdvertisementCard from "@/entities/advertisement/ui/AdvertisementCard";
import { Text } from "@mantine/core";

export default function FeaturedAdvertisements() {
  const { data: advertisementsData, isLoading } = useGetAdvertisements();

  if (isLoading) {
    return <Text>Завантаження...</Text>;
  }

  return (
    <section className={styles.featuredAdvertisements}>
      {advertisementsData?.items?.map((ad) => (
        <AdvertisementCard key={ad.id} {...ad} />
      ))}
    </section>
  );
}
