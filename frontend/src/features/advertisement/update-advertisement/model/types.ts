export type AdvertisementPictureUpdate = {
  id?: number;
  file?: File;
  order: number;
  isDeleted: boolean;
};

export type AdvertisementVariantUpdate = {
  price: number;
  stock: number;
};

export type UpdateAdvertisementRequestDto = {
  title?: string;
  description?: string;
  variants?: Record<number, AdvertisementVariantUpdate>;
  pictures?: AdvertisementPictureUpdate[];
};
