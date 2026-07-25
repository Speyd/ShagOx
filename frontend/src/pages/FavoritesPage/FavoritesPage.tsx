import { useGetFavorites } from "@/features/favorites/model/hooks/useGetFavorites";
import { Loader } from "lucide-react";
import AdvertisementCard from "@/entities/Advertisement";
import Container from "@/shared/ui/Container";
import styles from "./FavoritesPage.module.css";

export default function FavoritesPage() {
  const { data: favorites, isLoading } = useGetFavorites();

  if (isLoading) return <Loader />;

  return (
    <Container>
      <div className={styles.favoritesPage}>
        <h1 className={styles.title}>Обране</h1>

        {favorites?.length ? (
          <div className={styles.list}>
            {favorites.map((item) => (
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
