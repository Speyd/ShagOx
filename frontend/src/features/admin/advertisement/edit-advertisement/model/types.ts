import type { Advertisement } from "@/shared/lib/types/advertisements";
import type { EntityStatus } from "@/shared/api/crud";
import type { ImageItem } from "@/shared/lib/types/image";
import type { AttributeFormValue } from "@/features/advertisement/create-advertisement/model/types";

export type AdminAdvertisement = Advertisement & {
  entityStatus?: EntityStatus;
};

export type EditorVariant = {
  uid: string;
  id?: number;
  options: Record<string, string>;
  labels: Record<string, string>;
  price: number | "";
  previousPrice: number | "";
  stock: number | "";
  initialPrice?: number;
  initialStock?: number;
};

export type EditorValues = {
  title: string;
  description: string;
  categoryId: string | null;
  conditionId: string | null;
  currencyId: string | null;
  statusId: string | null;
  attributes: Record<string, AttributeFormValue>;
  variants: EditorVariant[];
  images: ImageItem[];
};

export type EditorChanges = {
  deletedVariantIds: number[];
  deletedImageIds: number[];
};
