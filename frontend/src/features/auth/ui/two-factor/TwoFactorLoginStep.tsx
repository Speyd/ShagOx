import { useMutation } from "@tanstack/react-query";
import { useNavigate } from "react-router-dom";
import { toast } from "sonner";
import Button from "@/shared/ui/buttons/button";
import { getErrorMessage } from "@/shared/api/getErrorMessage";
import { isTwoFactorRequired, login, verify } from "../../api/api";
import { useLogin } from "../../model/hooks/useLogin";
import type { LoginRequest } from "../../model/types";
import CodeConfirmation from "../code-input/CodeConfirmation";

type Props = {
  userId: number;
  contact?: string | null;
  credentials: LoginRequest;
  onBack: () => void;
};

export default function TwoFactorLoginStep({
  userId,
  contact,
  credentials,
  onBack,
}: Props) {
  const navigate = useNavigate();
  const loginMutation = useLogin();
  const verifyMutation = useMutation({ mutationFn: verify });
  const resendMutation = useMutation({ mutationFn: login });

  const confirm = async (code: string) => {
    try {
      await verifyMutation.mutateAsync({ userId, code });
    } catch (error) {
      toast.error(getErrorMessage(error, "Невірний або прострочений код."));
      return false;
    }

    try {
      const response = await loginMutation.mutateAsync(credentials);

      if (isTwoFactorRequired(response)) {
        toast.error("Не вдалося підтвердити пристрій. Спробуйте ще раз.");
        return false;
      }
    } catch (error) {
      toast.error(
        getErrorMessage(error, "Не вдалося увійти. Спробуйте увійти вручну."),
      );
      onBack();
      return true;
    }

    navigate("/", { replace: true });
    return true;
  };

  const resend = async () => {
    try {
      await resendMutation.mutateAsync(credentials);
      toast.success("Новий код надіслано.");
    } catch (error) {
      toast.error(getErrorMessage(error, "Не вдалося надіслати код."));
      throw error;
    }
  };

  return (
    <CodeConfirmation
      description={
        <>
          Для вашого акаунта увімкнено двофакторну автентифікацію. Ми надіслали
          код підтвердження{contact ? ` на ${contact}` : ""}. Введіть його, щоб
          увійти з цього пристрою.
        </>
      }
      submitLabel="Увійти"
      onSubmit={confirm}
      onResend={resend}
      isSubmitting={verifyMutation.isPending || loginMutation.isPending}
      isResending={resendMutation.isPending}
      secondaryAction={
        <Button
          type="button"
          variant="ghost"
          onClick={onBack}
          disabled={verifyMutation.isPending || loginMutation.isPending}
        >
          Назад
        </Button>
      }
    />
  );
}
