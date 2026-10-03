import type { AdvertisementShort } from "@/shared/lib/types/advertisements";

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
  advertisement: BasketAdvertisement;
  quantity: number;
};

export type BasketAdvertisement = AdvertisementShort;

export type Basket = {
  id: number;
  userId: number;
  basketItems: BasketItem[];
};

export type BasketCreateResponse = {
  id: number;
  createdAt: string;
};
