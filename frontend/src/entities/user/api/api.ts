import type { UserStatistics } from "../model/statistics";
import { api } from "@/shared/api/api";
import type { PaginatedResponse } from "@/shared/lib/types/paginatedResponse";
import type { User } from "@/shared/lib/types/user";

export async function getUsers(
  page: number,
  pageSize: number,
): Promise<PaginatedResponse<User>> {
  const { data } = await api.get("/admin/users", {
    params: {
      page,
      pageSize,
    },
  });

  return data;
}

export async function getUser(id: number): Promise<User> {
  const response = await api.get(`/users/${id}`);
  return response.data;
}

export type UserUpdatePayload = {
  firstName?: string;
  lastName?: string;
  userName?: string;
  bio?: string;
  cityId?: number;
  avatar?: File;
  currencyId?: number;
};

export async function updateUser(payload: UserUpdatePayload): Promise<void> {
  const formData = new FormData();

  if (payload.firstName !== undefined)
    formData.append("FirstName", payload.firstName);
  if (payload.lastName !== undefined)
    formData.append("LastName", payload.lastName);
  if (payload.userName !== undefined)
    formData.append("UserName", payload.userName);
  if (payload.bio !== undefined) formData.append("Bio", payload.bio);
  if (payload.cityId !== undefined)
    formData.append("CityId", String(payload.cityId));
  if (payload.avatar !== undefined) formData.append("Avatar", payload.avatar);
  if (payload.currencyId !== undefined)
    formData.append("CurrencyId", String(payload.currencyId));

  await api.put(`/users/me`, formData, {
    headers: { "Content-Type": "multipart/form-data" },
  });
}

export type UserByContact = {
  id: number;
  userName: string;
  email: string | null;
  phone: string | null;
};

export async function getByContanct(contact: string): Promise<UserByContact> {
  const response = await api.get(
    `/users/contact/${encodeURIComponent(contact.trim())}`,
  );
  return response.data;
}

export async function getMyStatistics(): Promise<UserStatistics> {
  const { data } = await api.get<UserStatistics>("/users/me/statistics");
  return data;
}
