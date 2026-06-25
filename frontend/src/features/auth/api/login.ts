import { api } from "@/shared/api/api";
import type { LoginRequest, LoginResponse } from "../model/types";

export async function login(data: LoginRequest): Promise<LoginResponse> {
  const response = await api.post("/auth/login", data);
  return response.data;
}
