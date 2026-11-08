import { useMemo } from "react";
import { Tooltip } from "@mantine/core";
import clsx from "clsx";
import type { AdvertisementVariant } from "@/shared/lib/types/advertisements";
import {
  findVariantForOption,
  getVariantOptionGroups,
  getVariantSelection,
  isOptionCombinable,
  type VariantOptionGroup,
} from "../../lib/variants";
import styles from "./VariantPicker.module.css";

type VariantPickerProps = {
  variants: AdvertisementVariant[];
  selected?: AdvertisementVariant;
  onSelect: (variant: AdvertisementVariant) => void;
  size?: "sm" | "lg";
};

const MAX_COMPACT_COLORS = 5;

export function VariantPicker({ variants, selected, onSelect, size = "lg" }: VariantPickerProps) {
  const groups = useMemo(() => getVariantOptionGroups(variants), [variants]);
  const selection = useMemo(() => getVariantSelection(selected), [selected]);

  const choose = (key: string, code: string) => {
    const variant = findVariantForOption(variants, selection, key, code);
    if (variant && variant.id !== selected?.id) onSelect(variant);
  };

  if (size === "sm") {
    const colors = groups.find((g) => g.isColor);
    if (!colors) return null;

    const visible = colors.options.slice(0, MAX_COMPACT_COLORS);
    const hidden = colors.options.length - visible.length;

    return (
      <div className={styles.compact}>
        {visible.map((option) => (
          <Tooltip key={option.code} label={option.label} withArrow openDelay={200}>
            <button
              type="button"
              aria-label={option.label}
              aria-pressed={selection[colors.key] === option.code}
              className={clsx(styles.dot, selection[colors.key] === option.code && styles.dotActive)}
              style={{ backgroundColor: option.hex }}
              onClick={() => choose(colors.key, option.code)}
            />
          </Tooltip>
        ))}
        {hidden > 0 && <span className={styles.more}>+{hidden}</span>}
      </div>
    );
  }

  const renderGroup = (group: VariantOptionGroup) => {
    const current = group.options.find((o) => o.code === selection[group.key]);

    return (
      <div key={group.key} className={styles.group}>
        <div className={styles.groupTitle}>
          {group.label}
          {current && (
            <>
              : <span className={styles.groupValue}>{current.label}</span>
            </>
          )}
        </div>

        <div className={styles.options}>
          {group.options.map((option) => {
            const isActive = selection[group.key] === option.code;
            const isCombinable = isOptionCombinable(variants, selection, group.key, option.code);

            return group.isColor ? (
              <Tooltip key={option.code} label={option.label} withArrow openDelay={150}>
                <button
                  type="button"
                  aria-label={option.label}
                  aria-pressed={isActive}
                  className={clsx(styles.swatch, isActive && styles.swatchActive, !isCombinable && styles.unavailable)}
                  onClick={() => choose(group.key, option.code)}
                >
                  <span className={styles.swatchColor} style={{ backgroundColor: option.hex }} />
                </button>
              </Tooltip>
            ) : (
              <button
                key={option.code}
                type="button"
                aria-pressed={isActive}
                className={clsx(styles.chip, isActive && styles.chipActive, !isCombinable && styles.unavailable)}
                onClick={() => choose(group.key, option.code)}
              >
                {option.label}
              </button>
            );
          })}
        </div>
      </div>
    );
  };

  return <div className={styles.picker}>{groups.map(renderGroup)}</div>;
}
