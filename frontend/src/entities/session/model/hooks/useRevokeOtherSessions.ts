import { useMutation, useQueryClient } from "@tanstack/react-query";
import { revokeOtherSessions } from "../../api/api";
import { toast } from "sonner";

export function useRevokeOtherSessions() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: revokeOtherSessions,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["user-sessions"] });
      toast.success("Усі інші сесії завершено");
    },
    onError: () => {
      toast.error("Не вдалося завершити інші сесії");
    },
  });
}
