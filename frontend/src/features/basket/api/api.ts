import { api } from "@/shared/api/api";
import axios from "axios";
import type {
  Basket,
  BasketCreateResponse,
  BasketItem,
  BasketItemCreateRequest,
  BasketItemUpdateRequest,
} from "../model/types";
import type { PaginatedResponse } from "@/shared/lib/types/paginatedResponse";

export async function getBasket(): Promise<Basket | null> {
  try {
    const basketResponse = await api.get<Basket>("/baskets");
    const basket = basketResponse.data;

    if (
      basket.basketItems.every((item) => item.advertisement != null)
    ) {
      return basket;
    }

    try {
      const firstPage = await api.get<PaginatedResponse<BasketItem>>(
        "/basket-items",
        { params: { page: 1, pageSize: 100 } },
      );
      const pages = [firstPage.data];

      for (let page = 2; page <= firstPage.data.totalPages; page += 1) {
        const response = await api.get<PaginatedResponse<BasketItem>>(
          "/basket-items",
          { params: { page, pageSize: 100 } },
        );
        pages.push(response.data);
      }

      return {
        ...basket,
        basketItems: pages.flatMap((page) => page.items),
      };
    } catch (error) {
      if (axios.isAxiosError(error) && error.response?.status === 404) {
        return { ...basket, basketItems: [] };
      }

      throw error;
    }
  } catch (error) {
    if (axios.isAxiosError(error) && error.response?.status === 404) {
      return null;
    }

    throw error;
  }
}

export async function createBasket(): Promise<BasketCreateResponse> {
  try {
    const response = await api.post("/baskets");
    return response.data;
  } catch (error) {
    if (axios.isAxiosError(error) && error.response?.status === 409) {
      const basket = await getBasket();
      if (basket) {
        return { id: basket.id, createdAt: new Date().toISOString() };
      }
    }

    throw error;
  }
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