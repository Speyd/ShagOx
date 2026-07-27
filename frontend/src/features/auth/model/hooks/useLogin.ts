import { useMutation, useQueryClient } from "@tanstack/react-query";
import { toast } from "sonner";
import { useAuthStore } from "../../store/useAuthStore";
import { login, me } from "../../api/api";

export function useLogin() {
  const queryClient = useQueryClient();
  const setUser = useAuthStore((state) => state.setUser);

  return useMutation({
    mutationFn: login,

    onSuccess: async () => {
      const user = await queryClient.fetchQuery({
        queryKey: ["me"],
        queryFn: me,
      });
      setUser(user);
      toast.success("Ви успішно увійшли!");
    },

    onError: () => {
      toast.error("Помилка серверу.");
    },
  });
}
