import { api } from "@/shared/api/api";
import type {
  LoginRequest,
  LoginResponse,
  GoogleLoginRequest,
  LogoutResponse,
  RegisterRequest,
  RegisterResponse,
  ResetPasswordRequest,
  ChangePasswordRequest,
  ChangeEmailRequest,
  ChangePhoneRequest,
  VerifyRequest,
} from "../model/types";
import type { User } from "@/shared/lib/types/user";

export async function login(data: LoginRequest): Promise<LoginResponse> {
  const response = await api.post<LoginResponse | null>("/auth/login", data);
  return { accessToken: response.data?.accessToken ?? null };
}

export function isTwoFactorRequired(response: LoginResponse) {
  return !response.accessToken;
}

export async function enableTwoFactor() {
  await api.post("/auth/two-factor/enable");
}

export async function disableTwoFactor() {
  await api.post("/auth/two-factor/disable");
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

export async function resetPassword(data: ResetPasswordRequest) {
  const response = await api.post("/users/reset-password", data);
  return response.data;
}

export async function changePassword(data: ChangePasswordRequest) {
  const response = await api.put("/users/password", data);
  return response.data;
}

export async function changeEmail(data: ChangeEmailRequest) {
  const response = await api.put("/users/email", data);
  return response.data;
}

export async function changePhone(data: ChangePhoneRequest) {
  const response = await api.put("/users/phone", data);
  return response.data;
}
