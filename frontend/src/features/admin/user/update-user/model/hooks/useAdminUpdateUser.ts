import { useMutation, useQueryClient } from "@tanstack/react-query";
import { toast } from "sonner";
import { getErrorMessage } from "@/shared/api/getErrorMessage";
import type { UpdateUserRequest } from "../types";
import { adminUpdateUser } from "../../api/api";

export default function useAdminUpdateUser() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: ({ id, data }: UpdateUserRequest) => adminUpdateUser(id, data),
    onSuccess: () => {
      queryClient.invalidateQueries();
      toast.success("Дані користувача оновлено");
    },
    onError: (error) =>
      toast.error(getErrorMessage(error, "Не вдалося оновити користувача")),
  });
}
