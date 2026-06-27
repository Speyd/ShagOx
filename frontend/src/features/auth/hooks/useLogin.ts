import { useMutation } from "@tanstack/react-query";
import { login } from "../api/login";

export function useLogin() {
  return useMutation({
    mutationFn: login,
    onSuccess: (data) => {
      localStorage.setItem("message", data.message);
    },
  });
}
