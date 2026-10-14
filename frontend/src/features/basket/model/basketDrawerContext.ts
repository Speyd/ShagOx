import { createContext } from "react";

export type BasketDrawerContextValue = {
  closeBasket: () => void;
};

export const basketDrawerContext =
  createContext<BasketDrawerContextValue>({
    closeBasket: () => undefined,
  });
