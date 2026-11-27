import axios from "axios";
import { toast } from "sonner";

const DEVICE_ID_KEY = "deviceIdentifier";

export function getDeviceIdentifier(): string {
  let deviceIdentifier = localStorage.getItem(DEVICE_ID_KEY);

  if (!deviceIdentifier) {
    deviceIdentifier = crypto.randomUUID();

    localStorage.setItem(DEVICE_ID_KEY, deviceIdentifier);
  }

  return deviceIdentifier;
}

export const api = axios.create({
  baseURL: "/api",
  withCredentials: true,
  headers: {
    "Accept-Language": "uk",
  },
});

api.interceptors.request.use((config) => {
  config.headers.set("X-Device-Identifier", getDeviceIdentifier());

  return config;
});

api.interceptors.response.use(
  (response) => response,
  (error) => {
    const url = error.config?.url ?? "";
    const isAuthRequest =
      url.includes("/users/me") ||
      url.includes("/auth/") ||
      url.includes("/authentication");

    if (error.response?.status === 401 && !isAuthRequest) {
      toast.error("Сесія закінчилась або ви не авторизовані.");

      if (window.location.pathname !== "/authentication") {
        window.location.replace("/authentication");
      }
    }

    return Promise.reject(error);
  },
);
