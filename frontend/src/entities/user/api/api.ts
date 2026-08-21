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
