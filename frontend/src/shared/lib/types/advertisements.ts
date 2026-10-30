export type Advertisement = {
  id: number;
  title: string;
  description: string;
  currency: Currency;
  category: Category;
  seller: UserShort;
  buyer: UserShort | null;
  pictures: AdvertisementImage[];
  attributes: Record<string, unknown>;
  variants: AdvertisementVariant[];
  price?: number;
  previousPrice?: number;
  stock?: number;
  properties?: Record<string, unknown>;
  createdAt: string;
  soldAt: string | null;
};

export type AdvertisementShort = {
  id: number;
  title: string;
  description: string;
  currencyId: number;
  categoryId: number;
  sellerId: number;
  buyerId: number | null;
  pictures: AdvertisementImage[];
  attributes: Record<string, unknown>;
  variants: AdvertisementVariantShort[];
  createdAt: string;
  soldAt: string | null;
};

export type AdvertisementVariant = {
  id: number;
  advertisementId: number;
  price: number;
  previousPrice: number;
  stock: number;
  attributes: VariantAttribute[];
};

export type AdvertisementVariantShort = Omit<
  AdvertisementVariant,
  "attributes"
> & {
  attributes: Record<string, unknown>;
};

export type VariantAttribute = {
  attributeId: number;
  attributeKey: string;
  value: {
    id: number;
    dictionaryId: number;
    code: string;
    value: string | null;
    label: string | null;
  };
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

export type Currency = {
  id: number;
  name: string;
  symbol: string;
};

export type Condition = {
  id: number;
  name: string;
};

export type Category = {
  id: number;
  name: string;
};

export type UserShort = {
  id: number;
  username: string;
  name: string;
};

export type AdvertisementImage = {
  id: number;
  advertisementId: number;
  order: number;
  publicId: string;
  url: string;
};

export function getAdvertisementPrice(
  advertisement:
    | Pick<Advertisement, "variants">
    | Pick<AdvertisementShort, "variants">,
): number {
  return advertisement.variants[0]?.price ?? 0;
}

export function getAdvertisementStock(
  advertisement:
    | Pick<Advertisement, "variants">
    | Pick<AdvertisementShort, "variants">,
): number {
  return advertisement.variants[0]?.stock ?? 0;
}
