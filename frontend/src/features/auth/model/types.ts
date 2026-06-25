export interface LoginRequest {
  emailOrPhone: string;
  password: string;
}

export interface LoginResponse {
  success: boolean;
  message: string;
  token: string;
}

export interface RegisterRequest {
  emailOrPhone: string;
  password: string;
}

export interface RegisterResponse {
  id: number;
  emailOrPhone: string;
  userName: string;
  success: boolean;
  message: string;
}
