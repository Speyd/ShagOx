import { useMutation, useQueryClient } from "@tanstack/react-query";
import { toast } from "sonner";
import { getUser, updateUser } from "@/entities/user/api/api";
import { useAuthStore } from "@/features/auth";
import type { UpdateProfileDto } from "../schemas/schema";

type UpdateProfileRequest = {
  id: number;
  data: UpdateProfileDto;
};

const KEEP_ON_CURRENCY_CHANGE = ["me", "user", "currencies"];

export default function useUpdateProfile() {
  const queryClient = useQueryClient();
  const setUser = useAuthStore((state) => state.setUser);

  return useMutation({
    mutationFn: async ({ id, data }: UpdateProfileRequest) => {
      await updateUser({
        firstName: data.firstName,
        lastName: data.lastName,
        userName: data.userName,
        bio: data.bio,
        cityId: data.cityId ?? undefined,
        currencyId: data.currencyId ?? undefined,
        avatar: data.avatar ?? undefined,
      });

      return getUser(id);
    },
    onSuccess: (updatedUser) => {
      const previousCurrencyId = useAuthStore.getState().user?.currency?.id;

      setUser(updatedUser);
      queryClient.setQueryData(["me"], updatedUser);
      queryClient.setQueryData(["user", updatedUser.id], updatedUser);

      if (previousCurrencyId !== updatedUser.currency?.id) {
        queryClient.invalidateQueries({
          predicate: (query) =>
            !KEEP_ON_CURRENCY_CHANGE.includes(String(query.queryKey[0])),
        });
      }

      toast.success("Профіль успішно оновлено");
    },
    onError: () => {
      toast.error("Не вдалося зберегти зміни. Спробуйте ще раз.");
    },
  });
}
