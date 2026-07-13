import { useMutation } from "@tanstack/react-query";
import { register } from "../api/register";
import { toast } from "sonner";

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
