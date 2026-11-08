import Input from "@/shared/ui/data-display/inputs/input";
import Button from "@/shared/ui/buttons/button";
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
import {
  ArrowRight,
  Eye,
  EyeOff,
  LockIcon,
  Mail,
  UserRound,
} from "lucide-react";
import { Text } from "@mantine/core";

const isEmail = (value: string) =>
  /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(value.trim());

export default function RegisterForm() {
  const [serverError, setServerError] = useState("");
  const [showPassword, setShowPassword] = useState(false);

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
      <div className={styles.inputWrapper}>
        <div className={styles.inputBlock}>
          <UserRound className={styles.icon} />
          <Input {...register("userName")} placeholder="Нікнейм" />
        </div>
        {errors.userName && (
          <p className={styles.error}>{errors.userName.message}</p>
        )}
      </div>

      <div className={styles.inputWrapper}>
        <div className={styles.inputBlock}>
          <Mail className={styles.icon} />
          <Input
            {...register("emailOrPhone")}
            placeholder="Електронна пошта чи телефон"
          />
        </div>
        {errors.emailOrPhone && (
          <p className={styles.error}>{errors.emailOrPhone.message}</p>
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

      {serverError && <p className={styles.serverError}>{serverError}</p>}

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
