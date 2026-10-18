import { CatalogSidebar, FeaturedAdvertisements } from "@/widgets/catalog";
import styles from "./CatalogMainPage.module.css";

export default function CatalogMainPage() {
  return (
    <section className={styles.catalogMainPage}>
      <CatalogSidebar />
      <FeaturedAdvertisements />
    </section>
  );
}
