import { api } from "@/shared/api/api";
import type { LogoutResponse } from "../model/types";

export async function logout(): Promise<LogoutResponse> {
  const response = await api.post("/auth/logout");
  return response.data;
}
