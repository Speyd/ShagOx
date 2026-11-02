import styles from "./CategoriesGrid.module.css";
import { useNavigate, useParams } from "react-router-dom";
import { useGetCategories } from "@/entities/advertisement/model/hooks/useGetCategories";
import { useGetProductTypes } from "@/entities/advertisement/model/hooks/useGetProductTypes";
import { Text } from "@mantine/core";
import { getCategoryImage } from "@/shared/lib/categoryImages";
import { EmptyState } from "@/shared/ui/data-display/empty-state/EmptyState";
import { CategoryCard, CategoryCardSkeleton } from "@/entities/category";

type CatalogGridProps = {
  selectedProductTypeId?: number | string;
};

export function CategoriesGrid({
  selectedProductTypeId: propsProductTypeId,
}: CatalogGridProps) {
  const { productTypeCode: paramProductType } = useParams<{
    productTypeCode?: string;
  }>();

  const targetType = propsProductTypeId ?? paramProductType;
  const navigate = useNavigate();
  const { data: productTypes } = useGetProductTypes();

  const numericProductTypeId = (() => {
    if (!targetType) return undefined;
    if (!isNaN(Number(targetType))) return Number(targetType);

    const foundType = productTypes?.find(
      (type) =>
        type.name.toLowerCase() === String(targetType).toLowerCase() ||
        type.label.toLowerCase() === String(targetType).toLowerCase(),
    );

    return foundType?.id;
  })();

  const { data: categories, isLoading: isLoadingCategories } =
    useGetCategories(numericProductTypeId);

  if (!targetType) {
    return (
      <div className={styles.placeholder}>
        <Text c="dimmed">Оберіть тип товару зі списку ліворуч</Text>
      </div>
    );
  }

  if (isLoadingCategories) {
    return (
      <section className={styles.categoriesGrid}>
        {Array.from({ length: 8 }).map((_, index) => (
          <CategoryCardSkeleton key={index} />
        ))}
      </section>
    );
  }

  const categoriesWithImages = categories?.map((category) => ({
    ...category,
    imageUrl: getCategoryImage(category.code),
  }));

  return (
    <section className={styles.categoriesGrid}>
      {categoriesWithImages?.length ? (
        categoriesWithImages.map((category) => (
          <CategoryCard
            key={category.id}
            code={category.code}
            label={category.label}
            imageUrl={category.imageUrl}
          />
        ))
      ) : (
        <EmptyState
          label="У цьому розділі немає категорій"
          description="Зачекайте, можливо скоро з'являться, або додайте першим!"
          buttonLabel="Додати оголошення"
          onButtonClick={() => navigate("/advertisements/create-advertisement")}
        />
      )}
    </section>
  );
}
