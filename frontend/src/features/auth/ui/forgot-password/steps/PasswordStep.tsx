import { ArrowRight, Eye, EyeOff, LockIcon } from "lucide-react";
import { useState } from "react";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { Text } from "@mantine/core";
import Input from "@/shared/ui/data-display/inputs/input";
import Button from "@/shared/ui/buttons/button";
import styles from "../ForgotPasswordForm.module.css";
import {
  newPasswordSchema,
  type NewPasswordFormData,
} from "@/features/auth/model/schemas/resetPasswordSchema";

type Props = {
  isSending: boolean;
  error: string;
  onSubmit: (newPassword: string) => void;
};

export default function PasswordStep({ isSending, error, onSubmit }: Props) {
  const [showPassword, setShowPassword] = useState(false);

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<NewPasswordFormData>({
    resolver: zodResolver(newPasswordSchema),
    mode: "onChange",
  });

  return (
    <>
      <div className={styles.description}>
        <Text fz={15} c="var(--text-base)">
          Придумайте новий пароль. Після цього ми надішлемо код підтвердження.
        </Text>
      </div>

      <form
        className={styles.form}
        onSubmit={handleSubmit((data) => onSubmit(data.newPassword))}
      >
        <div className={styles.inputWrapper}>
          <div className={styles.inputBlock}>
            <LockIcon className={styles.icon} />
            <Input
              {...register("newPassword")}
              type={showPassword ? "text" : "password"}
              placeholder="Новий пароль"
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
              type={showPassword ? "text" : "password"}
              placeholder="Повторіть пароль"
            />
          </div>

          {errors.confirmPassword && (
            <Text c="red" fz={12} mt={4}>
              {errors.confirmPassword.message}
            </Text>
          )}
        </div>

        {error && (
          <Text c="red" fz={12}>
            {error}
          </Text>
        )}

        <Button type="submit" loading={isSending}>
          <div className={styles.loginButton}>
            <Text fw={700} fz={15}>
              Надіслати код
            </Text>
            <ArrowRight size={20} />
          </div>
        </Button>
      </form>
    </>
  );
}
