export type DeliveryProvider = {
  id: number;
  code: string;
  label: string | null;
};

export type DeliveryPointType = "Branch" | "ParcelLocker" | "ParcelShop";

export type DeliveryCity = {
  externalId: string;
  name: string;
  area: string | null;
};

export type DeliveryPoint = {
  externalId: string;
  number: string;
  name: string;
  address: string;
  type: DeliveryPointType;
};

export type DeliveryStreet = {
  externalId: string;
  name: string;
};

export const NOVA_POSHTA_CODE = "NOVA_POSHTA";
