import { useNavigate } from "react-router-dom";
import { Group, Image, Text } from "@mantine/core";
import { toast } from "sonner";
import logo from "@/shared/assets/images/logo.png";
import { getErrorMessage } from "@/shared/api/getErrorMessage";
import { useVerify } from "../../model/hooks/useVerify";
import { useLogin } from "../../model/hooks/useLogin";
import { useRegister } from "../../model/hooks/useRegister";
import CodeConfirmation from "../code-input/CodeConfirmation";
import styles from "./VerifyForm.module.css";

type VerifyFormProps = {
  email?: string;
  phone?: string;
  password?: string;
  userName?: string;
  userId?: number;
  onVerifySuccess?: () => void;
};

export default function VerifyForm({
  email,
  phone,
  userId,
  password,
  userName,
  onVerifySuccess,
}: VerifyFormProps) {
  const navigate = useNavigate();
  const loginMutation = useLogin();
  const registerMutation = useRegister();
  const verify = useVerify("Акаунт успішно підтверджено!");

  const contact = email ?? phone;

  const handleSubmit = async (code: string) => {
    if (userId == null && !email) {
      toast.error("Не вдалося визначити користувача або email.");
      return false;
    }

    if (userId != null) {
      try {
        await verify.mutateAsync({ userId, code });
      } catch {
        return false;
      }
    }

    if (password && contact) {
      try {
        await loginMutation.mutateAsync({
          emailOrPhoneOrUserName: contact,
          password,
        });
      } catch (error) {
        toast.error(
          getErrorMessage(error, "Не вдалося увійти. Спробуйте увійти вручну."),
        );
        navigate("/authentication", { replace: true });
        return true;
      }
    }

    if (onVerifySuccess) onVerifySuccess();
    else navigate("/", { replace: true });

    return true;
  };

  const handleResend = async () => {
    if (!contact || !userName || !password) {
      toast.error(
        "Не вдалося надіслати код повторно. Пройдіть реєстрацію ще раз.",
      );
      navigate("/authentication", { replace: true });
      throw new Error("Missing registration data");
    }

    try {
      await registerMutation.mutateAsync({
        emailOrPhone: contact,
        userName,
        password,
      });
      toast.success("Новий код надіслано.");
    } catch (error) {
      toast.error(getErrorMessage(error, "Не вдалося надіслати код повторно."));
      throw error;
    }
  };

  return (
    <div className={styles.wrapper}>
      <div className={styles.header}>
        <Group gap={5} align="center" wrap="nowrap">
          <Image
            src={logo}
            alt="Marketly"
            w={40}
            h={40}
            fit="contain"
            style={{ flexShrink: 0 }}
          />
          <Text fz={24} fw={600}>
            Marketly
          </Text>
        </Group>

        <Text fz={24} fw={400} c="var(--text-base)">
          Підтвердження реєстрації
        </Text>
      </div>

      <CodeConfirmation
        description={
          <>
            Ми надіслали код підтвердження на{" "}
            {phone ? "ваш номер телефону" : "вашу електронну пошту"}
            {contact ? ` (${contact})` : ""}. Будь ласка, введіть його нижче.
          </>
        }
        onSubmit={handleSubmit}
        onResend={handleResend}
        isSubmitting={verify.isPending || loginMutation.isPending}
        isResending={registerMutation.isPending}
      />
    </div>
  );
}
