import { api } from "@/shared/api/api";
import type { Advertisement } from "@/shared/lib/types/advertisements";
import type { PaginatedResponse } from "@/shared/lib/types/paginatedResponse";
import type {
  ApiAdvertisement,
  ApiCategoryItem,
  ApiProductTypeItem,
  CategoryFacets,
  CatalogParams,
  CategoryAttributeValueResponse,
  CategoryItem,
  ProductTypeItem,
} from "../model/types";

function normalizeAdvertisement(
  advertisement: ApiAdvertisement,
): Advertisement {
  const variants = advertisement.variants ?? [];
  const firstVariant = variants[0];

  const normalized = {
    ...advertisement,
    attributes: advertisement.attributes ?? {},
    price: advertisement.price ?? firstVariant?.price ?? 0,
    previousPrice:
      advertisement.previousPrice ?? firstVariant?.previousPrice ?? 0,
    stock:
      advertisement.stock ??
      variants.reduce((total, variant) => total + (variant.stock ?? 0), 0),
    properties: advertisement.properties ?? advertisement.attributes ?? {},
  };

  return normalized as unknown as Advertisement;
}

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

export async function getAdvertisement(id: number) {
  const response = await api.get<ApiAdvertisement>(`/advertisements/${id}`);
  return normalizeAdvertisement(response.data);
}

export async function getCategories(
  productTypeId?: number,
): Promise<CategoryItem[]> {
  const endpoint = productTypeId != null ? "/categories/search" : "/categories";

  const response = await api.get<PaginatedResponse<ApiCategoryItem>>(endpoint, {
    params: {
      page: 1,
      pageSize: 100,
      productTypeId: productTypeId ?? undefined,
    },
  });

  return response.data.items.map((category) => ({
    id: category.id,
    code: category.code ?? category.name ?? "",
    productType: {
      id: category.productType?.id ?? 0,
      name: category.productType?.code ?? category.productType?.name ?? "",
      description: category.productType?.description ?? "",
      label:
        category.productType?.label ||
        category.productType?.lable ||
        category.productType?.name ||
        "",
    },
    label:
      category.label || category.lable || category.name || category.code || "",
  }));
}

export async function getProductTypes(): Promise<ProductTypeItem[]> {
  const response = await api.get<PaginatedResponse<ApiProductTypeItem>>(
    "/product-types",
    {
      params: { page: 1, pageSize: 100 },
    },
  );

  return response.data.items.map((pt) => ({
    id: pt.id,
    name: pt.code ?? pt.name ?? pt.label ?? "",
    description: pt.description ?? "",
    label: pt.label || pt.lable || pt.name || "",
  }));
}

/** Атрибути категорії разом зі значеннями їхніх словників (один запит). */
export async function getAttributeDictionaryValues(
  categoryId?: number,
): Promise<CategoryAttributeValueResponse[]> {
  if (!categoryId) return [];
  const response = await api.get<CategoryAttributeValueResponse[]>(
    `/attribute-values/category/${categoryId}`,
    // pageSize обмежує кількість значень у кожному словнику
    { params: { page: 1, pageSize: 200 } },
  );
  return response.data.filter((item) => item.attribute != null);
}

export async function getCategoryFacets(categoryId: number): Promise<CategoryFacets> {
  const { data } = await api.get<CategoryFacets>("/advertisements/facets", {
    params: { categoryId },
  });
  return data;
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
