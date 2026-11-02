import { Link } from "react-router-dom";
import { Image, Text } from "@mantine/core";
import styles from "./CategoryCard.module.css";

type CategoryCardProps = {
  code: string;
  label: string;
  imageUrl: string;
};

export function CategoryCard({
  code,
  label,
  imageUrl,
}: CategoryCardProps) {
  return (
    <Link to={`/catalog/category/${code}`} className={styles.categoryCard}>
      <div className={styles.imageWrapper}>
        <Image
          src={imageUrl}
          alt={label}
          fit="contain"
          className={styles.image}
        />
      </div>
      <Text fw={500} className={styles.label}>
        {label}
      </Text>
    </Link>
  );
}
