import Input from "@/shared/ui/input";
import Button from "@/shared/ui/button";
import styles from "./RegisterForm.module.css";
import { useState } from "react";
import axios from "axios";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import {
  registerSchema,
  type RegisterFormData,
} from "../../model/schemas/registerSchema";
import { useRegister } from "../../model/hooks/useRegister";

import { useNavigate } from "react-router-dom";

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
  });

  const onSubmit = async (data: RegisterFormData) => {
    setServerError("");

    try {
      await registerMutation.mutateAsync(data);
      navigate("/login");
    } catch (error) {
      if (axios.isAxiosError(error)) {
        const responseData = error.response?.data;
        if (typeof responseData === "string") {
          setServerError(responseData);
        } else if (responseData && typeof responseData === "object") {
          const msg = (responseData as { message?: string; detail?: string; error?: string }).message ||
            (responseData as { detail?: string }).detail ||
            (responseData as { error?: string }).error ||
            "Помилка при реєстрації.";
          setServerError(msg);
        } else {
          setServerError("Помилка при реєстрації.");
        }
      }
    }
  };

  return (
    <form className={styles.registerForm} onSubmit={handleSubmit(onSubmit)}>
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
          <Input type="password" {...register("password")} />
          {errors.password && (
            <p className={styles.error}>{errors.password.message}</p>
          )}
        </div>
      </div>

      <Button type="submit">
        {registerMutation.isPending ? "Завантаження..." : "Зареєструватись"}
      </Button>

      {serverError && <p className={styles.error}>{serverError}</p>}
    </form>
  );
}
