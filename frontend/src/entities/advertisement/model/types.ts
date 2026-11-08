import type {
  Advertisement,
  AdvertisementVariant,
} from "@/shared/lib/types/advertisements";

export type ApiAdvertisement = Omit<
  Advertisement,
  "attributes" | "variants"
> & {
  price?: number;
  previousPrice?: number;
  stock?: number;
  properties?: Record<string, unknown>;
  attributes?: Record<string, unknown>;
  variants?: AdvertisementVariant[];
};

export type CatalogParams = {
  page?: number;
  pageSize?: number;
  search?: string;
  categoryId?: number;
  categoryIds?: number[];
  inStock?: boolean;
  minPrice?: number;
  maxPrice?: number;
  memory?: string[];
  colors?: string[];
  attributes?: Record<string, unknown>;
  variantAttributes?: Record<string, unknown>;
  sortBy?: string;
  sortOrder?: "asc" | "desc";
};

export type ProductTypeItem = {
  id: number;
  name: string;
  description: string;
  label: string;
};

export type ApiProductTypeItem = {
  id: number;
  name?: string;
  code?: string;
  description?: string;
  label?: string;
  lable?: string;
};

export type CategoryItem = {
  id: number;
  code: string;
  productType: ProductTypeItem;
  label: string;
};

export type ApiCategoryItem = {
  id: number;
  name?: string;
  code?: string;
  label?: string;
  lable?: string;
  productType?: ApiProductTypeItem;
};

export type AttributeType =
  | "String"
  | "Integer"
  | "Decimal"
  | "Boolean"
  | "Select";

export type AttributeInfo = {
  id: number;
  categoryId: number;
  categoryCode: string;
  key: string;
  dictionaryId: number | null;
  type: AttributeType;
  required: boolean;
  min: number | null;
  max: number | null;
  isVariant: boolean;
  multiple: boolean;
  label: string | null;
};

export type DictionaryValueItem = {
  id: number;
  dictionaryId: number;
  code: string;
  value: string | null;
  label: string;
};

export type CategoryAttributeValueResponse = {
  attribute: AttributeInfo;
  values: DictionaryValueItem[] | null;
};

export type AttributeFacet = {
  key: string;
  value: string;
  count: number;
};

export type CategoryFacets = {
  minPrice: number | null;
  maxPrice: number | null;
  attributes: AttributeFacet[];
};
