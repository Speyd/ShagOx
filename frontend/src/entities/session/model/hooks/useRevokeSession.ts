import { useMutation, useQueryClient } from "@tanstack/react-query";
import { revokeSession } from "../../api/api";
import { toast } from "sonner";

export function useRevokeSession() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (sessionId: number) => revokeSession(sessionId),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["user-sessions"] });
      toast.success("Сесію успішно завершено");
    },
    onError: () => {
      toast.error("Не вдалося завершити сесію");
    },
  });
}
