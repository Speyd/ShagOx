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
    const isAuthCheck = url.includes("/users/me") || url.includes("/login");

    if (error.response?.status === 401 && !isAuthCheck) {
      toast.error("Сесія закінчилась або ви не авторизовані.");
    }

    return Promise.reject(error);
  },
);
