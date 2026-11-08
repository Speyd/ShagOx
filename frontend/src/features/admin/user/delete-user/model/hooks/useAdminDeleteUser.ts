import { useMutation, useQueryClient } from "@tanstack/react-query";
import { toast } from "sonner";
import { adminDeleteUser } from "../../api/api";

export function useAdminDeleteUser() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: adminDeleteUser,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["users"] });
      toast.success("Користувача видалено!");
    },
    onError: () => {
      toast.error("Не вдалося видалити користувача. Спробуйте ще раз.");
    },
  });
}
