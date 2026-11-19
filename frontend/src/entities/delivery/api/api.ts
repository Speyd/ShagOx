import { api } from "@/shared/api/api";
import type { PaginatedResponse } from "@/shared/lib/types/paginatedResponse";
import type {
  DeliveryCity,
  DeliveryPoint,
  DeliveryPointType,
  DeliveryProvider,
  DeliveryStreet,
} from "../model/types";

const SEARCH_PAGE_SIZE = 20;

export async function getDeliveryProviders(): Promise<DeliveryProvider[]> {
  const { data } = await api.get<PaginatedResponse<DeliveryProvider>>(
    "/delivery-providers",
    { params: { page: 1, pageSize: 50 } },
  );
  return data.items;
}

export async function searchDeliveryCities(
  providerCode: string,
  search: string,
): Promise<DeliveryCity[]> {
  const { data } = await api.get<DeliveryCity[]>("/delivery/cities", {
    params: {
      providerCode,
      citySearch: search,
      page: 1,
      pageSize: SEARCH_PAGE_SIZE,
    },
  });
  return data;
}

export async function searchDeliveryPoints(
  providerCode: string,
  cityExternalId: string,
  pointType: DeliveryPointType,
  search: string,
): Promise<DeliveryPoint[]> {
  const { data } = await api.get<DeliveryPoint[]>("/delivery/points", {
    params: {
      providerCode,
      cityExternalId,
      deliveryPointType: pointType,
      search,
      page: 1,
      pageSize: SEARCH_PAGE_SIZE,
    },
  });
  return data;
}

export async function searchDeliveryStreets(
  providerCode: string,
  cityExternalId: string,
  search: string,
): Promise<DeliveryStreet[]> {
  const { data } = await api.get<DeliveryStreet[]>("/delivery/streets", {
    params: {
      providerCode,
      cityExternalId,
      search,
      page: 1,
      pageSize: SEARCH_PAGE_SIZE,
    },
  });
  return data;
}
