export type SelectOption = { value: string; label: string };

export type ImageFileItem = {
  id: string;
  file: File;
  previewUrl: string;
};

export type AttributeFormValue = string | number | string[] | null | undefined;

export type VariantRow = {
  key: string;
  options: Record<string, string>;
  price: number | "";
  previousPrice: number | "";
  stock: number | "";
};

type ApiScalar = string | number | boolean;
export type ApiAttributeValue = ApiScalar | ApiScalar[];
export type ApiAttributes = Record<string, ApiAttributeValue>;

export type CreateAdvertisementPayload = {
  title: string;
  description: string;
  categoryId: number;
  conditionId: number;
  currencyId: number;
  statusId: number;
  pictures: File[];
  attributes: ApiAttributes;
  variants: Array<{
    price: number;
    previousPrice: number;
    stock: number;
    attributes: ApiAttributes;
  }>;
};

export type CreateAdvertisementResponse = {
  id: number;
  createdAt: string;
};

export type LookupDto = {
  id: number;
  code: string;
  label?: string | null;
};

export type CurrencyDto = {
  id: number;
  code: string;
  symbol: string;
  name: string;
};
