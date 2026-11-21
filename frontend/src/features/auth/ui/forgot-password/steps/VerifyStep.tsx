import { useNavigate } from "react-router-dom";
import { useVerify } from "@/features/auth/model/hooks/useVerify";
import CodeConfirmation from "../../code-input/CodeConfirmation";

type Props = {
  userId: number;
  contact: string;
  isResending: boolean;
  onResend: () => Promise<void>;
};

export default function VerifyStep({
  userId,
  contact,
  isResending,
  onResend,
}: Props) {
  const navigate = useNavigate();
  const verify = useVerify("Пароль змінено! Увійдіть з новим паролем.");

  const handleSubmit = async (code: string) => {
    try {
      await verify.mutateAsync({ userId, code });
      navigate("/authentication", { replace: true });
      return true;
    } catch {
      return false;
    }
  };

  return (
    <CodeConfirmation
      description={`Ми надіслали код підтвердження на ${contact}. Введіть його нижче.`}
      submitLabel="Змінити пароль"
      onSubmit={handleSubmit}
      onResend={onResend}
      isSubmitting={verify.isPending}
      isResending={isResending}
    />
  );
}
