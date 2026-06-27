import { useMutation } from "@tanstack/react-query";
import { register } from "../api/register";

export function useRegister() {
  return useMutation({
    mutationFn: register,
    onSuccess: (data) => console.log(data),
  });
}
