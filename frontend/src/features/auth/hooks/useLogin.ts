import { useMutation, useQueryClient } from "@tanstack/react-query";
import { login } from "../api/login";
import { toast } from "sonner";

export function useLogin() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: login,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["me"] });
      toast.success("Ви успішно увійшли!");
    },
    onError: () => {
      toast.error("Помилка серверу.");
    },
  });
}
