import { api } from "@/shared/api/api";
import type {
  LoginRequest,
  GoogleLoginRequest,
  LogoutResponse,
  RegisterRequest,
  RegisterResponse,
  ConfirmResetPasswordRequest,
  ResetPasswordRequest,
  VerifyRequest,
} from "../model/types";
import type { User } from "@/shared/lib/types/user";

export async function login(data: LoginRequest) {
  await api.post("/auth/login", data);
}

export async function googleLogin(data: GoogleLoginRequest) {
  await api.post("/auth/external/google", data);
}

export async function logout(): Promise<LogoutResponse> {
  const response = await api.post("/auth/logout");
  return response.data;
}

export async function me(): Promise<User> {
  const response = await api.get("/users/me");
  return response.data;
}

export async function register(
  data: RegisterRequest,
): Promise<RegisterResponse> {
  const response = await api.post("/auth/register", data);
  return response.data;
}

export async function verify(data: VerifyRequest) {
  const response = await api.post("/verifications", data);
  return response.data;
}

export async function resetPassword(
  data: ResetPasswordRequest,
): Promise<number> {
  const response = await api.post("/users/reset-password", data);
  return response.data;
}

export async function confirmPasswordReset(data: ConfirmResetPasswordRequest) {
  const response = await api.post("/users/reset-password/confirm", data);
  return response.data;
}
