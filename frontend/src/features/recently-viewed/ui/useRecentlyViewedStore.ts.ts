import type { Advertisement } from "@/shared/lib/types/advertisements";
import { create } from "zustand";
import { persist, createJSONStorage } from "zustand/middleware";

interface RecentlyViewedState {
  items: Advertisement[];
  addProduct: (product: Advertisement) => void;
  clearAll: () => void;
}

const MAX_ITEMS = 20;

export const useRecentlyViewedStore = create<RecentlyViewedState>()(
  persist(
    (set) => ({
      items: [],

      addProduct: (product) =>
        set((state) => {
          const filteredItems = state.items.filter(
            (item) => item.id !== product.id,
          );
          const updatedItems = [product, ...filteredItems].slice(0, MAX_ITEMS);

          return { items: updatedItems };
        }),

      clearAll: () => set({ items: [] }),
    }),
    {
      name: "recently-viewed-storage",
      storage: createJSONStorage(() => localStorage),
    },
  ),
);
