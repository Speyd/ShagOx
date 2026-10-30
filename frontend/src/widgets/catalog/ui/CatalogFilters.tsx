import styles from "./CatalogFilters.module.css";
import {
  Text,
  Checkbox,
  Stack,
  NumberInput,
  Group,
  Accordion,
} from "@mantine/core";
import { Sliders } from "lucide-react";
import Button from "@/shared/ui/button";
import { priceRangeSchema, type PriceRange } from "../model/priceRangeSchema";
import type { AttributeInfo } from "@/entities/advertisement/api/api";

type CatalogFiltersProps = {
  definitions: AttributeInfo[];
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

  const defaultOpenedValues = [
    "price",
    ...filterableDefinitions.map((def) => String(def.id)),
  ];

  return (
    <section aria-label="Фільтри" className={styles.catalogFilters}>
      <div className={styles.header}>
        <Sliders size={18} />
        <Text fw={700} fz={16}>
          Фільтри
        </Text>
      </div>

      <Accordion
        multiple
        defaultValue={defaultOpenedValues}
        variant="separated"
        radius="md"
        chevronPosition="right"
        className={styles.accordion}
      >
        <Accordion.Item value="price" className={styles.accordionItem}>
          <Accordion.Control className={styles.accordionControl}>
            <Text fw={600} fz={14}>
              Ціна (грн)
            </Text>
          </Accordion.Control>
          <Accordion.Panel>
            <Group grow align="flex-start" gap="xs">
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
                size="sm"
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
                size="sm"
              />
            </Group>
            {errorMessage && (
              <Text c="red" fz={12} mt={6}>
                {errorMessage}
              </Text>
            )}
          </Accordion.Panel>
        </Accordion.Item>

        {filterableDefinitions.map((definition) => {
          const options = optionsByDefinition[definition.id] ?? [];
          const currentValues = selectedValues[definition.id] ?? [];

          return (
            <Accordion.Item
              key={definition.id}
              value={String(definition.id)}
              className={styles.accordionItem}
            >
              <Accordion.Control className={styles.accordionControl}>
                <Text fw={600} fz={14}>
                  {definition.lable || definition.key}
                </Text>
              </Accordion.Control>
              <Accordion.Panel>
                <Checkbox.Group
                  value={currentValues}
                  onChange={(values) => onSelectValue(definition.id, values)}
                >
                  <Stack gap="sm">
                    {options.map((option) => (
                      <Checkbox
                        key={option.value}
                        value={option.value}
                        label={option.label}
                        size="sm"
                        className={styles.checkboxOption}
                      />
                    ))}
                  </Stack>
                </Checkbox.Group>
              </Accordion.Panel>
            </Accordion.Item>
          );
        })}
      </Accordion>

      <Button
        variant="outline"
        onClick={onReset}
        className={styles.resetButton}
      >
        Очистити фільтри
      </Button>
    </section>
  );
}
