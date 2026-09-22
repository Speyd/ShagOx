import {
  ArrowRight,
  Eye,
  EyeOff,
  LockIcon,
  Mail,
  MoveLeft,
} from "lucide-react";
import { useRef, useState } from "react";
import { useForm } from "react-hook-form";
import { Link, useNavigate } from "react-router-dom";
import { zodResolver } from "@hookform/resolvers/zod";
import { Group, Image, Text } from "@mantine/core";
import { toast } from "sonner";
import Input from "@/shared/ui/input";
import Button from "@/shared/ui/button";
import logo from "@/shared/assets/images/logo.png";
import styles from "./ForgotPasswordForm.module.css";
import { useGetByContact } from "@/entities/user/model/hooks/useGetByContact";
import { useResetPassword } from "../../model/hooks/useResetPassword";
import { useVerify } from "../../model/hooks/useVerify";

import {
  resetPasswordSchema,
  type ResetPasswordFormData,
} from "../../model/schemas/resetPasswordSchema";

type Step = "contact" | "passwords" | "verify";

const CODE_LENGTH = 6;

export default function ForgotPasswordForm() {
  const navigate = useNavigate();

  const [step, setStep] = useState<Step>("contact");
  const [code, setCode] = useState<string[]>(Array(CODE_LENGTH).fill(""));
  const [showPassword, setShowPassword] = useState(false);
  const [showConfirmPassword, setShowConfirmPassword] = useState(false);

  const inputRefs = useRef<(HTMLInputElement | null)[]>([]);

  const {
    register,
    trigger,
    getValues,
    formState: { errors },
  } = useForm<ResetPasswordFormData>({
    resolver: zodResolver(resetPasswordSchema),
    mode: "onChange",
  });

  const contact = getValues("contact");

  const getByContact = useGetByContact(contact || "");
  const passwordReset = useResetPassword();
  const verify = useVerify();

  const handleCheckContact = async () => {
    const isValid = await trigger("contact");

    if (!isValid) return;

    try {
      const { data } = await getByContact.refetch();

      if (!data) {
        toast.error("Користувача не знайдено.");
        return;
      }

      toast.success("Користувача знайдено.");
      setStep("passwords");
    } catch {
      toast.error("Помилка під час пошуку користувача.");
    }
  };

  const handlePasswordSubmit = async () => {
    const isValid = await trigger(["newPassword", "confirmPassword"]);

    if (!isValid) return;

    const userId = getByContact.data?.id;

    if (!userId) {
      toast.error("Користувача не знайдено.");
      return;
    }

    try {
      await passwordReset.mutateAsync({
        userId,
        emailOrPhoneOrUserName: getValues("contact"),
        newPassword: getValues("newPassword"),
      });

      setStep("verify");
    } catch {
      toast.error("Помилка під час зміни пароля.");
    }
  };

  const handleChange = (index: number, value: string) => {
    if (!/^\d*$/.test(value)) return;

    const digit = value.slice(-1);

    setCode((prev) => {
      const next = [...prev];
      next[index] = digit;
      return next;
    });

    if (digit && index < CODE_LENGTH - 1) {
      inputRefs.current[index + 1]?.focus();
    }
  };

  const handleKeyDown = (
    index: number,
    event: React.KeyboardEvent<HTMLInputElement>,
  ) => {
    if (event.key === "Backspace" && !code[index] && index > 0) {
      inputRefs.current[index - 1]?.focus();
    }
  };

  const handlePaste = (event: React.ClipboardEvent<HTMLDivElement>) => {
    event.preventDefault();

    const pastedData = event.clipboardData
      .getData("text")
      .replace(/\D/g, "")
      .slice(0, CODE_LENGTH);

    if (!pastedData) return;

    const digits = pastedData.split("");

    setCode((prev) => {
      const next = [...prev];

      digits.forEach((digit, index) => {
        next[index] = digit;
      });

      return next;
    });

    const focusIndex = Math.min(pastedData.length, CODE_LENGTH - 1);

    inputRefs.current[focusIndex]?.focus();
  };

  const handleVerify = async () => {
    const fullCode = code.join("");

    if (fullCode.length !== CODE_LENGTH) {
      toast.error("Введіть повний код підтвердження.");
      return;
    }

    const userId = getByContact.data?.id;

    if (!userId) {
      toast.error("Користувача не знайдено.");
      return;
    }

    try {
      await verify.mutateAsync({
        userId,
        code: fullCode,
      });

      toast.success("Пароль успішно змінено!");
      navigate("/profile");
    } catch {
      toast.error("Невірний код або помилка зміни пароля.");
    }
  };

  const isVerifying = verify.isPending;

  return (
    <div className={styles.wrapper}>
      <div className={styles.header}>
        <Group gap={5} align="center" wrap="nowrap">
          <Image src={logo} alt="Marketly" w={40} h={40} fit="contain" />

          <Text fz={24} fw={600}>
            Marketly
          </Text>
        </Group>

        <Text fz={24} fw={400} c="var(--text-base)">
          Відновлення пароля
        </Text>
      </div>

      <div className={styles.description}>
        {step === "contact" && (
          <Text fz={15} c="var(--text-base)">
            Введіть електронну пошту чи телефон. Ми надішлемо вам код
            підтвердження.
          </Text>
        )}

        {step === "passwords" && (
          <Text fz={15} c="var(--text-base)">
            Придумайте новий пароль та повторіть його.
          </Text>
        )}

        {step === "verify" && (
          <Text fz={15} c="var(--text-base)">
            Введіть код підтвердження .
          </Text>
        )}
      </div>

      {step === "contact" && (
        <form
          className={styles.form}
          onSubmit={(event) => {
            event.preventDefault();
            handleCheckContact();
          }}
        >
          <div className={styles.inputWrapper}>
            <div className={styles.inputBlock}>
              <Mail className={styles.icon} />

              <Input
                placeholder="Електронна пошта чи телефон"
                {...register("contact")}
              />
            </div>

            {errors.contact && (
              <Text c="red" fz={12} mt={4}>
                {errors.contact.message}
              </Text>
            )}
          </div>

          <Button type="submit" className={styles.button}>
            <div className={styles.loginButton}>
              <Text fw={700} fz={15}>
                Далі
              </Text>

              <ArrowRight size={20} />
            </div>
          </Button>

          <div className={styles.guestWrapper}>
            <Link to="/authentication" className={styles.guestLink}>
              <MoveLeft size={20} />
              <Text fw={700} fz={12}>
                Повернутись до входу
              </Text>
            </Link>
          </div>
        </form>
      )}

      {step === "passwords" && (
        <form
          className={styles.form}
          onSubmit={(event) => {
            event.preventDefault();
            handlePasswordSubmit();
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
      )}

      {step === "verify" && (
        <form
          className={styles.form}
          onSubmit={(event) => {
            event.preventDefault();
            handleVerify();
          }}
        >
          <div className={styles.codeContainer} onPaste={handlePaste}>
            {code.map((digit, index) => (
              <input
                key={index}
                ref={(element) => {
                  inputRefs.current[index] = element;
                }}
                type="text"
                inputMode="numeric"
                maxLength={1}
                value={digit}
                disabled={isVerifying}
                onChange={(event) => handleChange(index, event.target.value)}
                onKeyDown={(event) => handleKeyDown(index, event)}
                className={`${styles.input} ${digit ? styles.inputFilled : ""}`}
              />
            ))}
          </div>

          <Button
            type="submit"
            disabled={code.join("").length !== CODE_LENGTH || isVerifying}
            className={styles.submitBtn}
          >
            <div className={styles.verifyButton}>
              <Text fw={700} fz={15}>
                {isVerifying ? "Перевірка..." : "Підтвердити"}
              </Text>

              <ArrowRight size={20} />
            </div>
          </Button>
        </form>
      )}
    </div>
  );
}
