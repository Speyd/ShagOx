import { CategoriesGrid } from "@/widgets/catalog";
import styles from "./CatalogCategorySelectionPage.module.css";
import { useParams } from "react-router-dom";
import RecentlyViewedSection from "@/widgets/recently-viewed-list";

export default function CatalogCategorySelectionPage() {
  const { productTypeId } = useParams<{ productTypeId: string }>();

  const selectedProductTypeId = productTypeId
    ? Number(productTypeId)
    : undefined;

  return (
    <section className={styles.catalogCategorySelectionPage}>
      <div className={styles.categoriesWrapper}>
        <CategoriesGrid selectedProductTypeId={selectedProductTypeId} />
      </div>
      <RecentlyViewedSection />
    </section>
  );
}
