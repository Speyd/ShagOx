export type UpdateAdvertisementRequest = {
  id: number;
  data: UpdateAdvertisementRequestDto;
};

export type UpdateAdvertisementRequestDto = {
  title?: string;
  description?: string;
  popularity?: number;
  currencyId?: number;
  conditionId?: number;
  categoryId?: number;
  buyerId?: number;
  attributes?: Record<string, unknown>;
  variants?: Record<number, {
    price?: number;
    stock?: number;
    attributes?: Record<string, unknown>;
  }>;
  images: {
    id?: number;
    file?: File;
    order: number;
    isDeleted: boolean;
  }[];
};
