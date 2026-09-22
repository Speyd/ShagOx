import type { User } from "@/shared/lib/types/user";

export type { User };

export type LoginRequest = {
  emailOrPhoneOrUserName: string;
  password: string;
};

export type GoogleLoginRequest = {
  code: string;
};

export type RegisterRequest = {
  emailOrPhone: string;
  userName: string;
  password: string;
};

export type RegisterResponse = {
  id: number;
  emailOrPhone: string;
  userName: string;
  emailVerificationRequired: boolean;
  phoneVerificationRequired: boolean;
};

export type AuthUser = {
  id: number;
  emailOrPhone: string;
  userName: string;
  roles: Role[];
};

export type Role = {
  id: number;
  name: string;
  description: string;
};

export type LogoutResponse = {
  success: boolean;
  message: string;
};

export type VerifyRequest = {
  userId?: number;
  code: string;
};

export type ConfirmResetPasswordRequest = {
  userId: number;
  code: string;
  newPassword: string;
};

export type ResetPasswordRequest = {
  userId: number;
  emailOrPhoneOrUserName: string;
  newPassword: string;
};