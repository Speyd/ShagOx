import type { BaseEntity } from "@/shared/api/crud";

export type AdminCodeEntity = BaseEntity & {
  code: string;
  label?: string | null;
};

export type AdminProductType = BaseEntity & {
  name: string;
  description: string;
  label?: string | null;
};

export type AdminCategory = AdminCodeEntity & {
  productTypeId: number;
};

export type AdminRegion = AdminCodeEntity;

export type AdminCity = AdminCodeEntity & {
  region?: AdminRegion | null;
};

export type AdminCurrency = BaseEntity & {
  code: string;
  symbol: string;
  name: string;
};

export type AdminRole = BaseEntity & {
  name: string;
  description: string;
};

export type AdminDictionary = BaseEntity & {
  code: string;
  attributeIds: number[];
  valueIds: number[];
};

export type AdminDictionaryValue = BaseEntity & {
  dictionaryId: number;
  code: string;
  value: string | null;
  label: string | null;
};

export type AttributeKind = "String" | "Integer" | "Decimal" | "Boolean" | "Select";

export type AdminAttributeDefinition = BaseEntity & {
  categoryId: number;
  categoryCode: string;
  key: string;
  dictionaryId: number | null;
  type: AttributeKind;
  required: boolean;
  min: number | null;
  max: number | null;
  isVariant: boolean;
  multiple: boolean;
  label: string | null;
};

export type AdminReview = BaseEntity & {
  owner: {
    id: number;
    firstName: string | null;
    lastName: string | null;
    userName: string | null;
    avatar: { url: string } | null;
  };
  commentableEntityId: number;
  text: string;
  createdAt: string;
  updatedAt: string | null;
  grade?: number;
  repliesCount?: number;
  pictures: { id: number; url: string }[];
};
