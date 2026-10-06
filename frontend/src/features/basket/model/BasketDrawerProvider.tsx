import type { ReactNode } from "react";
import { basketDrawerContext } from "./basketDrawerContext";

type BasketDrawerProviderProps = {
  closeBasket: () => void;
  children: ReactNode;
};

export function BasketDrawerProvider({
  closeBasket,
  children,
}: BasketDrawerProviderProps) {
  return (
    <basketDrawerContext.Provider value={{ closeBasket }}>
      {children}
    </basketDrawerContext.Provider>
  );
}
