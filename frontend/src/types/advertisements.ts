export interface Advertisement {
  id: number;
  title: string;
  category: string;
  properties: Record<string, any>;
  description: string;
  condition: string;
  popularity: number;
  price: number;
  previousPrice: number;
  currency: string;
  images: ImageItem[];
  seller: number;
  buyer: number | null;
  createdAt: string;
  soldAt: string | null;
}

export type ImageItem = {
  id: string;
  imageId?: number;
  url: string;
  file?: File;
};
