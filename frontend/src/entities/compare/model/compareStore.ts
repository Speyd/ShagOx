import type { Advertisement } from "@/shared/lib/types/advertisements";
import { create } from "zustand";
import { persist } from "zustand/middleware";

interface CompareState {
  compareList: Advertisement[];
  addAdvertisement: (product: Advertisement) => void;
  removeAdvertisement: (productId: string | number) => void;
  clearCompare: () => void;
}

export const useCompareStore = create<CompareState>()(
  persist(
    (set) => ({
      compareList: [],

      addAdvertisement: (product) =>
        set((state) => {
          if (state.compareList.find((p) => p.id === product.id)) {
            return state;
          }
          return { compareList: [...state.compareList, product] };
        }),

      removeAdvertisement: (productId) =>
        set((state) => ({
          compareList: state.compareList.filter((p) => p.id !== productId),
        })),

      clearCompare: () => set({ compareList: [] }),
    }),
    {
      name: "compare-storage",
    },
  ),
);
