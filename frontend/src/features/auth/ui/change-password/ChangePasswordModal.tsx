import { useState } from "react";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { Modal, PinInput, Text } from "@mantine/core";
import { toast } from "sonner";
import Button from "@/shared/ui/buttons/button";
import { FormField, TextField } from "@/shared/ui/form";
import { getErrorMessage } from "@/shared/api/getErrorMessage";
import { useChangePassword } from "../../model/hooks/useChangePassword";
import { useVerify } from "../../model/hooks/useVerify";
import {
  changePasswordSchema,
  type ChangePasswordFormData,
} from "../../model/schemas/changePasswordSchema";
import styles from "./ChangePasswordModal.module.css";

type Props = {
  opened: boolean;
  onClose: () => void;
};

const CODE_LENGTH = 6;

export default function ChangePasswordModal({ opened, onClose }: Props) {
  const [step, setStep] = useState<"form" | "code">("form");
  const [code, setCode] = useState("");
  const [serverError, setServerError] = useState("");

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
    setCode("");
    setServerError("");
    reset();
    onClose();
  };

  const sendCode = async () => {
    setServerError("");

    try {
      await changePassword.mutateAsync({
        oldPassword: getValues("oldPassword"),
        newPassword: getValues("newPassword"),
      });

      setCode("");
      setStep("code");
      toast.success("Код підтвердження надіслано.");
    } catch (error) {
      setServerError(getErrorMessage(error, "Не вдалося змінити пароль."));
    }
  };

  const confirmCode = async () => {
    if (code.length !== CODE_LENGTH) return;

    try {
      await verify.mutateAsync({ code });
      handleClose();
    } catch {
      setCode("");
    }
  };

  return (
    <Modal
      opened={opened}
      onClose={handleClose}
      title="Зміна пароля"
      centered
      radius="lg"
      zIndex={1100}
    >
      {step === "form" && (
        <form className={styles.form} onSubmit={handleSubmit(sendCode)}>
          <FormField
            label="Поточний пароль"
            htmlFor="oldPassword"
            error={errors.oldPassword?.message}
          >
            <TextField
              id="oldPassword"
              type="password"
              autoComplete="current-password"
              invalid={!!errors.oldPassword}
              {...register("oldPassword")}
            />
          </FormField>

          <FormField
            label="Новий пароль"
            htmlFor="newPassword"
            error={errors.newPassword?.message}
          >
            <TextField
              id="newPassword"
              type="password"
              autoComplete="new-password"
              invalid={!!errors.newPassword}
              {...register("newPassword")}
            />
          </FormField>

          <FormField
            label="Повторіть новий пароль"
            htmlFor="confirmPassword"
            error={errors.confirmPassword?.message}
          >
            <TextField
              id="confirmPassword"
              type="password"
              autoComplete="new-password"
              invalid={!!errors.confirmPassword}
              {...register("confirmPassword")}
            />
          </FormField>

          {serverError && (
            <Text c="red.7" fz={13}>
              {serverError}
            </Text>
          )}

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
        <div className={styles.form}>
          <Text fz={14}>
            Ми надіслали код підтвердження на вашу пошту або телефон. Введіть
            його, щоб зберегти новий пароль.
          </Text>

          <div className={styles.code}>
            <PinInput
              length={CODE_LENGTH}
              type="number"
              oneTimeCode
              size="lg"
              value={code}
              onChange={setCode}
              onComplete={setCode}
              disabled={verify.isPending}
            />
          </div>

          <div className={styles.actions}>
            <Button
              type="button"
              variant="ghost"
              onClick={sendCode}
              loading={changePassword.isPending}
            >
              Надіслати код ще раз
            </Button>
            <Button
              type="button"
              onClick={confirmCode}
              disabled={code.length !== CODE_LENGTH}
              loading={verify.isPending}
            >
              Підтвердити
            </Button>
          </div>
        </div>
      )}
    </Modal>
  );
}
