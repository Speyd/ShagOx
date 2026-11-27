import { useMutation } from "@tanstack/react-query";
import { toast } from "sonner";
import { verify } from "../../api/api";
import { getErrorMessage } from "@/shared/api/getErrorMessage";

export function useVerify(
  successMessage: string | null = "Ви успішно підтвердили свою електронну пошту!",
) {
  return useMutation({
    mutationFn: verify,
    onSuccess: () => {
      if (successMessage) toast.success(successMessage);
    },
    onError: (error) => {
      toast.error(getErrorMessage(error, "Невірний або прострочений код."));
    },
  });
}
