import styles from "./CategoriesGrid.module.css";
import { NavLink } from "react-router-dom";
import { useGetCategories } from "@/entities/advertisement/model/hooks/useGetCategories";
import { Text } from "@mantine/core";

type CatalogGridProps = {
  selectedProductTypeId?: number;
};

export default function CategoriesGrid({
  selectedProductTypeId,
}: CatalogGridProps) {
  const { data: categories, isLoading: isLoadingCategories } = useGetCategories(
    selectedProductTypeId,
  );

  if (!selectedProductTypeId) {
    return (
      <div className={styles.placeholder}>
        <Text c="dimmed">Оберіть тип товару зі списку ліворуч</Text>
      </div>
    );
  }

  if (isLoadingCategories) {
    return <Text>Завантаження...</Text>;
  }

  return (
    <section className={styles.categoriesGrid}>
      {categories?.map((category) => (
        <NavLink key={category.id} to={`/catalog/${category.name}`}>
          {category.name}
        </NavLink>
      ))}
    </section>
  );
}
