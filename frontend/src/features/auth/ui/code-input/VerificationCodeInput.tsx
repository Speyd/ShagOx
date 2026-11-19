import { useEffect, useRef } from "react";
import type { ClipboardEvent, KeyboardEvent } from "react";
import styles from "./VerificationCodeInput.module.css";

export const VERIFICATION_CODE_LENGTH = 6;

type Props = {
  /** Digits entered so far (string of 0..length digits). */
  value: string;
  onChange: (code: string) => void;
  /** Called once all cells are filled. */
  onComplete?: (code: string) => void;
  length?: number;
  disabled?: boolean;
  autoFocus?: boolean;
};

/**
 * Six separate cells for a one-time code: auto-advance, Backspace to the previous
 * cell, arrow navigation and pasting the whole code at once.
 */
export default function VerificationCodeInput({
  value,
  onChange,
  onComplete,
  length = VERIFICATION_CODE_LENGTH,
  disabled = false,
  autoFocus = true,
}: Props) {
  const refs = useRef<(HTMLInputElement | null)[]>([]);
  const digits = Array.from({ length }, (_, i) => value[i] ?? "");

  useEffect(() => {
    if (autoFocus) refs.current[0]?.focus();
  }, [autoFocus]);

  // When the parent clears the code (wrong code / resend) — go back to the first cell.
  // (inputs may still be disabled while the request settles, so focus once enabled).
  const previousValue = useRef(value);
  const pendingFocus = useRef(false);
  useEffect(() => {
    if (previousValue.current !== "" && value === "") pendingFocus.current = true;
    previousValue.current = value;

    if (pendingFocus.current && !disabled) {
      pendingFocus.current = false;
      refs.current[0]?.focus();
    }
  }, [value, disabled]);

  const focus = (index: number) =>
    refs.current[Math.max(0, Math.min(index, length - 1))]?.focus();

  const commit = (next: string[]) => {
    const code = next.join("");
    onChange(code);
    if (code.length === length && next.every(Boolean)) onComplete?.(code);
  };

  const handleChange = (index: number, raw: string) => {
    const digit = raw.replace(/\D/g, "").slice(-1);
    if (raw && !digit) return;

    const next = [...digits];
    next[index] = digit;
    commit(next);

    if (digit) focus(index + 1);
  };

  const handleKeyDown = (index: number, event: KeyboardEvent<HTMLInputElement>) => {
    if (event.key === "Backspace" && !digits[index] && index > 0) {
      event.preventDefault();
      const next = [...digits];
      next[index - 1] = "";
      commit(next);
      focus(index - 1);
    } else if (event.key === "ArrowLeft") {
      event.preventDefault();
      focus(index - 1);
    } else if (event.key === "ArrowRight") {
      event.preventDefault();
      focus(index + 1);
    }
  };

  const handlePaste = (event: ClipboardEvent<HTMLDivElement>) => {
    event.preventDefault();

    const pasted = event.clipboardData
      .getData("text")
      .replace(/\D/g, "")
      .slice(0, length);

    if (!pasted) return;

    const next = Array.from({ length }, (_, i) => pasted[i] ?? "");
    commit(next);
    focus(pasted.length);
  };

  return (
    <div className={styles.container} onPaste={handlePaste}>
      {digits.map((digit, index) => (
        <input
          key={index}
          ref={(element) => {
            refs.current[index] = element;
          }}
          type="text"
          inputMode="numeric"
          autoComplete={index === 0 ? "one-time-code" : "off"}
          maxLength={1}
          value={digit}
          disabled={disabled}
          aria-label={`Цифра ${index + 1} з ${length}`}
          onChange={(event) => handleChange(index, event.target.value)}
          onKeyDown={(event) => handleKeyDown(index, event)}
          onFocus={(event) => event.target.select()}
          className={`${styles.cell} ${digit ? styles.filled : ""}`}
        />
      ))}
    </div>
  );
}
