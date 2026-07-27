import { useMutation, useQueryClient } from "@tanstack/react-query";
import { toast } from "sonner";
import { logout } from "../../api/api";

export function useLogout() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: logout,
    onSuccess: async () => {
      queryClient.clear();
      toast.success("Ви успішно вийшли.");
    },
  });
}
