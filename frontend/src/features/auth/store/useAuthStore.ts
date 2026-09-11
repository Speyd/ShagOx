import type { User } from "../model/types";
import { create } from "zustand";

interface AuthState {
  user: User | null;
  isInitialized: boolean;
  setUser: (user: User | null) => void;
  logout: () => void;
}

export const useAuthStore = create<AuthState>((set) => ({
  user: null,
  isInitialized: false,

  setUser: (user) => set({ user, isInitialized: true }),

  logout: () => set({ user: null, isInitialized: true }),
}));
