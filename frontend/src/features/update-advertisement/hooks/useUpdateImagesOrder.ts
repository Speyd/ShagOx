import { useMutation } from "@tanstack/react-query";
import { updateImagesOrder } from "../model/api";

export function useUpdateImagesOrder() {
  return useMutation({
    mutationFn: ({
      advertisementId,
      imageIds,
    }: {
      advertisementId: number;
      imageIds: number[];
    }) => updateImagesOrder(advertisementId, imageIds),
  });
}
