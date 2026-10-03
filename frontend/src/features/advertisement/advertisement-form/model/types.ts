export type AdvertisementDto = {
  title: string;
  description: string;
  popularity: number;
  currencyId: number;
  conditionId: number;
  categoryId: number;
  attributes?: Record<string, unknown>;
  variants?: AdvertisementVariantCreate[];
  images: File[];
};

export type AdvertisementVariantCreate = {
  price: number;
  previousPrice: number;
  stock: number;
  attributes: Record<string, unknown>;
};
