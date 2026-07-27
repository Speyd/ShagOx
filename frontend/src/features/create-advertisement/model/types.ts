export type CreateAdvertisementDto = {
  title: string;
  description: string;
  price: number;
  previousPrice: number;
  currencyId: number;
  sellerId: number;
  categoryId: number;
  conditionId: number;
  properties: Record<string, string>;
  images: File[];
};
