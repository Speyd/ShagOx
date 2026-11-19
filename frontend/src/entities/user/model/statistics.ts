export type MoneyAmount = {
  currencyCode: string;
  symbol: string;
  amount: number;
};

export type GradeCount = {
  grade: number;
  count: number;
};

export type TopAdvertisement = {
  id: number;
  title: string;
  pictureUrl: string | null;
  soldUnits: number;
  favorites: number;
  reviews: number;
  averageGrade: number | null;
};

export type SellerStatistics = {
  activeAdvertisements: number;
  outOfStockAdvertisements: number;
  unitsInStock: number;
  reviewsCount: number;
  averageGrade: number | null;
  grades: GradeCount[];
  favoritesCount: number;
  salesCount: number;
  soldUnits: number;
  revenue: MoneyAmount[];
  topAdvertisements: TopAdvertisement[];
};

export type BuyerStatistics = {
  ordersCount: number;
  activeOrdersCount: number;
  spent: MoneyAmount[];
  favoritesCount: number;
  reviewsWritten: number;
};

export type UserStatistics = {
  seller: SellerStatistics;
  buyer: BuyerStatistics;
};
