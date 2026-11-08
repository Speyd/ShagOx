import { Group, NumberInput, RangeSlider } from "@mantine/core";
import styles from "../CatalogFilters.module.css";

type PriceFilterProps = {
  bounds: [number, number];
  value: [number, number] | null;
  onChange: (value: [number, number] | null) => void;
};

const parseNumber = (text: string) => Number(text.replace(/\D/g, ""));

export function PriceFilter({ bounds, value, onChange }: PriceFilterProps) {
  const [min, max] = bounds;
  const [from, to] = value ?? bounds;
  const step = max - min > 10_000 ? 100 : 10;

  const commit = ([a, b]: [number, number]) => {
    const low = Math.max(min, Math.min(a, b));
    const high = Math.min(max, Math.max(a, b));
    onChange(low === min && high === max ? null : [low, high]);
  };

  const inputProps = {
    size: "sm" as const,
    thousandSeparator: " ",
    hideControls: true,
    onKeyDown: (e: React.KeyboardEvent<HTMLInputElement>) => e.key === "Enter" && e.currentTarget.blur(),
  };

  return (
    <div className={styles.rangeBox}>
      <RangeSlider
        min={min}
        max={max}
        step={step}
        minRange={0}
        value={[from, to]}
        onChange={commit}
        label={null}
        color="var(--color-base)"
      />
      <Group grow gap="xs">
        <NumberInput
          {...inputProps}
          key={`from-${from}`}
          prefix="від "
          defaultValue={from}
          onBlur={(e) => commit([parseNumber(e.currentTarget.value) || min, to])}
        />
        <NumberInput
          {...inputProps}
          key={`to-${to}`}
          prefix="до "
          defaultValue={to}
          onBlur={(e) => commit([from, parseNumber(e.currentTarget.value) || max])}
        />
      </Group>
    </div>
  );
}
