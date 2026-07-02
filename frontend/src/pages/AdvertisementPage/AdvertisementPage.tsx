import { useNavigate, useParams } from "react-router-dom";
import styles from "./AdvertisementPage.module.css";
import DeleteAdvertisementButton from "@/features/delete-advertisement";
import Button from "@/shared/ui/Button";
import { useGetAdvertisement } from "@/entities/Advertisement/hooks/useGetAdvertisement";

export default function AdvertisementPage() {
  const { id } = useParams<{ id: string }>();

  const { data: advertisement, isLoading } = useGetAdvertisement(Number(id));

  const navigate = useNavigate();

  if (isLoading) {
    return <div>Loading...</div>;
  }

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
      <DeleteAdvertisementButton id={advertisement.id} />
      <Button
        onClick={() => {
          navigate(`/update-advertisement/${advertisement.id}`);
        }}
      >
        Змінити
      </Button>
      <p>{advertisement.price}</p>
    </div>
  );
}
