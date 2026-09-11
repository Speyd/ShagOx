import { useParams } from "react-router-dom";
import styles from "./AdvertisementPage.module.css";
import Price from "@/shared/ui/price";
import UpdateAdvertisementButton from "@/features/advertisement/update-advertisement/ui/UpdateAdvertisementButton";
import DeleteAdvertisementButton from "@/features/advertisement/delete-advertisement";
import FavoriteButton from "@/features/favorites";
import { useAuthStore } from "@/features/auth/store/useAuthStore";
import { useGetAdvertisement } from "@/entities/advertisement/model/hooks/useGetAdvertisement";

export default function AdvertisementPage() {
  const { id } = useParams<{ id: string }>();

  const { data: advertisement, isLoading } = useGetAdvertisement(Number(id));
  const user = useAuthStore((state) => state.user);

  if (isLoading) {
    return <div>Loading...</div>;
  }

  if (!advertisement) {
    return <div>Advertisement not found</div>;
  }

  const mainImageUrl = advertisement.images?.[0]?.url || "/placeholder-image.png";

  return (
    <div className={styles.advertisementPage}>
      <div className={styles.left}>
        <h1>{advertisement.title}</h1>
        <div className={styles.imageContainer}>
          <img
            src={mainImageUrl}
            alt={advertisement.title}
            className={styles.image}
          />
        </div>
        <p>{advertisement.description}</p>
        {advertisement.properties && Object.keys(advertisement.properties).length > 0 && (
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
        )}
      </div>

      <div className={styles.right}>
        <div className={styles.buttons}>
          <FavoriteButton advertisementId={advertisement.id} />
          {user?.id === advertisement.seller.id && (
            <UpdateAdvertisementButton id={advertisement.id} />
          )}
          {user?.id === advertisement.seller.id && (
            <DeleteAdvertisementButton id={advertisement.id} />
          )}
        </div>
        <Price
          price={advertisement.price}
          currency={advertisement.currency?.symbol}
        />
      </div>
    </div>
  );
}
