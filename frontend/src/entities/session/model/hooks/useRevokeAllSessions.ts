import { useMutation, useQueryClient } from "@tanstack/react-query";
import { useNavigate } from "react-router-dom";
import { toast } from "sonner";
import { revokeAllSessions } from "../../api/api";

export function useRevokeAllSessions() {
  const queryClient = useQueryClient();
  const navigate = useNavigate();

  return useMutation({
    mutationFn: revokeAllSessions,
    onSuccess: () => {
      queryClient.clear();
      toast.success("Ви вийшли з усіх пристроїв");
      navigate("/authentication", { replace: true });
    },
    onError: () => {
      toast.error("Не вдалося вийти з усіх сесій");
    },
  });
}
