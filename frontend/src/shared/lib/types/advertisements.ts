import type { Category } from "./category";

export type Currency = {
  id: number;
  code: string;
  symbol: string;
  name: string;
};

export type UserSeller = {
  id: number;
  firstName: string;
  lastName: string;
  userName: string;
  bio: string | null;
  phone: string | null;
  email: string;
  avatarId: string | null;
  cityId: number | null;
  cityCode: string;
  lastSeenAt: string;
};

export type AdvertisementImage = {
  id: number;
  order: number;
  advertisementId: number;
  url: string;
  publicId: string;
};

export type AttributeDefinition = {
  id: number;
  categoryId: number;
  categoryCode: string;
  key: string;
  dictionaryId: number | null;
  type: string;
  required: boolean;
  min: number | null;
  max: number | null;
  isVariant: boolean;
  multiple: boolean;
  label: string;
};

export type AttributeValue = {
  id: number;
  dictionaryId: number;
  code: string;
  value: string;
  label: string;
};

export type VariantAttribute = {
  attribute: AttributeDefinition;
  values: AttributeValue[];
};

export type AdvertisementVariant = {
  id: number;
  advertisementId: number;
  price: number;
  previousPrice: number;
  stock: number;
  attributes: VariantAttribute[];
};

export type Advertisement = {
  id: number;
  title: string;
  description: string;
  status: {
    id: number;
    code: string;
    lable: string | null;
  };
  priceConversion: ConvertedPrices | null;
  currency: Currency;
  category: Category;
  seller: UserSeller;
  buyer: UserSeller | null;
  pictures: AdvertisementImage[];
  attributes: VariantAttribute[];
  variants: AdvertisementVariant[];
  soldAt: string | null;
  createdAt: string;
};

export type ShortAdvertisementVariant = {
  id: number;
  advertisementId: number;
  price: number;
  previousPrice: number;
  priceConversion: ConvertedPrices;
  stock: number;
  attributes: Record<string, unknown>;
};

export type ShortAdvertisement = {
  id: number;
  title: string;
  description: string;
  price: number | null;
  previousPrice: number | null;
  priceConversion: ConvertedPrices;
  currencyId: number;
  categoryId: number;
  sellerId: number;
  buyerId: number | null;
  status: Status;
  picture: AdvertisementImage | null;
  attributes: Record<string, unknown>;
  variants: ShortAdvertisementVariant[];
  soldAt: string | null;
  createdAt: string;
};

export type CatalogAdvertisement = {
  id: number;
  title: string;
  description: string;
  price: number | null;
  previousPrice: number | null;
  priceConversion: ConvertedPrices;
  currencyId: number;
  categoryId: number;
  picture: AdvertisementImage | null;
  attributes: VariantAttribute[];
  variants: AdvertisementVariant[];
  reviewCount: number;
  averageRatings: number;
};

export type ConvertedPrices = {
  price: CurrencyConvert;
  previousPrice: CurrencyConvert;
};

export type CurrencyConvert = {
  from: CurrencyShort;
  to: CurrencyShort | null;
  convertPrice: number | null;
  coefficient: number;
};

export type CurrencyShort = {
  code: string;
  symbol: string;
  name: string;
};

export type Status = {
  id: number;
  code: string;
  label: string | null;
};

export function formatAttributeValue(value: unknown): string {
  if (value == null) return "";

  if (
    typeof value === "string" ||
    typeof value === "number" ||
    typeof value === "boolean"
  ) {
    return String(value);
  }

  if (Array.isArray(value)) {
    return value.map(formatAttributeValue).filter(Boolean).join(", ");
  }

  if (typeof value === "object") {
    const record = value as Record<string, unknown>;
    for (const key of ["label", "value", "code"]) {
      if (record[key] != null) return formatAttributeValue(record[key]);
    }

    return JSON.stringify(value) ?? "";
  }

  return String(value);
}

export function getAdvertisementPrice(
  advertisement: Pick<Advertisement, "variants">,
): number {
  return advertisement.variants[0]?.price ?? 0;
}
