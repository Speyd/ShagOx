import { AdvertisementsGrid, CatalogFilters } from "@/widgets/catalog";
import styles from "./CategoryAdvertisementsPage.module.css";

export default function CategoryAdvertisementsPage() {
  return (
    <section className={styles.categoryAdvertisementsPage}>
      <CatalogFilters />
      <AdvertisementsGrid />
    </section>
  );
}
