import { useMutation } from "@tanstack/react-query";
import { toast } from "sonner";
import { verify } from "../../api/api";
import { getErrorMessage } from "@/shared/api/getErrorMessage";

export function useVerify(successMessage = "Ви успішно підтвердили свою електронну пошту!") {
  return useMutation({
    mutationFn: verify,
    onSuccess: () => toast.success(successMessage),
    onError: (error) => {
      toast.error(getErrorMessage(error, "Невірний або прострочений код."));
    },
  });
}
