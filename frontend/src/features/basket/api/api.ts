import { api } from "@/shared/api/api";
import axios from "axios";
import type {
  Basket,
  BasketCreateResponse,
  BasketItemCreateRequest,
  BasketItemUpdateRequest,
} from "../model/types";

export async function getBasket(): Promise<Basket | null> {
  const response = await api.get("/basket-items", {
    params: { page: 1, pageSize: 100 },
  });
  const items = response.data.items ?? [];

  if (items.length > 0) {
    return {
      id: items[0].basketId,
      userId: 0,
      basketItems: items,
    };
  }

  try {
    const emptyBasketResponse = await api.get("/baskets");
    return emptyBasketResponse.data;
  } catch (error) {
    if (axios.isAxiosError(error) && error.response?.status === 404) {
      return null;
    }

    throw error;
  }
}

export async function createBasket(): Promise<BasketCreateResponse> {
  const response = await api.post("/baskets");
  return response.data;
}

export async function createItem(data: BasketItemCreateRequest) {
  await api.post("/basket-items", data);
}

export async function updateItem(id: number, data: BasketItemUpdateRequest) {
  await api.put(`/basket-items/${id}`, data);
}

export async function deleteItem(id: number) {
  await api.delete(`/basket-items/${id}`);
}
