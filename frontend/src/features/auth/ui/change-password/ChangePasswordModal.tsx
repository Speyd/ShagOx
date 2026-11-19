import { useState } from "react";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { KeyRound, LockIcon } from "lucide-react";
import { Modal, Text } from "@mantine/core";
import { toast } from "sonner";
import Button from "@/shared/ui/buttons/button";
import { getErrorMessage } from "@/shared/api/getErrorMessage";
import { useChangePassword } from "../../model/hooks/useChangePassword";
import { useVerify } from "../../model/hooks/useVerify";
import {
  changePasswordSchema,
  type ChangePasswordFormData,
} from "../../model/schemas/changePasswordSchema";
import AuthPasswordField from "../fields/AuthPasswordField";
import ServerError from "../fields/ServerError";
import CodeConfirmation from "../code-input/CodeConfirmation";
import styles from "./ChangePasswordModal.module.css";

type Props = {
  opened: boolean;
  onClose: () => void;
};

type Step = "form" | "code";

export default function ChangePasswordModal({ opened, onClose }: Props) {
  const [step, setStep] = useState<Step>("form");
  const [serverError, setServerError] = useState("");
  const [showNewPassword, setShowNewPassword] = useState(false);

  const changePassword = useChangePassword();
  const verify = useVerify("Пароль успішно змінено!");

  const {
    register,
    handleSubmit,
    getValues,
    reset,
    formState: { errors },
  } = useForm<ChangePasswordFormData>({
    resolver: zodResolver(changePasswordSchema),
    mode: "onChange",
  });

  const handleClose = () => {
    setStep("form");
    setServerError("");
    setShowNewPassword(false);
    reset();
    onClose();
  };

  const requestCode = () =>
    changePassword.mutateAsync({
      oldPassword: getValues("oldPassword"),
      newPassword: getValues("newPassword"),
    });

  const sendCode = async () => {
    setServerError("");

    try {
      await requestCode();
      setStep("code");
      toast.success("Код підтвердження надіслано.");
    } catch (error) {
      setServerError(getErrorMessage(error, "Не вдалося змінити пароль."));
    }
  };

  const resendCode = async () => {
    try {
      await requestCode();
      toast.success("Новий код надіслано.");
    } catch (error) {
      toast.error(getErrorMessage(error, "Не вдалося надіслати код."));
      throw error;
    }
  };

  const confirmCode = async (code: string) => {
    try {
      await verify.mutateAsync({ code });
      handleClose();
      return true;
    } catch {
      return false;
    }
  };

  return (
    <Modal
      opened={opened}
      onClose={handleClose}
      title={
        <Text fz={20} fw={600}>
          {step === "form" ? "Зміна пароля" : "Підтвердження"}
        </Text>
      }
      centered
      radius="lg"
      size="md"
      padding="xl"
      zIndex={1100}
    >
      {step === "form" && (
        <form className={styles.form} onSubmit={handleSubmit(sendCode)}>
          <Text fz={14} c="dimmed">
            Після збереження ми надішлемо код підтвердження на вашу пошту.
          </Text>

          <AuthPasswordField
            label="Поточний пароль"
            placeholder="Введіть поточний пароль"
            autoComplete="current-password"
            icon={KeyRound}
            error={errors.oldPassword?.message}
            {...register("oldPassword")}
          />

          <AuthPasswordField
            label="Новий пароль"
            placeholder="Щонайменше 8 символів"
            autoComplete="new-password"
            icon={LockIcon}
            visible={showNewPassword}
            onVisibleChange={setShowNewPassword}
            error={errors.newPassword?.message}
            {...register("newPassword")}
          />

          <AuthPasswordField
            label="Повторіть новий пароль"
            placeholder="Повторіть новий пароль"
            autoComplete="new-password"
            icon={LockIcon}
            visible={showNewPassword}
            hideToggle
            error={errors.confirmPassword?.message}
            {...register("confirmPassword")}
          />

          <ServerError message={serverError} />

          <div className={styles.actions}>
            <Button type="button" variant="outline" onClick={handleClose}>
              Скасувати
            </Button>
            <Button type="submit" loading={changePassword.isPending}>
              Продовжити
            </Button>
          </div>
        </form>
      )}

      {step === "code" && (
        <CodeConfirmation
          description="Ми надіслали код підтвердження на вашу пошту або телефон. Введіть його, щоб зберегти новий пароль."
          submitLabel="Зберегти пароль"
          onSubmit={confirmCode}
          onResend={resendCode}
          isSubmitting={verify.isPending}
          isResending={changePassword.isPending}
          secondaryAction={
            <Button
              type="button"
              variant="ghost"
              onClick={() => setStep("form")}
              disabled={verify.isPending}
            >
              Назад
            </Button>
          }
        />
      )}
    </Modal>
  );
}
