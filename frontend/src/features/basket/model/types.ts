import type { ShortAdvertisement } from "@/shared/lib/types/advertisements";
import {
  toDisplayPrice,
  type DisplayPrice,
} from "@/entities/advertisement/lib/price";

export type BasketItemCreateRequest = {
  basketId: number;
  advertisementId: number;
  advertisementVariantId: number | null;
  quantity: number;
};

export type BasketItemUpdateRequest = {
  quantity?: number;
};

export type BasketItem = {
  id: number;
  basketId: number;
  advertisementId: number;
  advertisementVariantId: number | null;
  advertisement: ShortAdvertisement;
  quantity: number;
};

export type Basket = {
  id: number;
  userId: number;
  basketItems: BasketItem[];
};

export type BasketCreateResponse = {
  id: number;
  createdAt: string;
};

export type BasketProductKey = {
  advertisementId: number;
  advertisementVariantId?: number | null;
};

export function isSameBasketProduct(item: BasketItem, key: BasketProductKey) {
  if (key.advertisementVariantId != null) {
    return item.advertisementVariantId === key.advertisementVariantId;
  }

  return (
    item.advertisementId === key.advertisementId &&
    item.advertisementVariantId == null
  );
}

export function getBasketItemVariant(item: BasketItem) {
  if (item.advertisementVariantId == null) return null;

  return (
    item.advertisement?.variants?.find(
      (variant) => variant.id === item.advertisementVariantId,
    ) ?? null
  );
}

export function getBasketItemPrice(item: BasketItem): DisplayPrice {
  const variant = getBasketItemVariant(item);
  const conversion =
    variant?.priceConversion?.price ??
    item.advertisement?.priceConversion?.price;

  return toDisplayPrice(
    variant?.price ?? item.advertisement?.price ?? 0,
    conversion,
  );
}

export function getBasketTotal(items: BasketItem[]): DisplayPrice {
  return items.reduce<DisplayPrice>(
    (total, item) => {
      const price = getBasketItemPrice(item);
      return {
        amount: Math.round((total.amount + price.amount * item.quantity) * 100) / 100,
        symbol: total.symbol ?? price.symbol,
      };
    },
    { amount: 0, symbol: undefined },
  );
}

export function getBasketItemStock(item: BasketItem): number | null {
  if (item.advertisementVariantId == null) return null;
  return getBasketItemVariant(item)?.stock ?? 0;
}
