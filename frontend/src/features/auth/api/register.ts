import { api } from "@/shared/api/api";
import type { RegisterRequest, RegisterResponse } from "../model/types";

export async function register(
  data: RegisterRequest,
): Promise<RegisterResponse> {
  const response = await api.post("/auth/register", data);
  return response.data;
}
