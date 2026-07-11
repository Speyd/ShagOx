import { useMutation } from "@tanstack/react-query";
import { createImage } from "./api";

export function useCreateImage() {
  return useMutation({
    mutationFn: createImage,
  });
}
