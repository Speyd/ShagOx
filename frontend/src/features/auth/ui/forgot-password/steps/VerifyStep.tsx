import { ArrowRight } from "lucide-react";
import { useRef, useState } from "react";
import { useNavigate } from "react-router-dom";
import { Text } from "@mantine/core";
import Button from "@/shared/ui/buttons/button";
import styles from "../ForgotPasswordForm.module.css";
import { useVerify } from "@/features/auth/model/hooks/useVerify";

const CODE_LENGTH = 6;

type Props = {
  userId: number;
  contact: string;
  isResending: boolean;
  onResend: () => void;
};

export default function VerifyStep({
  userId,
  contact,
  isResending,
  onResend,
}: Props) {
  const navigate = useNavigate();
  const [code, setCode] = useState<string[]>(Array(CODE_LENGTH).fill(""));
  const inputRefs = useRef<(HTMLInputElement | null)[]>([]);

  const verify = useVerify("Пароль змінено! Увійдіть з новим паролем.");

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

    const digits = event.clipboardData
      .getData("text")
      .replace(/\D/g, "")
      .slice(0, CODE_LENGTH)
      .split("");

    if (digits.length === 0) return;

    setCode((prev) => prev.map((digit, index) => digits[index] ?? digit));
    inputRefs.current[Math.min(digits.length, CODE_LENGTH - 1)]?.focus();
  };

  const handleSubmit = async (event: React.FormEvent) => {
    event.preventDefault();

    const fullCode = code.join("");
    if (fullCode.length !== CODE_LENGTH) return;

    try {
      await verify.mutateAsync({ userId, code: fullCode });
      navigate("/authentication", { replace: true });
    } catch {
      setCode(Array(CODE_LENGTH).fill(""));
      inputRefs.current[0]?.focus();
    }
  };

  const handleResend = () => {
    setCode(Array(CODE_LENGTH).fill(""));
    onResend();
  };

  return (
    <>
      <div className={styles.description}>
        <Text fz={15} c="var(--text-base)">
          Ми надіслали код підтвердження на {contact}. Введіть його нижче.
        </Text>
      </div>

      <form className={styles.form} onSubmit={handleSubmit}>
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
              disabled={verify.isPending}
              onChange={(event) => handleChange(index, event.target.value)}
              onKeyDown={(event) => handleKeyDown(index, event)}
              className={`${styles.input} ${digit ? styles.inputFilled : ""}`}
            />
          ))}
        </div>

        <Button
          type="submit"
          disabled={code.join("").length !== CODE_LENGTH}
          loading={verify.isPending}
        >
          <div className={styles.verifyButton}>
            <Text fw={700} fz={15}>
              Підтвердити
            </Text>
            <ArrowRight size={20} />
          </div>
        </Button>

        <Button
          type="button"
          variant="ghost"
          onClick={handleResend}
          loading={isResending}
        >
          Надіслати код ще раз
        </Button>
      </form>
    </>
  );
}
