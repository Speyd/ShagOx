import Input from "@/shared/ui/Input";
import Button from "@/shared/ui/Button";
import styles from "./RegisterForm.module.css";
import { useState } from "react";
import axios from "axios";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { useRegister } from "../../hooks/useRegister";
import {
  registerSchema,
  type RegisterFormData,
} from "../../model/registerSchema";

export default function RegisterForm() {
  const [serverError, setServerError] = useState("");

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
    } catch (error) {
      if (axios.isAxiosError(error)) {
        setServerError(String(error.response?.data));
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
