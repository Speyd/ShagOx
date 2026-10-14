import type { Advertisement } from "@/shared/lib/types/advertisements";

export type BasketItemCreateRequest = {
  basketId: number;
  advertisementVariantId: number;
  quantity: number;
};

export type BasketItemUpdateRequest = {
  quantity?: number;
};

export type BasketItem = {
  id: number;
  basketId: number;
  advertisementVariantId: number;
  advertisement: BasketAdvertisement;
  quantity: number;
};

export type BasketAdvertisement = Advertisement;

export type Basket = {
  id: number;
  userId: number;
  basketItems: BasketItem[];
};

export type BasketCreateResponse = {
  id: number;
  createdAt: string;
};

export function getBasketItemVariant(item: BasketItem) {
  if (!item.advertisement?.variants?.length) return null;

  if (item.advertisementVariantId) {
    const found = item.advertisement.variants.find(
      (variant) => variant.id === item.advertisementVariantId,
    );
    if (found) return found;
  }

  return item.advertisement.variants[0];
}

export function getBasketItemPrice(item: BasketItem) {
  return getBasketItemVariant(item)?.price ?? 0;
}

export function getBasketItemStock(item: BasketItem) {
  return getBasketItemVariant(item)?.stock ?? 0;
}
