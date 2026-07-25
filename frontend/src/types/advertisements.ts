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

  images: ImageItem[];
  properties: Record<string, string>;

  createdAt: string;
  soldAt: string | null;
};

export type Currency = {
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
};

export type ImageItem = {
  id: number;
  imageId?: number;
  url: string;
  file?: File;
};
