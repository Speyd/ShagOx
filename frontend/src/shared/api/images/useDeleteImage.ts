import { useMutation } from "@tanstack/react-query";
import { deleteImage } from "./api";

export function useDeleteImage() {
  return useMutation({
    mutationFn: deleteImage,
  });
}
