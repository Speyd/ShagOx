import { useMutation } from "@tanstack/react-query";
import { toast } from "sonner";
import { register } from "../../api/api";

export function useRegister() {
  return useMutation({
    mutationFn: register,
    onSuccess: () => {
      toast.success("Ви успішно зареєструвались!");
    },
    onError: () => {
      toast.error("Помилка серверу.");
    },
  });
}
