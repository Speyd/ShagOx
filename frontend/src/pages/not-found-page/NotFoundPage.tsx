import { Image, Text } from "@mantine/core";
import { Link } from "react-router-dom";
import image from "@/shared/assets/images/not-found-page.svg";
import Button from "@/shared/ui/button";
import styles from "./NotFoundPage.module.css";

export default function NotFoundPage() {
  return (
    <main className={styles.notFoundPage}>
      <div className={styles.notFoundCard}>
        <div className={styles.content}>
          <div className={styles.textContainer}>
            <Text fz={35} fw={700}>
              Ой-ой...
            </Text>

            <Text fz={24} >
              Сторінку не знайдено
            </Text>

            <Text fz={16} c="dimmed">
              Цієї сторінки не існує або її було видалено.
              <br />
              Радимо повернутися на головну сторінку.
            </Text>
          </div>

          <Link to="/" className={styles.homeLink}>
            <Button>Перейти на головну</Button>
          </Link>
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
