import { Skeleton } from "@mantine/core";
import styles from "./CategoryCardSkeleton.module.css";

export default function CategoryCardSkeleton() {
  return (
    <div className={styles.skeletonWrapper}>
      <Skeleton height={120} mb="sm" radius="md" />

      <Skeleton height={20} width="70%" mx="auto" radius="xl" />
    </div>
  );
}
