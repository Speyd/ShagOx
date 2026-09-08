import { api } from "@/shared/api/api";
import type {
  LoginRequest,
  LogoutResponse,
  RegisterRequest,
  RegisterResponse,
} from "../model/types";
import type { User } from "@/shared/lib/types/user";

export async function login(data: LoginRequest) {
  await api.post("/login", data);
}

export async function logout(): Promise<LogoutResponse> {
  const response = await api.post("/logout");
  return response.data;
}

export async function me(): Promise<User> {
  const response = await api.get("/users/me");
  return response.data;
}

export async function register(
  data: RegisterRequest,
): Promise<RegisterResponse> {
  const response = await api.post("/register", data);
  return response.data;
}
