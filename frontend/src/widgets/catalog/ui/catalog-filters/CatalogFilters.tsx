import { Accordion, Text } from "@mantine/core";
import { SlidersHorizontal } from "lucide-react";
import type { CatalogFilters as CatalogFiltersState } from "@/features/catalog";
import Button from "@/shared/ui/buttons/button";
import { ActiveFilters } from "./filters/ActiveFilters";
import { OptionsFilter } from "./filters/OptionsFilter";
import { PriceFilter } from "./filters/PriceFilter";
import { RangeFilter } from "./filters/RangeFilter";
import styles from "./CatalogFilters.module.css";

const OPENED_BY_DEFAULT = 5;

export function CatalogFilters({ filters }: { filters: CatalogFiltersState }) {
  const { groups, priceBounds, state, activeCount } = filters;

  if (!priceBounds && groups.length === 0) return null;

  const opened = ["price", ...groups.slice(0, OPENED_BY_DEFAULT).map((g) => g.key)];

  return (
    <section aria-label="Фільтри" className={styles.catalogFilters}>
      <div className={styles.header}>
        <SlidersHorizontal size={18} />
        <Text fw={700} fz={16}>
          Фільтри
        </Text>
        {activeCount > 0 && <span className={styles.badge}>{activeCount}</span>}
      </div>

      <ActiveFilters filters={filters} />

      <Accordion multiple defaultValue={opened} variant="separated" radius="md" className={styles.accordion}>
        {priceBounds && (
          <Accordion.Item value="price" className={styles.accordionItem}>
            <Accordion.Control className={styles.accordionControl}>
              <Text fw={600} fz={14}>
                Ціна, ₴
              </Text>
            </Accordion.Control>
            <Accordion.Panel>
              <PriceFilter bounds={priceBounds} value={state.price} onChange={filters.setPrice} />
            </Accordion.Panel>
          </Accordion.Item>
        )}

        {groups.map((group) => (
          <Accordion.Item key={group.key} value={group.key} className={styles.accordionItem}>
            <Accordion.Control className={styles.accordionControl}>
              <Text fw={600} fz={14}>
                {group.label}
              </Text>
            </Accordion.Control>
            <Accordion.Panel>
              {group.kind === "options" ? (
                <OptionsFilter
                  group={group}
                  value={state.options[group.key] ?? []}
                  onChange={(values) => filters.setOptions(group.key, values)}
                />
              ) : (
                <RangeFilter
                  group={group}
                  value={state.ranges[group.key]}
                  onChange={(range) => filters.setRange(group.key, range)}
                />
              )}
            </Accordion.Panel>
          </Accordion.Item>
        ))}
      </Accordion>

      {activeCount > 0 && (
        <Button variant="outline" onClick={filters.reset} className={styles.resetButton}>
          Скинути фільтри
        </Button>
      )}
    </section>
  );
}
