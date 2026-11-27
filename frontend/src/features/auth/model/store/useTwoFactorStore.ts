import { create } from "zustand";
import { persist } from "zustand/middleware";

interface TwoFactorState {
  statuses: Record<number, boolean>;
  setStatus: (userId: number, enabled: boolean) => void;
}

export const useTwoFactorStore = create<TwoFactorState>()(
  persist(
    (set) => ({
      statuses: {},
      setStatus: (userId, enabled) =>
        set((state) => ({
          statuses: { ...state.statuses, [userId]: enabled },
        })),
    }),
    { name: "twoFactorStatus" },
  ),
);

export function useTwoFactorStatus(userId: number | undefined) {
  return useTwoFactorStore((state) =>
    userId == null ? undefined : state.statuses[userId],
  );
}
