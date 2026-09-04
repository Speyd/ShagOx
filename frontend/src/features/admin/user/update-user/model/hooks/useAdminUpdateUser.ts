import { useMutation } from "@tanstack/react-query";
import type { UpdateUserRequest } from "../types";
import { adminUpdateUser } from "../../api/api";
import { toast } from "sonner";

export default function useAdminUpdateUser() {
  return useMutation({
    mutationFn: ({ id, data }: UpdateUserRequest) => adminUpdateUser(id, data),
    onSuccess: () => toast.success("Ви успішно оновили користувача!"),
    onError: () => {
      toast.error("Не вдалося оновити користувача. Спробуйте ще раз.");
    },
  });
}
