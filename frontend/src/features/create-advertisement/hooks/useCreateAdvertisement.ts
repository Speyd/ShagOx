import { useMutation, useQueryClient } from "@tanstack/react-query";
import { createAdvertisement } from "../model/api";
import { toast } from "sonner";

export function useCreateAdvertisement() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: createAdvertisement,

    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["advertisements"],
      });
      toast.success("Advertisement created!");
    },

    onError: () => {
      toast.error("Failed to create advertisement");
    },
  });
}
