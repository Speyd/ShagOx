import styles from "./LoginForm.module.css";
import { useState } from "react";
import { useForm } from "react-hook-form";
import {
  loginSchema,
  type LoginFormData,
} from "../../model/schemas/loginSchema";
import { zodResolver } from "@hookform/resolvers/zod";
import axios from "axios";
import { Link, useNavigate } from "react-router-dom";
import { useLogin } from "../../model/hooks/useLogin";
import { ArrowRight, Eye, EyeOff, LockIcon, Mail } from "lucide-react";

import { Text } from "@mantine/core";
import Input from "@/shared/ui/input";
import Button from "@/shared/ui/button";

export default function LoginForm() {
  const [serverError, setServerError] = useState("");
  const [showPassword, setShowPassword] = useState(false);
  const loginMutation = useLogin();

  const navigate = useNavigate();

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<LoginFormData>({
    resolver: zodResolver(loginSchema),
  });

  const onSubmit = async (data: LoginFormData) => {
    setServerError("");

    try {
      await loginMutation.mutateAsync(data);
      navigate("/");
    } catch (error) {
      if (axios.isAxiosError(error)) {
        const responseData = error.response?.data;
        if (typeof responseData === "string") {
          setServerError(responseData);
        } else if (responseData && typeof responseData === "object") {
          const msg =
            (
              responseData as {
                message?: string;
                detail?: string;
                error?: string;
              }
            ).message ||
            (responseData as { detail?: string }).detail ||
            (responseData as { error?: string }).error ||
            "Невірний логін або пароль.";
          setServerError(msg);
        } else {
          setServerError("Невірний логін або пароль.");
        }
      }
    }
  };

  return (
    <form className={styles.loginForm} onSubmit={handleSubmit(onSubmit)}>
      <div className={styles.inputWrapper}>
        <div className={styles.inputBlock}>
          <Mail className={styles.icon} />
          <Input
            {...register("emailOrPhoneOrUserName")}
            placeholder="Електронна пошта чи телефон"
          />
        </div>
        {errors.emailOrPhoneOrUserName && (
          <p className={styles.error}>
            {errors.emailOrPhoneOrUserName.message}
          </p>
        )}
      </div>

      <div className={styles.inputWrapper}>
        <div className={styles.inputBlock}>
          <LockIcon className={styles.icon} />
          <Input
            {...register("password")}
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
        {errors.password && (
          <p className={styles.error}>{errors.password.message}</p>
        )}
      </div>

      <div className={styles.forgotPasswordWrapper}>
        <Link to="/forgot-password" className={styles.forgotLink}>
          Забули пароль?
        </Link>
      </div>

      {serverError && <p className={styles.serverError}>{serverError}</p>}

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
