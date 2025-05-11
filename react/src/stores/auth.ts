import { create } from "zustand";
import { devtools, persist } from "zustand/middleware";

export interface User {
  token: string;
}

export type AuthState = {
  isAuthenticated: boolean;
  user: User | null;
};

export type AuthAction = {
  login: (user: User) => void;
  logout: () => void;
};

export const useAuthStore = create(
  devtools(
    persist<AuthState & AuthAction>(
      (set) => ({
        isAuthenticated: false,
        user: null,
        login: (user) => set({ isAuthenticated: true, user }),
        logout: () => set({ isAuthenticated: false, user: null }),
      }),
      {
        name: "auth-storage",
      }
    ),
    { name: "authStore" }
  )
);
