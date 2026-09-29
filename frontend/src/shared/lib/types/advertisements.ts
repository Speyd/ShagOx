export type Advertisement = {
  id: number;
  title: string;
  description: string;
  currency: Currency;
  category: Category;
  seller: UserShort;
  buyer: UserShort | null;
  images: AdvertisementImage[];
  attributes: Record<string, unknown>;
  variants: AdvertisementVariant[];
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
  imageIds: number[];
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
  valueId: number;
  valueCode: string;
  value: string | null;
  valueLabel: string;
};

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
  advertisement: Pick<Advertisement, "variants"> | Pick<AdvertisementShort, "variants">,
): number {
  return advertisement.variants[0]?.price ?? 0;
}

export function getAdvertisementStock(
  advertisement: Pick<Advertisement, "variants"> | Pick<AdvertisementShort, "variants">,
): number {
  return advertisement.variants[0]?.stock ?? 0;
}
