export interface CreateAdvertisementDto {
  title: string;
  description: string;
  price: number;
  previousPrice: number;
  currencyId: number;
  categoryId: number;
  properties: Record<string, string>;
  images: string[];
}
