import type { AttributeDefinition } from "@/entities/advertisement/api/api";
import styles from "./CatalogFilters.module.css";
import { Text, Checkbox, Stack, NumberInput, Group } from "@mantine/core";
import { Sliders } from "lucide-react";
import Button from "@/shared/ui/button";
import { priceRangeSchema, type PriceRange } from "../model/priceRangeSchema";

type CatalogFiltersProps = {
  definitions: AttributeDefinition[];
  optionsByDefinition: Record<number, { value: string; label: string }[]>;
  selectedValues: Record<number, string[]>;
  onSelectValue: (definitionId: number, values: string[]) => void;
  priceRange: PriceRange;
  onPriceChange: (priceRange: PriceRange) => void;
  onReset: () => void;
};

export default function CatalogFilters({
  definitions,
  optionsByDefinition,
  selectedValues,
  onSelectValue,
  priceRange,
  onPriceChange,
  onReset,
}: CatalogFiltersProps) {
  const filterableDefinitions = definitions.filter(
    (def) => optionsByDefinition[def.id]?.length > 0,
  );

  const validationResult = priceRangeSchema.safeParse(priceRange);
  const errorMessage = !validationResult.success
    ? validationResult.error.issues[0]?.message
    : null;

  if (filterableDefinitions.length === 0) {
    return null;
  }

  return (
    <section aria-label="Фільтри" className={styles.catalogFilters}>
      <div className={styles.header}>
        <Sliders />
        <Text fw={700} fz={15}>
          Фільтри
        </Text>
      </div>

      <div className="divider" />

      <div className={styles.filter}>
        <Text fw={600} fz={14} mb={8}>
          Ціна (грн)
        </Text>
        <Group grow align="flex-start">
          <NumberInput
            placeholder="Від"
            min={0}
            allowNegative={false}
            value={priceRange.min}
            onChange={(val) =>
              onPriceChange({
                ...priceRange,
                min: val === "" ? "" : Number(val),
              })
            }
            clampBehavior="strict"
          />
          <NumberInput
            placeholder="До"
            min={0}
            allowNegative={false}
            value={priceRange.max}
            onChange={(val) =>
              onPriceChange({
                ...priceRange,
                max: val === "" ? "" : Number(val),
              })
            }
            clampBehavior="strict"
            error={!!errorMessage}
          />
        </Group>
        {errorMessage && (
          <Text c="red" fz={12} mt={4}>
            {errorMessage}
          </Text>
        )}
      </div>

      <div className="divider" />

      {filterableDefinitions.map((definition) => {
        const options = optionsByDefinition[definition.id] ?? [];
        const currentValues = selectedValues[definition.id] ?? [];

        return (
          <div key={definition.id} className={styles.filterK}>
            <Checkbox.Group
              label={definition.lable || definition.key}
              value={currentValues}
              onChange={(values) => onSelectValue(definition.id, values)}
            >
              <Stack gap="xs" mt="xs">
                {options.map((option) => (
                  <Checkbox
                    key={option.value}
                    value={option.value}
                    label={option.label}
                  />
                ))}
              </Stack>
            </Checkbox.Group>
            <div className="divider" />
          </div>
        );
      })}
      <Button variant="outline" onClick={onReset}>
        Очистити фільтри
      </Button>
    </section>
  );
}
