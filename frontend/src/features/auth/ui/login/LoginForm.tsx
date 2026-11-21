import styles from "./LoginForm.module.css";
import { useState } from "react";
import { useForm } from "react-hook-form";
import {
  loginSchema,
  type LoginFormData,
} from "../../model/schemas/loginSchema";
import { zodResolver } from "@hookform/resolvers/zod";
import { getErrorMessage } from "@/shared/api/getErrorMessage";
import { Link, useNavigate } from "react-router-dom";
import { ArrowRight, Mail } from "lucide-react";

import { Text } from "@mantine/core";
import Button from "@/shared/ui/buttons/button";
import AuthTextField from "../fields/AuthTextField";
import AuthPasswordField from "../fields/AuthPasswordField";
import ServerError from "../fields/ServerError";
import { useLogin } from "../../model/hooks/useLogin";
import { register as registerUser } from "../../api/api";
import { getByContanct } from "@/entities/user/api/api";
import axios from "axios";
import { toast } from "sonner";

export default function LoginForm() {
  const [serverError, setServerError] = useState("");
  const loginMutation = useLogin();

  const navigate = useNavigate();

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<LoginFormData>({
    resolver: zodResolver(loginSchema),
    mode: "onChange",
  });

  const goToVerification = async (data: LoginFormData) => {
    const user = await getByContanct(data.emailOrPhoneOrUserName);
    const contact = user.email ?? user.phone ?? "";

    await registerUser({
      emailOrPhone: contact,
      userName: user.userName,
      password: data.password,
    });

    toast.info("Підтвердіть акаунт. Ми надіслали вам новий код.");

    navigate("/authentication/verify", {
      state: {
        userId: user.id,
        email: user.email ?? undefined,
        phone: user.email ? undefined : (user.phone ?? undefined),
        userName: user.userName,
        password: data.password,
      },
    });
  };

  const onSubmit = async (data: LoginFormData) => {
    setServerError("");

    try {
      await loginMutation.mutateAsync(data);
      navigate("/");
    } catch (error) {
      const isNotVerified =
        axios.isAxiosError(error) && error.response?.status === 403;

      if (!isNotVerified) {
        setServerError(getErrorMessage(error, "Невірний логін або пароль."));
        return;
      }

      try {
        await goToVerification(data);
      } catch {
        setServerError(getErrorMessage(error, "Акаунт не підтверджено."));
      }
    }
  };

  return (
    <form className={styles.loginForm} onSubmit={handleSubmit(onSubmit)}>
      <AuthTextField
        icon={Mail}
        placeholder="Електронна пошта, телефон або нікнейм"
        autoComplete="username"
        error={errors.emailOrPhoneOrUserName?.message}
        {...register("emailOrPhoneOrUserName")}
      />

      <AuthPasswordField
        placeholder="Пароль"
        autoComplete="current-password"
        error={errors.password?.message}
        {...register("password")}
      />

      <div className={styles.forgotPasswordWrapper}>
        <Link to="/forgot-password" className={styles.forgotLink}>
          Забули пароль?
        </Link>
      </div>

      <ServerError message={serverError} />

      <Button type="submit" disabled={loginMutation.isPending}>
        {loginMutation.isPending ? (
          "Завантаження..."
        ) : (
          <div className={styles.loginButton}>
            <Text fw={700} fz={15}>
              Увійти
            </Text>
            <ArrowRight size={20} />
          </div>
        )}
      </Button>
    </form>
  );
}
