import { useState } from "react";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { KeyRound, LockIcon } from "lucide-react";
import { toast } from "sonner";
import clsx from "clsx";
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
  /** Shows a "Cancel" button on the form step. */
  onCancel?: () => void;
  /** Called after the code is confirmed and the password is saved. */
  onSuccess?: () => void;
  submitLabel?: string;
  /** Hint under the "new password" field. */
  showPasswordHint?: boolean;
  className?: string;
};

type Step = "form" | "code";

/**
 * Password change in two steps: old/new password → confirmation code.
 * Rendered inline on the settings page and inside `ChangePasswordModal`.
 */
export default function ChangePasswordForm({
  onCancel,
  onSuccess,
  submitLabel = "Продовжити",
  showPasswordHint = false,
  className,
}: Props) {
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

  const resetState = () => {
    setStep("form");
    setServerError("");
    setShowNewPassword(false);
    reset();
  };

  const handleCancel = () => {
    resetState();
    onCancel?.();
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
      resetState();
      onSuccess?.();
      return true;
    } catch {
      return false;
    }
  };

  if (step === "code") {
    return (
      <div className={className}>
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
      </div>
    );
  }

  return (
    <form
      className={clsx(styles.form, className)}
      onSubmit={handleSubmit(sendCode)}
    >
      <AuthPasswordField
        label="Поточний пароль"
        placeholder="Введіть поточний пароль"
        autoComplete="current-password"
        icon={KeyRound}
        error={errors.oldPassword?.message}
        {...register("oldPassword")}
      />

      <div className={styles.fieldWithHint}>
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
        {showPasswordHint && !errors.newPassword && (
          <p className={styles.hint}>
            Мінімум 8 символів: велика й мала літери, цифра та спеціальний
            символ.
          </p>
        )}
      </div>

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
        {onCancel && (
          <Button type="button" variant="outline" onClick={handleCancel}>
            Скасувати
          </Button>
        )}
        <Button type="submit" loading={changePassword.isPending}>
          {submitLabel}
        </Button>
      </div>
    </form>
  );
}
