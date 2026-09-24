import { useMutation } from "@tanstack/react-query";
import axios from "axios";
import { toast } from "sonner";
import { googleLogin } from "../../api/api";

export function useGoogleLogin() {
  return useMutation({
    mutationFn: googleLogin,
    onSuccess: () => {
      toast.success("Ви успішно увійшли!");
      window.location.href = "/";
    },
    onError: (error) => {
      const responseData = axios.isAxiosError(error)
        ? error.response?.data
        : undefined;
      const message =
        responseData &&
        typeof responseData === "object" &&
        "message" in responseData &&
        typeof responseData.message === "string"
          ? responseData.message
          : "Помилка входу через Google.";

      toast.error(message);
    },
  });
}
