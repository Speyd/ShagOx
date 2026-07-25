import axios from "axios";
import { toast } from "sonner";

export const api = axios.create({
  baseURL: "/api",
  withCredentials: true,
});

api.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response?.status === 401) {
      toast.error("Ви не авторизовані.");
    }

    return Promise.reject(error);
  },
);
