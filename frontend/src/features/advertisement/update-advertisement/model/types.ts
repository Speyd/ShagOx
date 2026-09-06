export type AdvertisementImageUpdate = {
  id?: number;
  file?: File;
  order: number;
  isDeleted: boolean;
};

export type UpdateAdvertisementRequestDto = {
  title?: string;
  description?: string;
  price?: number;
  properties?: Record<string, string>;
  images?: AdvertisementImageUpdate[];
};
