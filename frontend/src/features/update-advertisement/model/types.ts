export type UpdateAdvertisementRequestDto = {
  title?: string;
  description?: string;
  price?: number;
  properties?: Record<string, string>;
  images: {
    id?: number;
    file?: File;
    order: number;
    isDeleted: boolean;
  }[];
};
