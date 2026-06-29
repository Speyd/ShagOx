import { useMutation, useQueryClient } from "@tanstack/react-query";
import { logout } from "../api/logout";

export function useLogout() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: logout,
    onSuccess: async () => {
      queryClient.removeQueries({ queryKey: ["me"] });
    },
  });
}
