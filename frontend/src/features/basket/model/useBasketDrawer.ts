import { useContext } from "react";
import { basketDrawerContext } from "./basketDrawerContext";

export function useBasketDrawer() {
  return useContext(basketDrawerContext);
}
