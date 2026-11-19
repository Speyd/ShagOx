import { ArrowRight } from "lucide-react";
import { useState } from "react";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { Text } from "@mantine/core";
import Button from "@/shared/ui/buttons/button";
import styles from "../ForgotPasswordForm.module.css";
import {
  newPasswordSchema,
  type NewPasswordFormData,
} from "@/features/auth/model/schemas/resetPasswordSchema";
import AuthPasswordField from "../../fields/AuthPasswordField";
import ServerError from "../../fields/ServerError";

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
        <AuthPasswordField
          placeholder="Новий пароль"
          autoComplete="new-password"
          visible={showPassword}
          onVisibleChange={setShowPassword}
          error={errors.newPassword?.message}
          {...register("newPassword")}
        />

        <AuthPasswordField
          placeholder="Повторіть пароль"
          autoComplete="new-password"
          visible={showPassword}
          hideToggle
          error={errors.confirmPassword?.message}
          {...register("confirmPassword")}
        />

        <ServerError message={error} />

        <Button
          type="submit"
          loading={isSending}
          rightSection={ArrowRight}
          iconProps={{ size: 20 }}
        >
          Надіслати код
        </Button>
      </form>
    </>
  );
}
