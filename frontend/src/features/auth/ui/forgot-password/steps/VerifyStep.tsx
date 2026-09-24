import { ArrowRight } from "lucide-react";
import { useRef, useState } from "react";
import { useNavigate } from "react-router-dom";
import { Text } from "@mantine/core";
import { toast } from "sonner";
import Button from "@/shared/ui/button";
import styles from "../ForgotPasswordForm.module.css";
import { useVerify } from "@/features/auth/model/hooks/useVerify";

const CODE_LENGTH = 6;

type Props = {
  userId: number;
};

export default function VerifyStep({ userId }: Props) {
  const navigate = useNavigate();

  const [code, setCode] = useState<string[]>(Array(CODE_LENGTH).fill(""));

  const inputRefs = useRef<(HTMLInputElement | null)[]>([]);

  const verify = useVerify();

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

  const handleSubmit = async () => {
    const fullCode = code.join("");

    if (fullCode.length !== CODE_LENGTH) {
      toast.error("Введіть повний код підтвердження.");
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
    <>
      <div className={styles.description}>
        <Text fz={15} c="var(--text-base)">
          Введіть код підтвердження.
        </Text>
      </div>

      <form
        className={styles.form}
        onSubmit={(event) => {
          event.preventDefault();
          handleSubmit();
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
    </>
  );
}
