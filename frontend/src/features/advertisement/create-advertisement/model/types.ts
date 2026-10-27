export type ConditionOption = {
  id: number;
  label: string;
  code: string;
};

export const CONDITIONS: ConditionOption[] = [
  { id: 0, label: "Новий", code: "new" },
  { id: 1, label: "Вживаний (як новий)", code: "used_like_new" },
  { id: 2, label: "Вживаний (добрий стан)", code: "used_good" },
  { id: 3, label: "Вживаний (задовільний стан)", code: "used_fair" },
  { id: 4, label: "На запчастини / несправний", code: "for_parts" },
];

export type CreateAdvertisementFormData = {
  title: string;
  categoryId: string; // string for Mantine Select, parsed to number
  price: number | "";
  stock: number | "";
  description: string;
  hashtags: string[];
  brand: string;
  conditionId: string; // string for Mantine Select, parsed to number
  tags: string;
  attributes: Record<string, unknown>;
  variantAttributes: Record<string, unknown>;
};

export type ImageFileItem = {
  id: string;
  file: File;
  previewUrl: string;
};

export type CreateAdvertisementPayload = {
  title: string;
  description: string;
  popularity: number;
  currencyId: number;
  conditionId: number;
  categoryId: number;
  statusId: number;
  pictures: File[];
  attributes: Record<string, unknown>;
  variants: {
    advertisementId: number;
    price: number;
    previousPrice: number;
    stock: number;
    attributes: Record<string, unknown>;
  }[];
};

