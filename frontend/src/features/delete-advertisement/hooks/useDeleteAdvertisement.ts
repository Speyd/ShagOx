import { useMutation, useQueryClient } from "@tanstack/react-query";
import { deleteAdvertisement } from "../model/api";
import { toast } from "sonner";

export function useDeleteAdvertisement() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: deleteAdvertisement,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["advertisements"] });
      toast.success("Advertisement deleted!");
    },
    onError: () => {
      toast.error("Failed to delete advertisement");
    },
  });
}
