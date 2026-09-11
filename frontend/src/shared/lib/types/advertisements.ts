export type Advertisement = {
  id: number;
  title: string;
  description: string;

  price: number;
  previousPrice: number;

  currency: Currency;
  category: Category;

  seller: UserShort;
  buyer: UserShort | null;

  images: AdvertisementImage[];
  properties: Record<string, string>;
  condition: Condition;
  createdAt: string;
  soldAt: string | null;
  inStock: number;
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
