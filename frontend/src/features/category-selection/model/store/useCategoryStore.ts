import { create } from "zustand";
import { persist } from "zustand/middleware";

interface CategoryState {
  selectedCategoryIds: string[];
  setSelectedCategories: (ids: string[]) => void;
  clearCategories: () => void;
}

export const useCategoryStore = create<CategoryState>()(
  persist(
    (set) => ({
      selectedCategoryIds: [],
      setSelectedCategories: (ids) => set({ selectedCategoryIds: ids }),
      clearCategories: () => set({ selectedCategoryIds: [] }),
    }),
    {
      name: "user-selected-categories", // Зберігає у localStorage автоматично
    },
  ),
);
