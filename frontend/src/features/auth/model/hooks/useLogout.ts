import { useMutation, useQueryClient } from "@tanstack/react-query";
import { toast } from "sonner";
import { logout } from "../../api/api";
import { useAuthStore } from "../../store/useAuthStore";

export function useLogout() {
  const queryClient = useQueryClient();
  const logoutUser = useAuthStore((state) => state.logout);

  return useMutation({
    mutationFn: logout,
    onSuccess: async () => {
      logoutUser();
      queryClient.clear();
      toast.success("Ви успішно вийшли.");
    },
  });
}
