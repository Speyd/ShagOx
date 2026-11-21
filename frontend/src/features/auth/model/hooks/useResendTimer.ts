import { useCallback, useEffect, useState } from "react";

/** Countdown for the "send the code again" button. */
export function useResendTimer(seconds = 60, startImmediately = true) {
  const [left, setLeft] = useState(startImmediately ? seconds : 0);

  useEffect(() => {
    if (left <= 0) return;

    const timeout = setTimeout(() => setLeft((value) => value - 1), 1000);
    return () => clearTimeout(timeout);
  }, [left]);

  const restart = useCallback(() => setLeft(seconds), [seconds]);

  return { secondsLeft: left, canResend: left <= 0, restart };
}
