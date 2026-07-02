export interface CreateAdvertisementDto {
  title: string;
  description: string;
  price: number;
  previousPrice: number;
  currencyId: number;
  sellerId: number;
  categoryId: number;
  properties: Record<string, string>;
  images: string[];
}

