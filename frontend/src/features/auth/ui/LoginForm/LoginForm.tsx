import Input from "@/shared/ui/input";
import styles from "./LoginForm.module.css";
import Button from "@/shared/ui/button";
import { useState } from "react";
import { useForm } from "react-hook-form";
import {
  loginSchema,
  type LoginFormData,
} from "../../model/schemas/loginSchema";
import { zodResolver } from "@hookform/resolvers/zod";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import { useLogin } from "../../model/hooks/useLogin";

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
          const msg = (responseData as { message?: string; detail?: string; error?: string }).message ||
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
        <label className={styles.label}>Електронна пошта чи телефон</label>

        <div>
          <Input {...register("emailOrPhone")} />

          {errors.emailOrPhone && (
            <p className={styles.error}>{errors.emailOrPhone.message}</p>
          )}
        </div>
      </div>

      <div className={styles.inputWrapper}>
        <label className={styles.label}>Пароль</label>

        <div>
          <Input {...register("password")} type="password" />

          {errors.password && (
            <p className={styles.error}>{errors.password.message}</p>
          )}
        </div>
      </div>

      <Button type="submit">
        {loginMutation.isPending ? "Завантаження..." : "Вхід"}
      </Button>

      {serverError && <p className={styles.error}>{serverError}</p>}
    </form>
  );
}
