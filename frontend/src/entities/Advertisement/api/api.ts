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
  sortBy?: string;
  sortOrder?: "asc" | "desc";
};

export type CategoryItem = {
  id: number;
  name: string;
};

type ApiCategoryItem = {
  id: number;
  name?: string;
  code?: string;
  label?: string;
  lable?: string;
};

export type ProductTypeItem = {
  id: number;
  name: string;
  description: string;
  lable: string;
};

type ApiProductTypeItem = {
  id: number;
  name?: string;
  description?: string;
  lable?: string;
};
export async function getCatalogAdvertisements(
  params: CatalogParams = {},
): Promise<PaginatedResponse<Advertisement>> {
  const search = params.search?.trim();
  const categoryIds = params.categoryIds?.length
    ? params.categoryIds
    : undefined;

  const hasFilters =
    Boolean(search) ||
    Boolean(categoryIds) ||
    params.inStock !== undefined ||
    params.minPrice !== undefined ||
    params.maxPrice !== undefined;

  const endpoint = hasFilters ? "/advertisements/search" : "/advertisements";

  const response = await api.get<PaginatedResponse<ApiAdvertisement>>(
    endpoint,
    {
      params: {
        page: params.page ?? 1,
        pageSize: params.pageSize ?? 20,
        title: search || undefined,
        categoryIds: categoryIds, // Якщо порожньо, буде undefined і не потрапить в URL
        inStock: params.inStock,
        minPrice: params.minPrice,
        maxPrice: params.maxPrice,
      },
      paramsSerializer: {
        indexes: null, // Дозволяє писати categoryIds=21 замість categoryIds[0]=21
      },
    },
  );

  return {
    ...response.data,
    items: response.data.items.map(normalizeAdvertisement),
  };
}

export async function getAdvertisements(): Promise<
  PaginatedResponse<Advertisement>
> {
  const response =
    await api.get<PaginatedResponse<ApiAdvertisement>>("/advertisements");

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
  const endpoint = productTypeId ? "/categories/search" : "/categories";

  const response = await api.get<PaginatedResponse<ApiCategoryItem>>(endpoint, {
    params: {
      page: 1,
      pageSize: 100,
      productTypeId: productTypeId ?? undefined,
    },
  });

  return response.data.items.map((category: ApiCategoryItem) => ({
    id: category.id,
    name: category.lable ?? category.name ?? "",
  }));
}

export async function getProductTypes(): Promise<ProductTypeItem[]> {
  const response = await api.get<PaginatedResponse<ProductTypeItem>>(
    "/product-types",
    {
      params: { page: 1, pageSize: 100 },
    },
  );

  return response.data.items.map((productType: ApiProductTypeItem) => ({
    id: productType.id,
    name: productType.lable ?? "",
    description: productType.description ?? "",
    lable: productType.lable ?? "",
  }));
}
