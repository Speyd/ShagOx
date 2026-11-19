import { useState } from "react";
import { Group, Image, Text } from "@mantine/core";
import { toast } from "sonner";
import logo from "@/shared/assets/images/logo.png";
import { getErrorMessage } from "@/shared/api/getErrorMessage";
import styles from "./ForgotPasswordForm.module.css";
import { useResetPassword } from "../../model/hooks/useResetPassword";
import ContactStep from "./steps/ContactStep";
import PasswordStep from "./steps/PasswordStep";
import VerifyStep from "./steps/VerifyStep";

type Step = "contact" | "password" | "verify";

export default function ForgotPasswordForm() {
  const [step, setStep] = useState<Step>("contact");
  const [userId, setUserId] = useState(0);
  const [contact, setContact] = useState("");
  const [newPassword, setNewPassword] = useState("");
  const [passwordError, setPasswordError] = useState("");

  const resetPassword = useResetPassword();

  const handleContactFound = (foundUserId: number, foundContact: string) => {
    setUserId(foundUserId);
    setContact(foundContact);
    setStep("password");
  };

  const sendCode = async (password: string) => {
    setPasswordError("");

    try {
      await resetPassword.mutateAsync({
        userId,
        emailOrPhoneOrUserName: contact,
        newPassword: password,
      });

      setNewPassword(password);
      setStep("verify");
      toast.success("Код підтвердження надіслано.");
    } catch (error) {
      const message = getErrorMessage(error, "Не вдалося надіслати код.");
      setPasswordError(message);
      toast.error(message);
    }
  };

  return (
    <div className={styles.wrapper}>
      <div className={styles.header}>
        <Group gap={5} align="center" wrap="nowrap">
          <Image src={logo} alt="Marketly" w={40} h={40} fit="contain" />
          <Text fz={24} fw={600}>
            Marketly
          </Text>
        </Group>

        <Text fz={24} fw={400} c="var(--text-base)">
          Відновлення пароля
        </Text>
      </div>

      {step === "contact" && <ContactStep onSuccess={handleContactFound} />}

      {step === "password" && (
        <PasswordStep
          isSending={resetPassword.isPending}
          error={passwordError}
          onSubmit={sendCode}
        />
      )}

      {step === "verify" && (
        <VerifyStep
          userId={userId}
          contact={contact}
          isResending={resetPassword.isPending}
          onResend={() => sendCode(newPassword)}
        />
      )}
    </div>
  );
}
