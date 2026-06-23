export interface Advertisement {
  id: number;
  title: string;
  category: string;
  description: string;
  condition: string;
  popularity: number;
  price: number;
  previousPrice: number | null;
  currency: string;
  images: string[];
  seller: number;
  buyer: number | null;
  createdAt: string;
  soldAt: string | null;
}
