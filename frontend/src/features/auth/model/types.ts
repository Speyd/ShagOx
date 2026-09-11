import type { User } from "@/shared/lib/types/user";

export type { User };

export type LoginRequest = {
  emailOrPhone: string;
  password: string;
};

export type RegisterRequest = {
  emailOrPhone: string;
  password: string;
};

export type RegisterResponse = {
  id: number;
  emailOrPhone: string;
  name: string;
  success: boolean;
  message: string;
};

export type AuthUser = {
  id: number;
  emailOrPhone: string;
  name: string;
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
