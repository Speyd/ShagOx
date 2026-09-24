import { useMutation } from "@tanstack/react-query";
import axios from "axios";
import { toast } from "sonner";
import { register } from "../../api/api";

export function useRegister() {
  return useMutation({
    mutationFn: register,
    onSuccess: () => {
      toast.success("Код підтвердження надіслано!");
    },
    onError: (error) => {
      const responseData = axios.isAxiosError(error)
        ? error.response?.data
        : undefined;
      const message =
        typeof responseData === "string"
          ? responseData
          : responseData &&
              typeof responseData === "object" &&
              "message" in responseData &&
              typeof responseData.message === "string"
            ? responseData.message
            : "Помилка серверу.";

      toast.error(message);
    },
  });
}
