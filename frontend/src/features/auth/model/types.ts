export interface LoginRequest {
  emailOrPhone: string;
  password: string;
}

export interface RegisterRequest {
  emailOrPhone: string;
  password: string;
}

export interface RegisterResponse {
  id: number;
  emailOrPhone: string;
  name: string;
  success: boolean;
  message: string;
}

export interface User {
  id: number;
  emailOrPhone: string;
  name: string;
  role: string;
}

export interface LogoutResponse {
  success: boolean;
  message: string;
}
