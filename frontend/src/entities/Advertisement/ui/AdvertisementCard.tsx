import styles from "./AdvertisementCard.module.css";
import Price from "@/shared/ui/price";
import type { Advertisement } from "@/shared/lib/types/advertisements";
import FavoriteButton from "@/features/favorites";
import { Button, Image, Text } from "@mantine/core";
import { Plus, Star } from "lucide-react";
import { Link } from "react-router-dom";

export default function AdvertisementCard(props: Advertisement) {
  const imageUrl = props.images?.[0]?.url || "/placeholder-image.png";

  return (
    <div className={styles.card}>
      <Link to={`/advertisement/${props.id}`} style={{ textDecoration: "none", color: "inherit" }}>
        <Image
          src={imageUrl}
          alt={props.title}
          radius="md"
          bg="white"
          h={300}
          w="100%"
          fit="cover"
        />
      </Link>
      <div className={styles.info}>
        <Link to={`/advertisement/${props.id}`} style={{ textDecoration: "none", color: "inherit" }}>
          <Text fw={600} size="md">{props.title}</Text>
        </Link>

        <Text
          c="dimmed"
          size="sm"
          className={styles.description}
        >
          {props.description}
        </Text>

        <div className={styles.ratingContainer}>
          <div className={styles.rating}>
            <Star size={16} fill="#fea945" color="#fea945" />
            <Text size="sm" c="dimmed">4.8</Text>
            <Text size="sm" c="dimmed">(12 відгуків)</Text>
          </div>
          {props.inStock ? (
            <Text c="green" fw={600} size="xs">В наявності</Text>
          ) : (
            <Text c="red" fw={600} size="xs">Не в наявності</Text>
          )}
        </div>

        <Price price={props.price} currency={props.currency?.symbol} />

        <div className={styles.actionButtons}>
          <Button leftSection={<Plus size={16} />} bg="var(--color-base)" bdrs={4} h={44}
            px={16} className={styles.addButton}>Додати в кошик</Button>
          <FavoriteButton advertisementId={props.id} />
        </div>

      </div>
    </div>
  );
}
