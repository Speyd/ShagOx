import styles from "./CategoriesGrid.module.css";
import { NavLink, useNavigate, useParams } from "react-router-dom";
import { useGetCategories } from "@/entities/advertisement/model/hooks/useGetCategories";
import { useGetProductTypes } from "@/entities/advertisement/model/hooks/useGetProductTypes";
import { Image, Text } from "@mantine/core";
import { getCategoryImage } from "@/shared/lib/categoryImages";
import EmptyState from "@/shared/ui/empty-state/EmptyState";
import { CategoryCardSkeleton } from "@/entities/category";

type CatalogGridProps = {
  selectedProductTypeId?: number | string;
};

export default function CategoriesGrid({
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
          <NavLink
            key={category.id}
            to={`/catalog/category/${category.code}`}
            className={styles.categoryCard}
          >
            <div className={styles.imageWrapper}>
              <Image
                src={category.imageUrl}
                alt={category.label}
                fit="contain"
                h={120}
              />
            </div>
            <Text fw={500} size="sm" fz={16} className={styles.label}>
              {category.label}
            </Text>
          </NavLink>
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
