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
  phone?: string;
  email?: string;
  cityId?: number;
  avatar?: File;
};

export async function updateUser(
  id: number,
  payload: UserUpdatePayload,
): Promise<void> {
  const formData = new FormData();

  if (payload.firstName !== undefined)
    formData.append("FirstName", payload.firstName);
  if (payload.lastName !== undefined)
    formData.append("LastName", payload.lastName);
  if (payload.userName !== undefined)
    formData.append("UserName", payload.userName);
  if (payload.bio !== undefined) formData.append("Bio", payload.bio);
  if (payload.phone !== undefined) formData.append("Phone", payload.phone);
  if (payload.email !== undefined) formData.append("Email", payload.email);
  if (payload.cityId !== undefined)
    formData.append("CityId", String(payload.cityId));
  if (payload.avatar !== undefined) formData.append("Avatar", payload.avatar);

  await api.put(`/users/${id}`, formData, {
    headers: { "Content-Type": "multipart/form-data" },
  });
}

export async function getByContanct(contact: string) {
  const response = await api.get(`/users/contact/${contact}`);
  return response.data;
}
