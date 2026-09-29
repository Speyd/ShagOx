import { useParams } from "react-router-dom";
import styles from "./AdvertisementPage.module.css";
import UpdateAdvertisementButton from "@/features/advertisement/update-advertisement/ui/UpdateAdvertisementButton";
import DeleteAdvertisementButton from "@/features/advertisement/delete-advertisement";
import FavoriteButton from "@/features/favorites";
import { useAuthStore } from "@/features/auth";

import Price from "@/shared/ui/price";
import { useGetAdvertisement } from "@/entities/advertisement/model/hooks/useGetAdvertisement";
import { getAdvertisementPrice } from "@/shared/lib/types/advertisements";
import AddToBasket from "@/features/basket/ui/AddToBasket";

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

  const mainImageUrl =
    advertisement.images?.[0]?.url || "/placeholder-image.png";

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
        {advertisement.attributes &&
          Object.keys(advertisement.attributes).length > 0 && (
            <div>
              <h2>Додаткова інформація</h2>
              <ul>
                {Object.entries(advertisement.attributes).map(
                  ([key, value]) => (
                    <li key={key}>
                    <strong>{key}:</strong> {String(value)}
                    </li>
                  ),
                )}
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
          {advertisement.variants[0] && (
            <AddToBasket
              advertisementVariantId={advertisement.variants[0].id}
              quantity={1}
            />
          )}
        </div>
        <Price
          price={getAdvertisementPrice(advertisement)}
          currency={advertisement.currency?.symbol}
        />
      </div>
    </div>
  );
}
