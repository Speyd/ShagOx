import { RangeSlider, Text } from "@mantine/core";
import type { RangeFilterGroup } from "@/features/catalog";
import { formatRangeValue } from "./format";
import styles from "../CatalogFilters.module.css";

type RangeFilterProps = {
  group: RangeFilterGroup;
  value: [number, number] | undefined;
  onChange: (value: [number, number] | null) => void;
};

export function RangeFilter({ group, value, onChange }: RangeFilterProps) {
  const { values, unit } = group;
  const last = values.length - 1;
  const fromIndex = value ? values.indexOf(value[0]) : -1;
  const toIndex = value ? values.indexOf(value[1]) : -1;
  const from = fromIndex === -1 ? 0 : fromIndex;
  const to = toIndex === -1 ? last : toIndex;

  return (
    <div className={styles.rangeBox}>
      <Text fz={13} c="dimmed">
        {formatRangeValue(values[from], unit)} — {formatRangeValue(values[to], unit)}
      </Text>
      <RangeSlider
        min={0}
        max={last}
        step={1}
        minRange={0}
        value={[from, to]}
        label={(index) => formatRangeValue(values[index], unit)}
        onChange={([a, b]) => onChange(a === 0 && b === last ? null : [values[a], values[b]])}
        color="var(--color-base)"
      />
    </div>
  );
}
