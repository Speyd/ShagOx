export type CatalogParams = {
  page?: number;
  pageSize?: number;
  search?: string;
  categoryId?: number;
  minPrice?: number;
  maxPrice?: number;
  attributes?: Record<string, unknown>;
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
