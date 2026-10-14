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

export async function getCatalogAdvertisements(
  params: CatalogParams = {},
): Promise<PaginatedResponse<Advertisement>> {
  const search = params.search?.trim();
  const hasFilters =
    Boolean(search) ||
    (params.categoryIds?.length ?? 0) > 0 ||
    params.inStock !== undefined ||
    params.minPrice !== undefined ||
    params.maxPrice !== undefined ||
    (params.memory?.length ?? 0) > 0 ||
    (params.colors?.length ?? 0) > 0;
  const endpoint = hasFilters ? "/advertisements/search" : "/advertisements";

  const response = await api.get<PaginatedResponse<ApiAdvertisement>>(
    endpoint,
    {
      params: {
        page: params.page ?? 1,
        pageSize: params.pageSize ?? 20,
        attributes: [],
        title: search || undefined,
        categoryIds: params.categoryIds,
        inStock: params.inStock,
        minPrice: params.minPrice,
        maxPrice: params.maxPrice,
        memory: params.memory,
        colors: params.colors,
        sortBy: params.sortBy,
        sortOrder: params.sortOrder,
      },
      paramsSerializer: {
        indexes: null,
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

export async function getCategories(): Promise<CategoryItem[]> {
  const response = await api.get<PaginatedResponse<CategoryItem>>(
    "/categories/search",
    {
      params: { page: 1, pageSize: 100 },
    },
  );

  return response.data.items.map((category: ApiCategoryItem) => ({
    id: category.id,
    name:
      category.lable ??
      category.label ??
      category.name ??
      category.code ??
      "Без назви",
  }));
}
