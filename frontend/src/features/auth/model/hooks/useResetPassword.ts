import { useMutation } from "@tanstack/react-query";
import { confirmPasswordReset, resetPassword } from "../../api/api";

export function useResetPassword() {
  return useMutation({ mutationFn: resetPassword });
}

export function useConfirmPasswordReset() {
  return useMutation({ mutationFn: confirmPasswordReset });
}
