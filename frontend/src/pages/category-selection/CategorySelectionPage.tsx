import CategorySelectionForm from "@/features/category-selection/ui";
import styles from "./CategorySelectionPage.module.css";
import heroAuthBg from "@/shared/assets/images/hero-auth.png";
import { Group, Image, Text } from "@mantine/core";
import { Link } from "react-router-dom";
import logo from "@/shared/assets/images/logo.png";
import { MoveRight } from "lucide-react";

export default function CategorySelectionPage() {
  return (
    <div
      className={styles.categorySelectionPage}
      style={{ backgroundImage: `url(${heroAuthBg})` }}
    >
      <div className={styles.wrapper}>
        <div className={styles.header}>
          <Group gap={5} align="center" wrap="nowrap">
            <Image
              src={logo}
              alt="Marketly"
              w={40}
              h={40}
              fit="contain"
              style={{ flexShrink: 0 }}
            />
            <Text fz={24} fw={600}>
              Marketly
            </Text>
          </Group>

          <Text fz={24} fw={400} c="var(--text-base)">
            Що вас цікавить?
          </Text>

          <div className={styles.description}>
            <Text fw={400} fz={15} c="var(--text-secondary)">
              Оберіть категорії , щоб ми могли підібрати найкращі пропозиції для
              вас
            </Text>
          </div>
        </div>

        <CategorySelectionForm />

        <div className={styles.guestWrapper}>
          <Link to="/" className={styles.guestLink}>
            <Text fw={700} fz={12}>
              Пропустити цей крок
            </Text>

            <MoveRight size={20} />
          </Link>
        </div>
      </div>
    </div>
  );
}
