export interface AdvertisementProperties {
  [key: string]: string | number | boolean;
}

export interface Advertisement {
  id: number;
  title: string;
  description: string;
  images: string[];
  popularity: number;
  price: number;
  previousPrice: number;
  currencyId: number;
  categoryId: number;
  sellerId: number;
  buyerId: number | null;
  soldAt: string | null;
  createdAt: string;
  properties: AdvertisementProperties;
}
