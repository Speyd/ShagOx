import { useMutation } from "@tanstack/react-query";
import { toast } from "sonner";
import { verify } from "../../api/api";

export function useVerify(successMessage = "Ви успішно підтвердили свою електронну пошту!") {
  return useMutation({
    mutationFn: verify,
    onSuccess: () => toast.success(successMessage),
    onError: () => {
      toast.error("Помилка серверу.");
    },
  });
}
