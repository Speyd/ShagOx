import { create } from "zustand";
import { persist } from "zustand/middleware";

/** Скільки товарів однієї категорії можна порівнювати одночасно. */
export const MAX_COMPARE_PER_CATEGORY = 4;

export type CompareItem = { id: number; categoryId: number };

interface CompareState {
  items: CompareItem[];
  /** Повертає false, якщо досягнуто ліміту для категорії. */
  add: (item: CompareItem) => boolean;
  remove: (id: number) => void;
  clearCategory: (categoryId: number) => void;
}

/**
 * Зберігаємо лише id та категорію: актуальні дані товару (ціна, наявність)
 * завантажуються з API при відкритті порівняння.
 */
export const useCompareStore = create<CompareState>()(
  persist(
    (set, get) => ({
      items: [],

      add: (item) => {
        const { items } = get();
        if (items.some((i) => i.id === item.id)) return true;

        const inCategory = items.filter((i) => i.categoryId === item.categoryId);
        if (inCategory.length >= MAX_COMPARE_PER_CATEGORY) return false;

        set({ items: [...items, item] });
        return true;
      },

      remove: (id) => set({ items: get().items.filter((i) => i.id !== id) }),

      clearCategory: (categoryId) =>
        set({ items: get().items.filter((i) => i.categoryId !== categoryId) }),
    }),
    {
      name: "compare-storage",
      version: 1,
      // v0 зберігав цілі оголошення в compareList
      migrate: (persisted, version) => {
        if (version === 0) {
          const old = (persisted as { compareList?: { id: number; category?: { id: number } }[] })
            .compareList ?? [];
          return { items: old.map((a) => ({ id: a.id, categoryId: a.category?.id ?? 0 })) };
        }
        return persisted as CompareState;
      },
    },
  ),
);

export const useIsInCompare = (id: number) =>
  useCompareStore((state) => state.items.some((i) => i.id === id));
