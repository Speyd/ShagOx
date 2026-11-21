import { useEffect, useRef, useState } from "react";

/**
 * Скільки мілісекунд лишилося до `target` (оновлюється щосекунди).
 * `onExpire` викликається один раз, коли час вийшов.
 */
export function useCountdown(target: Date | null, onExpire?: () => void) {
  const targetTime = target?.getTime() ?? null;
  const [now, setNow] = useState(() => Date.now());

  const onExpireRef = useRef(onExpire);
  useEffect(() => {
    onExpireRef.current = onExpire;
  }, [onExpire]);

  useEffect(() => {
    if (targetTime === null) return;

    let expired = false;

    const tick = () => {
      if (expired) return;

      const current = Date.now();
      setNow(current);

      if (current >= targetTime) {
        expired = true;
        window.clearInterval(timer);
        onExpireRef.current?.();
      }
    };

    // Перший тік одразу: `now` міг застаріти, поки таймер був вимкнений.
    const firstTick = window.setTimeout(tick, 0);
    const timer = window.setInterval(tick, 1000);

    return () => {
      window.clearTimeout(firstTick);
      window.clearInterval(timer);
    };
  }, [targetTime]);

  return targetTime === null ? 0 : Math.max(0, targetTime - now);
}
