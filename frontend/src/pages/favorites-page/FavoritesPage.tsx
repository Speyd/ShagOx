import { useGetFavorites } from "@/features/favorites/model/hooks/useGetFavorites";
import { Loader } from "lucide-react";
import AdvertisementCard from "@/entities/advertisement";
import Container from "@/shared/ui/container";
import styles from "./FavoritesPage.module.css";

export default function FavoritesPage() {
  const { data: favorites, isLoading } = useGetFavorites();

  if (isLoading) return <Loader />;

  return (
    <Container>
      <div className={styles.favoritesPage}>
        <h1 className={styles.title}>Обране</h1>

        {favorites?.items.length ? (
          <div className={styles.list}>
            {favorites.items.map((item) => (
              <AdvertisementCard
                key={item.advertisement.id}
                {...item.advertisement}
              />
            ))}
          </div>
        ) : (
          <p className={styles.empty}>У вас поки що немає обраних оголошень.</p>
        )}
      </div>
    </Container>
  );
}
