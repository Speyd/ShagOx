import { useRef, useState } from "react";
import type { FormEvent, ReactNode } from "react";
import { ArrowRight } from "lucide-react";
import { Text } from "@mantine/core";
import Button from "@/shared/ui/buttons/button";
import { useResendTimer } from "../../model/hooks/useResendTimer";
import VerificationCodeInput, {
  VERIFICATION_CODE_LENGTH,
} from "./VerificationCodeInput";
import styles from "./CodeConfirmation.module.css";

type Props = {
  /** Text above the code cells, e.g. "We sent a code to …". */
  description?: ReactNode;
  /** Return `true` on success; on `false`/throw the cells are cleared. */
  onSubmit: (code: string) => Promise<boolean>;
  /** Resend the code. Omit to hide the resend block. */
  onResend?: () => Promise<void>;
  isSubmitting?: boolean;
  isResending?: boolean;
  submitLabel?: string;
  /** Seconds before "resend" becomes available. */
  resendDelay?: number;
  /** Extra action next to "resend" (e.g. "Back"). */
  secondaryAction?: ReactNode;
  autoFocus?: boolean;
};

/**
 * Shared "enter the code from the e-mail/SMS" step: the code cells, submit button
 * and resend countdown. Used for registration, password recovery and password change.
 */
export default function CodeConfirmation({
  description,
  onSubmit,
  onResend,
  isSubmitting = false,
  isResending = false,
  submitLabel = "Підтвердити",
  resendDelay = 60,
  secondaryAction,
  autoFocus = true,
}: Props) {
  const [code, setCode] = useState("");
  const inFlight = useRef(false);
  const timer = useResendTimer(resendDelay);

  const submit = async (value: string) => {
    if (value.length !== VERIFICATION_CODE_LENGTH || inFlight.current) return;

    inFlight.current = true;
    const ok = await onSubmit(value).catch(() => false);
    inFlight.current = false;

    if (!ok) setCode("");
  };

  const handleSubmit = (event: FormEvent) => {
    event.preventDefault();
    void submit(code);
  };

  const handleResend = async () => {
    if (!onResend || !timer.canResend) return;

    try {
      await onResend();
      setCode("");
      timer.restart();
    } catch {
      // the caller shows the error
    }
  };

  return (
    <div className={styles.root}>
      {description && (
        <Text fz={14} c="var(--text-base)" className={styles.description}>
          {description}
        </Text>
      )}

      <form className={styles.form} onSubmit={handleSubmit}>
        <VerificationCodeInput
          value={code}
          onChange={setCode}
          onComplete={(value) => void submit(value)}
          disabled={isSubmitting}
          autoFocus={autoFocus}
        />

        <Button
          type="submit"
          className={styles.submit}
          disabled={code.length !== VERIFICATION_CODE_LENGTH}
          loading={isSubmitting}
          rightSection={ArrowRight}
          iconProps={{ size: 20 }}
        >
          {submitLabel}
        </Button>
      </form>

      {(onResend || secondaryAction) && (
        <div className={styles.footer}>
          {secondaryAction}

          {onResend &&
            (timer.canResend ? (
              <Button
                type="button"
                variant="outline"
                className={styles.resend}
                onClick={handleResend}
                loading={isResending}
                disabled={isSubmitting}
              >
                Надіслати код повторно
              </Button>
            ) : (
              <Text fz={14} c="dimmed">
                Надіслати код повторно через{" "}
                <strong>{timer.secondsLeft}с</strong>
              </Text>
            ))}
        </div>
      )}
    </div>
  );
}
