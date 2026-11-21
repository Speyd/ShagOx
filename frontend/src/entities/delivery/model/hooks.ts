import { useQuery } from "@tanstack/react-query";
import {
  getDeliveryProviders,
  searchDeliveryCities,
  searchDeliveryPoints,
  searchDeliveryStreets,
} from "../api/api";
import type { DeliveryPointType } from "./types";

export function useDeliveryProviders() {
  return useQuery({
    queryKey: ["delivery-providers"],
    queryFn: getDeliveryProviders,
    staleTime: Infinity,
  });
}

export function useDeliveryCities(providerCode: string, search: string) {
  return useQuery({
    queryKey: ["delivery-cities", providerCode, search],
    queryFn: () => searchDeliveryCities(providerCode, search),
    enabled: !!providerCode && search.trim().length >= 2,
  });
}

export function useDeliveryPoints(
  providerCode: string,
  cityExternalId: string | undefined,
  pointType: DeliveryPointType,
  search: string,
) {
  return useQuery({
    queryKey: ["delivery-points", providerCode, cityExternalId, pointType, search],
    queryFn: () =>
      searchDeliveryPoints(providerCode, cityExternalId!, pointType, search),
    enabled: !!providerCode && !!cityExternalId,
  });
}

export function useDeliveryStreets(
  providerCode: string,
  cityExternalId: string | undefined,
  search: string,
) {
  return useQuery({
    queryKey: ["delivery-streets", providerCode, cityExternalId, search],
    queryFn: () => searchDeliveryStreets(providerCode, cityExternalId!, search),
    enabled: !!providerCode && !!cityExternalId && search.trim().length >= 2,
  });
}
