import { api } from "@/shared/api/api";
import type {
  Advertisement,
  AdvertisementVariant,
} from "@/shared/lib/types/advertisements";
import type { PaginatedResponse } from "@/shared/lib/types/paginatedResponse";

type ApiAdvertisement = Omit<Advertisement, "attributes" | "variants"> & {
  attributes?: Record<string, unknown>;
  variants?: AdvertisementVariant[];
};

function normalizeAdvertisement(
  advertisement: ApiAdvertisement,
): Advertisement {
  const variants = advertisement.variants ?? [];
  const firstVariant = variants[0];

  return {
    ...advertisement,
    attributes: advertisement.attributes ?? {},
    price: advertisement.price ?? firstVariant?.price ?? 0,
    previousPrice:
      advertisement.previousPrice ?? firstVariant?.previousPrice ?? 0,
    stock:
      advertisement.stock ??
      variants.reduce((total, variant) => total + variant.stock, 0),
    properties: advertisement.properties ?? advertisement.attributes ?? {},
  };
}

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

type ApiProductTypeItem = {
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

type ApiCategoryItem = {
  id: number;
  name?: string;
  code?: string;
  label?: string;
  lable?: string;
  productType?: ApiProductTypeItem;
};

export type AttributeDefinition = {
  id: number;
  categoryId: number;
  categoryCode: string;
  key: string;
  type: string;
  dictionaryId: number | null;
  required: boolean;
  min: number | null;
  max: number | null;
  isVariant: boolean;
  multiple: boolean;
  lable: string;
};

export type AttributeDictionaryValue = {
  id: number;
  dictionaryId: number;
  code: string;
  value: string | null;
  label: string | null;
};

export type AttributeFilterOption = {
  key: string;
  value: string;
  isVariant: boolean;
};

export async function getCatalogAdvertisements(
  params: CatalogParams = {},
): Promise<PaginatedResponse<Advertisement>> {
  const search = params.search?.trim();
  const categoryId = params.categoryId ?? params.categoryIds?.[0];

  const hasFilters =
    Boolean(search) ||
    categoryId !== undefined ||
    params.inStock !== undefined ||
    params.minPrice !== undefined ||
    params.maxPrice !== undefined ||
    Object.keys(params.attributes ?? {}).length > 0 ||
    Object.keys(params.variantAttributes ?? {}).length > 0;

  const endpoint = hasFilters ? "/advertisements/search" : "/advertisements";

  const response = await api.get<PaginatedResponse<ApiAdvertisement>>(
    endpoint,
    {
      params: {
        Page: params.page ?? 1,
        PageSize: params.pageSize ?? 20,
        Title: search || undefined,
        CategoryId: categoryId,
        InStock: params.inStock,
        MinPrice: params.minPrice,
        MaxPrice: params.maxPrice,
        Attributes: Object.keys(params.attributes ?? {}).length
          ? JSON.stringify(params.attributes)
          : undefined,
        VariantAttributes: Object.keys(params.variantAttributes ?? {}).length
          ? JSON.stringify(params.variantAttributes)
          : undefined,
      },
    },
  );

  return {
    ...response.data,
    items: response.data.items.map(normalizeAdvertisement),
  };
}

export async function getAdvertisements(params?: {
  page?: number;
  pageSize?: number;
}): Promise<PaginatedResponse<Advertisement>> {
  const response = await api.get<PaginatedResponse<ApiAdvertisement>>(
    "/advertisements",
    {
      params: {
        Page: params?.page ?? 1,
        PageSize: params?.pageSize ?? 20,
      },
    },
  );

  return {
    ...response.data,
    items: response.data.items.map(normalizeAdvertisement),
  };
}

export async function getAdminAdvertisements(
  page: number,
  pageSize: number,
): Promise<PaginatedResponse<Advertisement>> {
  const { data } = await api.get<PaginatedResponse<Advertisement>>(
    "/advertisements",
    {
      params: { page, pageSize },
    },
  );

  return data;
}

export async function getAdvertisement(id: number) {
  const response = await api.get<ApiAdvertisement>(`/advertisements/${id}`);
  return normalizeAdvertisement(response.data);
}

export async function getCategories(
  productTypeId?: number,
): Promise<CategoryItem[]> {
  const hasProductType = productTypeId != null;
  const endpoint = hasProductType ? "/categories/search" : "/categories";

  const response = await api.get<PaginatedResponse<ApiCategoryItem>>(endpoint, {
    params: {
      page: 1,
      pageSize: 100,
      productTypeId: productTypeId ?? undefined,
    },
  });

  return response.data.items.map((category) => {
    const categoryTitle =
      category.label || category.lable || category.name || category.code || "";
    const productTypeTitle =
      category.productType?.label ||
      category.productType?.lable ||
      category.productType?.name ||
      "";

    return {
      id: category.id,
      code: category.code ?? category.name ?? "",
      productType: {
        id: category.productType?.id ?? 0,
        name: category.productType?.code ?? category.productType?.name ?? "",
        description: category.productType?.description ?? "",
        label: productTypeTitle,
      },
      label: categoryTitle,
    };
  });
}

export async function getProductTypes(): Promise<ProductTypeItem[]> {
  const response = await api.get<PaginatedResponse<ApiProductTypeItem>>(
    "/product-types",
    {
      params: { page: 1, pageSize: 100 },
    },
  );

  return response.data.items.map((productType) => {
    const title =
      productType.label || productType.lable || productType.name || "";

    return {
      id: productType.id,
      name: productType.code ?? productType.name ?? title,
      description: productType.description ?? "",
      label: title,
    };
  });
}

export async function getAttributeDefinitions(
  categoryId: number,
): Promise<AttributeDefinition[]> {
  const response = await api.get<PaginatedResponse<AttributeDefinition>>(
    "/attribute-definitions/search",
    {
      params: {
        categoryId,
        page: 1,
        pageSize: 100,
      },
    },
  );
  return response.data.items;
}

export async function getAttributeDictionaryValues(
  categoryId: number,
): Promise<AttributeDictionaryValue[]> {
  const response = await api.get<PaginatedResponse<AttributeDictionaryValue>>(
    `/attribute-dictionary-values/category/${categoryId}`,
    {
      params: {
        page: 1,
        pageSize: 100,
      },
    },
  );
  return response.data.items;
}

export async function getCategoryAttributeFilterOptions(
  categoryId: number,
): Promise<AttributeFilterOption[]> {
  const response = await api.get<AttributeFilterOption[]>(
    "/advertisements/attribute-filter-options",
    { params: { categoryId } },
  );
  return response.data;
}
