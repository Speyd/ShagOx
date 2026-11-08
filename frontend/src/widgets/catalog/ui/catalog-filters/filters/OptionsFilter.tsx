import { useState } from "react";
import { Checkbox, Stack, UnstyledButton } from "@mantine/core";
import type { OptionsFilterGroup } from "@/features/catalog";
import styles from "../CatalogFilters.module.css";

type OptionsFilterProps = {
  group: OptionsFilterGroup;
  value: string[];
  onChange: (values: string[]) => void;
};

const VISIBLE_OPTIONS = 6;

export function OptionsFilter({ group, value, onChange }: OptionsFilterProps) {
  const [expanded, setExpanded] = useState(false);
  const hidden = group.options.length - VISIBLE_OPTIONS;
  const options = expanded ? group.options : group.options.slice(0, VISIBLE_OPTIONS);

  return (
    <Checkbox.Group value={value} onChange={onChange}>
      <Stack gap={10}>
        {options.map((option) => (
          <Checkbox
            key={option.value}
            value={option.value}
            size="sm"
            color="var(--color-base)"
            className={styles.option}
            label={
              <span className={styles.optionLabel}>
                {option.label}
                <span className={styles.optionCount}>{option.count}</span>
              </span>
            }
          />
        ))}
        {hidden > 0 && (
          <UnstyledButton className={styles.moreButton} onClick={() => setExpanded(!expanded)}>
            {expanded ? "Згорнути" : `Показати ще ${hidden}`}
          </UnstyledButton>
        )}
      </Stack>
    </Checkbox.Group>
  );
}
