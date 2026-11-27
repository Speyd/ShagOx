import { api } from "@/shared/api/api";
import type { PaginatedResponse } from "@/shared/lib/types/paginatedResponse";
import type { UserSession } from "../model/types";

export async function getSessions(params?: {
  page?: number;
  pageSize?: number;
}): Promise<UserSession[]> {
  const { data } = await api.get<PaginatedResponse<UserSession>>(
    "/user-sessions/me",
    {
      params: {
        page: params?.page ?? 1,
        pageSize: params?.pageSize ?? 20,
      },
    },
  );
  return data.items;
}

export async function revokeSession(id: number): Promise<void> {
  const response = await api.post(`/user-sessions/${id}/revoke`);
  return response.data;
}

export async function revokeAllSessions(): Promise<void> {
  const response = await api.post("/auth/logout-all");
  return response.data;
}

export async function revokeOtherSessions(): Promise<void> {
  const response = await api.post("/auth/logout-other");
  return response.data;
}
