import type { EntityStatus } from "@/shared/api/crud";

export type DeviceInfo = {
  id: number;
  deviceName: string;
  deviceType: string;
  operatingSystem: string;
  deviceIdentifier: string;
  entityStatus: EntityStatus;
};

export type UserDevice = {
  id: number;
  ownerId: number;
  status: EntityStatus;
  device: DeviceInfo;
  createdAt: string;
  lastSeenAt: string;
  isTrusted: boolean;
  entityStatus: EntityStatus;
};

export type UserSession = {
  id: number;
  ownerId: number;
  userDevice: UserDevice;
  browser: string;
  browserVersion: string;
  ipAddress: string;
  createdAt: string;
  expiresAt: string;
  revokedAt: string | null;
  lastUsedAt: string;
  isActive: boolean;
  revocationReason: string | null;
  entityStatus: EntityStatus;
};
