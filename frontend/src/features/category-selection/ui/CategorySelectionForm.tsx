import { useState } from "react";
import { Image, Text } from "@mantine/core";
import camera from "@/shared/assets/icons/camera.png";
import headphones from "@/shared/assets/icons/headphones.png";
import laptop from "@/shared/assets/icons/laptop.png";
import smartphone from "@/shared/assets/icons/mobile-phone.png";
import smartHome from "@/shared/assets/icons/smart-home.png";
import smartwatch from "@/shared/assets/icons/smartwatch.png";
import Button from "@/shared/ui/button";
import { useCategoryStore } from "../model/store/useCategoryStore";
import styles from "./CategorySelectionForm.module.css";

interface CategorySelectionFormProps {
  onSubmit?: (selectedIds: string[]) => void;
  onSkip?: () => void;
}

const categories = [
  { id: "laptops", title: "Ноутбуки", icon: laptop },
  { id: "smartphones", title: "Смартфони", icon: smartphone },
  { id: "audio", title: "Аудіо", icon: headphones },
  { id: "accessories", title: "Аксесуари", icon: smartwatch },
  { id: "smart-home", title: "Розумний дім", icon: smartHome },
  { id: "cameras", title: "Камери", icon: camera },
];

export default function CategorySelectionForm({
  onSubmit,
  onSkip,
}: CategorySelectionFormProps) {
  const savedCategories = useCategoryStore(
    (state) => state.selectedCategoryIds,
  );
  const setSelectedCategoriesStore = useCategoryStore(
    (state) => state.setSelectedCategories,
  );

  const [selectedCategories, setSelectedCategories] =
    useState<string[]>(savedCategories);

  const toggleCategory = (id: string) => {
    setSelectedCategories((prev) =>
      prev.includes(id) ? prev.filter((catId) => catId !== id) : [...prev, id],
    );
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();

    setSelectedCategoriesStore(selectedCategories);

    if (selectedCategories.length === 0 && onSkip) {
      onSkip();
      return;
    }

    onSubmit?.(selectedCategories);
  };

  return (
    <form onSubmit={handleSubmit} className={styles.form}>
      <div className={styles.categoriesGrid}>
        {categories.map((category) => {
          const isSelected = selectedCategories.includes(category.id);

          return (
            <button
              key={category.id}
              type="button"
              role="checkbox"
              aria-checked={isSelected}
              onClick={() => toggleCategory(category.id)}
              className={`${styles.category} ${
                isSelected ? styles.categorySelected : ""
              }`}
            >
              <Image
                src={category.icon}
                alt={category.title}
                className={styles.icon}
                w={50}
                h={50}
              />

              <Text
                className={styles.title}
                fw={isSelected ? 600 : 400}
                fz={12}
              >
                {category.title}
              </Text>
            </button>
          );
        })}
      </div>

      <div className={styles.actions}>
        <Button type="submit" className={styles.submitBtn}>
          <div className={styles.btnContainer}>
            <Text fw={500} fz={15}>
              Продовжити
            </Text>

            {selectedCategories.length > 0 && (
              <div className={styles.selectedCount}>
                <Text fw={400} fz={13}>
                  ({selectedCategories.length})
                </Text>
              </div>
            )}
          </div>
        </Button>

        {onSkip && (
          <button type="button" onClick={onSkip} className={styles.skipBtn}>
            <Text fz={13} c="dimmed">
              Пропустити цей крок
            </Text>
          </button>
        )}
      </div>
    </form>
  );
}
