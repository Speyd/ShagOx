import { useMutation, useQueryClient } from "@tanstack/react-query";
import { createAdvertisement } from "../model/api";

export function useCreateAdvertisement() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: createAdvertisement,

    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["advertisements"],
      });
    },
  });
}
