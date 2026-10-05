import axios from "axios";
import { toast } from "sonner";

export const api = axios.create({
  baseURL: "/api",
  withCredentials: true,
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
