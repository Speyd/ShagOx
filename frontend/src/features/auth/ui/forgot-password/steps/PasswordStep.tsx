import { ArrowRight, Eye, EyeOff, LockIcon } from "lucide-react";
import { useState } from "react";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { Text } from "@mantine/core";
import { toast } from "sonner";
import Input from "@/shared/ui/input";
import Button from "@/shared/ui/button";
import styles from "../ForgotPasswordForm.module.css";
import {
  resetPasswordSchema,
  type ResetPasswordFormData,
} from "@/features/auth/model/schemas/resetPasswordSchema";
import { useResetPassword } from "@/features/auth/model/hooks/useResetPassword";


type Props = {
  userId: number;
  onSuccess: () => void;
};

export default function PasswordStep({ userId, onSuccess }: Props) {
  const [showPassword, setShowPassword] = useState(false);
  const [showConfirmPassword, setShowConfirmPassword] = useState(false);

  const {
    register,
    trigger,
    getValues,
    formState: { errors },
  } = useForm<ResetPasswordFormData>({
    resolver: zodResolver(resetPasswordSchema),
    mode: "onChange",
  });

  const passwordReset = useResetPassword();

  const handleSubmit = async () => {
    const isValid = await trigger(["newPassword", "confirmPassword"]);

    if (!isValid) return;

    try {
      await passwordReset.mutateAsync({
        userId,
        emailOrPhoneOrUserName: getValues("contact"),
        newPassword: getValues("newPassword"),
      });

      onSuccess();
    } catch {
      toast.error("Помилка під час зміни пароля.");
    }
  };

  return (
    <>
      <div className={styles.description}>
        <Text fz={15} c="var(--text-base)">
          Придумайте новий пароль та повторіть його.
        </Text>
      </div>

      <form
        className={styles.form}
        onSubmit={(event) => {
          event.preventDefault();
          handleSubmit();
        }}
      >
        <div className={styles.inputWrapper}>
          <div className={styles.inputBlock}>
            <LockIcon className={styles.icon} />

            <Input
              {...register("newPassword")}
              type={showPassword ? "text" : "password"}
              placeholder="Пароль"
            />

            <button
              type="button"
              onClick={() => setShowPassword(!showPassword)}
              className={styles.eyeButton}
              tabIndex={-1}
            >
              {showPassword ? (
                <Eye className={styles.eyeIcon} />
              ) : (
                <EyeOff className={styles.eyeIcon} />
              )}
            </button>
          </div>

          {errors.newPassword && (
            <Text c="red" fz={12} mt={4}>
              {errors.newPassword.message}
            </Text>
          )}
        </div>

        <div className={styles.inputWrapper}>
          <div className={styles.inputBlock}>
            <LockIcon className={styles.icon} />

            <Input
              {...register("confirmPassword")}
              type={showConfirmPassword ? "text" : "password"}
              placeholder="Пароль"
            />

            <button
              type="button"
              onClick={() => setShowConfirmPassword(!showConfirmPassword)}
              className={styles.eyeButton}
              tabIndex={-1}
            >
              {showConfirmPassword ? (
                <Eye className={styles.eyeIcon} />
              ) : (
                <EyeOff className={styles.eyeIcon} />
              )}
            </button>
          </div>

          {errors.confirmPassword && (
            <Text c="red" fz={12} mt={4}>
              {errors.confirmPassword.message}
            </Text>
          )}
        </div>

        <Button
          type="submit"
          className={styles.button}
          disabled={passwordReset.isPending}
        >
          <div className={styles.loginButton}>
            <Text fw={700} fz={15}>
              {passwordReset.isPending ? "Надсилання..." : "Надіслати код"}
            </Text>

            <ArrowRight size={20} />
          </div>
        </Button>
      </form>
    </>
  );
}
