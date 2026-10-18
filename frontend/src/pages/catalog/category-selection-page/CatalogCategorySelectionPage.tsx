import { CategoriesGrid } from "@/widgets/catalog";
import styles from "./CatalogCategorySelectionPage.module.css";
import { useParams } from "react-router-dom";

export default function CatalogCategorySelectionPage() {
  const { productTypeId } = useParams<{ productTypeId: string }>();
  
  console.log(productTypeId);
  const selectedProductTypeId = productTypeId
    ? Number(productTypeId)
    : undefined;

  return (
    <section className={styles.catalogCategorySelectionPage}>
      <CategoriesGrid selectedProductTypeId={selectedProductTypeId} />
    </section>
  );
}
