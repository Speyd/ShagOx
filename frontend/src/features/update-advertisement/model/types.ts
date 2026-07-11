export type UpdateAdvertisementDto = {
  title?: string;
  description?: string;
  price?: number;

  newImages?: File[];
  deletedImageIds?: number[];
};
