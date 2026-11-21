import { Image, Text } from "@mantine/core";
import { useNavigate } from "react-router-dom";
import image from "@/shared/assets/images/not-found-page.svg";
import Button from "@/shared/ui/buttons/button";
import styles from "./NotFoundPage.module.css";

export default function NotFoundPage() {
  const navigate = useNavigate();

  return (
    <main className={styles.notFoundPage}>
      <div className={styles.notFoundCard}>
        <div className={styles.content}>
          <div className={styles.textContainer}>
            <Text className={styles.title}>Ой-ой...</Text>
            <Text className={styles.subtitle}>Сторінку не знайдено</Text>
            <Text className={styles.description} c="dimmed">
              Цієї сторінки не існує або її було видалено.
              <br />
              Радимо повернутися на головну сторінку.
            </Text>
          </div>

          <Button onClick={() => navigate("/")} className={styles.homeButton}>
            Перейти на головну
          </Button>
        </div>

        <div className={styles.imageContainer}>
          <Image
            src={image}
            alt="Сторінку не знайдено"
            className={styles.image}
          />
        </div>
      </div>
    </main>
  );
}
