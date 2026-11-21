import Button from "@/shared/ui/buttons/button";
import AuthTextField from "../fields/AuthTextField";
import AuthPasswordField from "../fields/AuthPasswordField";
import ServerError from "../fields/ServerError";
import styles from "./RegisterForm.module.css";
import { useState } from "react";
import { useForm } from "react-hook-form";
import { getErrorMessage } from "@/shared/api/getErrorMessage";
import { zodResolver } from "@hookform/resolvers/zod";
import {
  registerSchema,
  type RegisterFormData,
} from "../../model/schemas/registerSchema";
import { useRegister } from "../../model/hooks/useRegister";
import { useNavigate } from "react-router-dom";
import { ArrowRight, Mail, UserRound } from "lucide-react";
import { Text } from "@mantine/core";

const isEmail = (value: string) =>
  /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(value.trim());

export default function RegisterForm() {
  const [serverError, setServerError] = useState("");

  const navigate = useNavigate();
  const registerMutation = useRegister();

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<RegisterFormData>({
    resolver: zodResolver(registerSchema),
    mode: "onChange",
  });

  const onSubmit = async (data: RegisterFormData) => {
    setServerError("");

    try {
      const response = await registerMutation.mutateAsync(data);

      if (
        response.phoneVerificationRequired ||
        response.emailVerificationRequired
      ) {
        const contact = response.emailOrPhone || data.emailOrPhone.trim();
        const isInputEmail = isEmail(contact);

        navigate("/authentication/verify", {
          state: {
            userId: response.id,
            email: isInputEmail ? contact : undefined,
            phone: isInputEmail ? undefined : contact,
            userName: data.userName,
            password: data.password,
          },
        });
      } else {
        navigate("/authentication");
      }
    } catch (error) {
      setServerError(getErrorMessage(error, "Помилка при реєстрації."));
    }
  };

  return (
    <form className={styles.registerForm} onSubmit={handleSubmit(onSubmit)}>
      <AuthTextField
        icon={UserRound}
        placeholder="Нікнейм"
        autoComplete="nickname"
        error={errors.userName?.message}
        {...register("userName")}
      />

      <AuthTextField
        icon={Mail}
        placeholder="Електронна пошта чи телефон"
        autoComplete="username"
        error={errors.emailOrPhone?.message}
        {...register("emailOrPhone")}
      />

      <AuthPasswordField
        placeholder="Пароль"
        autoComplete="new-password"
        error={errors.password?.message}
        {...register("password")}
      />

      <ServerError message={serverError} />

      <Button type="submit" disabled={registerMutation.isPending}>
        {registerMutation.isPending ? (
          "Завантаження..."
        ) : (
          <div className={styles.registerButton}>
            <Text fw={700} fz={15}>
              Зареєструватись
            </Text>
            <ArrowRight size={20} />
          </div>
        )}
      </Button>
    </form>
  );
}
