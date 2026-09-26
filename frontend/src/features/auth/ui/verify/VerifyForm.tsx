import React, { useState, useRef, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { Group, Image, Text } from "@mantine/core";
import styles from "./VerifyForm.module.css";
import logo from "@/shared/assets/images/logo.png";
import { ArrowRight } from "lucide-react";
import Button from "@/shared/ui/button";
import { useVerify } from "@/features/auth/model/hooks/useVerify";
import { toast } from "sonner";
import { useLogin } from "../../model/hooks/useLogin";

type VerifyFormProps = {
  email?: string;
  phone?: string;
  password?: string;
  userId?: number;
  onVerifySuccess?: () => void;
};

export default function VerifyForm({
  email,
  phone,
  userId,
  password,
  onVerifySuccess,
}: VerifyFormProps) {
  const [code, setCode] = useState<string[]>(Array(6).fill(""));
  const [resendTimer, setResendTimer] = useState(60);
  const inputRefs = useRef<(HTMLInputElement | null)[]>([]);
  const loginMutation = useLogin();

  const navigate = useNavigate();

  const { mutateAsync: verifyEmail, isPending } = useVerify();

  useEffect(() => {
    if (resendTimer <= 0) return;
    const interval = setInterval(() => {
      setResendTimer((prev) => prev - 1);
    }, 1000);
    return () => clearInterval(interval);
  }, [resendTimer]);

  const handleChange = (index: number, value: string) => {
    if (!/^\d*$/.test(value)) return;

    const newCode = [...code];
    newCode[index] = value.slice(-1);
    setCode(newCode);

    if (value && index < 5) {
      inputRefs.current[index + 1]?.focus();
    }
  };

  const handleKeyDown = (
    index: number,
    e: React.KeyboardEvent<HTMLInputElement>,
  ) => {
    if (e.key === "Backspace" && !code[index] && index > 0) {
      inputRefs.current[index - 1]?.focus();
    }
  };

  const handlePaste = (e: React.ClipboardEvent) => {
    e.preventDefault();
    const pastedData = e.clipboardData.getData("text").trim();
    if (/^\d{6}$/.test(pastedData)) {
      const digits = pastedData.split("");
      setCode(digits);
      inputRefs.current[5]?.focus();
    }
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    const fullCode = code.join("");

    if (fullCode.length !== 6) return;

    if (userId == null && !email) {
      toast.error("Не вдалося визначити користувача або email.");
      return;
    }

    try {
      if (userId != null) {
        await verifyEmail({ userId, code: fullCode });
      } else if (email) {
        // Потік відновлення пароля: викликаємо відповідний хук/ендпоінт
        // await confirmResetPassword({ email, code: fullCode });
      }

      const loginIdentifier = email ?? phone;
      if (password && loginIdentifier) {
        await loginMutation.mutateAsync({
          emailOrPhoneOrUserName: loginIdentifier,
          password,
        });
      }

      if (onVerifySuccess) {
        onVerifySuccess();
      } else {
        navigate("/", { replace: true });
      }
    } catch (err) {
      console.error("Помилка підтвердження", err);
    }
  };

  const handleResendCode = async () => {
    if (resendTimer > 0) return;
    try {
      setResendTimer(60);
      setCode(Array(6).fill(""));
      inputRefs.current[0]?.focus();
    } catch (err) {
      console.error("Не вдалося відправити код повторно", err);
    }
  };

  return (
    <div className={styles.wrapper}>
      <div className={styles.header}>
        <Group gap={5} align="center" wrap="nowrap">
          <Image
            src={logo}
            alt="Marketly"
            w={40}
            h={40}
            fit="contain"
            style={{ flexShrink: 0 }}
          />
          <Text fz={24} fw={600}>
            Marketly
          </Text>
        </Group>

        <Text fz={24} fw={400} c="var(--text-base)">
          Підтвердження реєстрації
        </Text>

        <Text
          fz={14}
          fw={400}
          c="var(--text-base)"
          className={styles.description}
        >
          Ми надіслали код підтвердження на{" "}
          {phone ? "ваш номер телефону" : "вашу електронну пошту"}
          {email || phone ? ` (${email ?? phone})` : ""}. Будь ласка, введіть
          його нижче.
        </Text>
      </div>

      <form onSubmit={handleSubmit} className={styles.form}>
        <div className={styles.codeContainer} onPaste={handlePaste}>
          {code.map((digit, index) => (
            <input
              key={index}
              ref={(el) => {
                inputRefs.current[index] = el;
              }}
              type="text"
              inputMode="numeric"
              maxLength={1}
              value={digit}
              onChange={(e) => handleChange(index, e.target.value)}
              onKeyDown={(e) => handleKeyDown(index, e)}
              className={`${styles.input} ${digit ? styles.inputFilled : ""}`}
              disabled={isPending || loginMutation.isPending}
            />
          ))}
        </div>

        <Button
          type="submit"
          disabled={
            code.join("").length !== 6 ||
            isPending ||
            loginMutation.isPending
          }
          className={styles.submitBtn}
        >
          <div className={styles.verifyButton}>
            <Text fw={700} fz={15}>
              Підтвердити
            </Text>
            <ArrowRight size={20} />
          </div>
        </Button>
      </form>

      <div className={styles.resendBox}>
        {resendTimer > 0 ? (
          <Text fz={14} c="dimmed">
            Надіслати код повторно через <strong>{resendTimer}с</strong>
          </Text>
        ) : (
          <Button
            onClick={handleResendCode}
            disabled={isPending || loginMutation.isPending}
          >
            Надіслати код повторно
          </Button>
        )}
      </div>
    </div>
  );
}
