import { useMutation, useQueryClient } from "@tanstack/react-query";
import { toast } from "sonner";
import { getUser, updateUser } from "@/entities/user/api/api";

import type { UpdateProfileDto } from "../schemas/schema";
import { useAuthStore } from "@/features/auth";

type UpdateProfileRequest = {
  id: number;
  data: UpdateProfileDto;
};

export default function useUpdateProfile() {
  const queryClient = useQueryClient();
  const setUser = useAuthStore((state) => state.setUser);

  return useMutation({
    mutationFn: async ({ id, data }: UpdateProfileRequest) => {
      await updateUser(id, {
        firstName: data.firstName,
        lastName: data.lastName,
        userName: data.userName,
        bio: data.bio,
        email: data.email,
        cityId: data.cityId ?? undefined,
        avatar: data.avatar ?? undefined,
      });

      return getUser(id);
    },
    onSuccess: async (updatedUser) => {
      setUser(updatedUser);
      queryClient.setQueryData(["me"], updatedUser);
      queryClient.setQueryData(["user", updatedUser.id], updatedUser);
      toast.success("Профіль успішно оновлено");
    },
    onError: () => {
      toast.error("Не вдалося зберегти зміни. Спробуйте ще раз.");
    },
  });
}
