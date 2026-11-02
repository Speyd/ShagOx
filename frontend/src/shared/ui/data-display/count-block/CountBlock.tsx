import { useState } from "react";
import styles from "./CountBlock.module.css";
import { Minus, Plus } from "lucide-react";

type CountBlockProps = {
  value?: number;
  min?: number;
  max?: number;
  onChange?: (value: number) => void;
};

export default function CountBlock({
  value: initialValue = 1,
  min = 1,
  max = 99,
  onChange,
}: CountBlockProps) {
  const [count, setCount] = useState<number>(initialValue);

  const handleDecrement = () => {
    if (count > min) {
      const nextValue = count - 1;
      setCount(nextValue);
      onChange?.(nextValue);
    }
  };

  const handleIncrement = () => {
    if (count < max) {
      const nextValue = count + 1;
      setCount(nextValue);
      onChange?.(nextValue);
    }
  };

  return (
    <div className={styles.counter}>
      <button
        type="button"
        className={styles.button}
        onClick={handleDecrement}
        disabled={count <= min}
        aria-label="Decrease count"
      >
        <Minus />
      </button>

      <span className={styles.value}>{count}</span>

      <button
        type="button"
        className={styles.button}
        onClick={handleIncrement}
        disabled={count >= max}
        aria-label="Increase count"
      >
        <Plus />
      </button>
    </div>
  );
}
