import { AdvertisementsGrid, CatalogFilters } from "@/widgets/catalog";
import styles from "./CategoryAdvertisementsPage.module.css";
import { useParams } from "react-router-dom";
import { useEffect, useState, useMemo } from "react";
import { useGetAttributeDefinitions } from "@/entities/advertisement/model/hooks/useGetAttributeDefinitions";
import { useGetCategoryAttributeFilterOptions } from "@/entities/advertisement/model/hooks/useGetCategoryAttributeFilterOptions";
import type { AttributeDefinition } from "@/entities/advertisement/api/api";
import { useDebouncedValue } from "@mantine/hooks";
import { useGetCategoryByCode } from "@/entities/category/model/useGetCategoryByCode";
import {
  priceRangeSchema,
  type PriceRange,
} from "@/widgets/catalog/model/priceRangeSchema";

function parseAttributeValue(value: string, type: string): unknown {
  const normalizedType = type.toLowerCase();

  if (normalizedType === "integer" || normalizedType === "decimal") {
    const numberValue = Number(value);
    return Number.isFinite(numberValue) ? numberValue : value;
  }

  if (normalizedType === "boolean") {
    if (value.toLowerCase() === "true") return true;
    if (value.toLowerCase() === "false") return false;
  }

  return value;
}

function buildAttributeFilters(
  definitions: AttributeDefinition[],
  selectedValues: Record<number, string[]>,
  priceRange: PriceRange,
) {
  const attributes: Record<string, unknown[]> = {};
  const variantAttributes: Record<string, unknown> = {};

  for (const definition of definitions) {
    const values = selectedValues[definition.id];
    if (!values || values.length === 0) continue;

    const parsedValues = values.map((val) =>
      parseAttributeValue(val, definition.type),
    );
    const target = definition.isVariant ? variantAttributes : attributes;
    target[definition.key] = parsedValues;
  }

  let minPrice: number | undefined = undefined;
  let maxPrice: number | undefined = undefined;

  const priceValidation = priceRangeSchema.safeParse(priceRange);
  if (priceValidation.success) {
    if (typeof priceRange.min === "number") {
      minPrice = priceRange.min;
    }
    if (typeof priceRange.max === "number") {
      maxPrice = priceRange.max;
    }
  }

  return { attributes, variantAttributes, minPrice, maxPrice };
}

export default function CategoryAdvertisementsPage() {
  const { categoryCode } = useParams<{ categoryCode: string }>();

  const [selectedValues, setSelectedValues] = useState<
    Record<number, string[]>
  >({});

  const [priceRange, setPriceRange] = useState<PriceRange>({
    min: "",
    max: "",
  });

  const [debouncedPriceRange] = useDebouncedValue(priceRange, 500);

  const { data: category, isLoading: isLoadingCategory } =
    useGetCategoryByCode(categoryCode);
  const categoryId = category?.id;

  const { data: attributeDefinitions = [] } =
    useGetAttributeDefinitions(categoryId);
  const { data: categoryAttributeOptions = [] } =
    useGetCategoryAttributeFilterOptions(categoryId);

  useEffect(() => {
    setSelectedValues({});
    setPriceRange({ min: "", max: "" });
  }, [categoryId]);

  const optionsByDefinition = useMemo((): Record<
    number,
    { value: string; label: string }[]
  > => {
    if (!categoryAttributeOptions.length || !attributeDefinitions.length) {
      return {};
    }

    const optionsByKey = new Map<string, Set<string>>();
    for (const opt of categoryAttributeOptions) {
      const key = opt.key.toLowerCase();
      let set = optionsByKey.get(key);
      if (!set) {
        set = new Set<string>();
        optionsByKey.set(key, set);
      }
      set.add(opt.value);
    }

    const result: Record<number, { value: string; label: string }[]> = {};

    for (const definition of attributeDefinitions) {
      const key = definition.key.toLowerCase();
      const valueSet = optionsByKey.get(key);
      if (!valueSet || valueSet.size === 0) continue;

      const type = definition.type.toLowerCase();
      const isNumeric = type === "integer" || type === "decimal";

      const options = Array.from(valueSet).map((val) => ({
        value: val,
        label: val,
      }));

      options.sort((left, right) =>
        isNumeric
          ? Number(left.value) - Number(right.value)
          : left.label.localeCompare(right.label),
      );

      result[definition.id] = options;
    }

    return result;
  }, [attributeDefinitions, categoryAttributeOptions]);

  const hasFilters = useMemo(
    () => Object.keys(optionsByDefinition).length > 0,
    [optionsByDefinition],
  );

  const { attributes, variantAttributes, minPrice, maxPrice } = useMemo(
    () =>
      buildAttributeFilters(
        attributeDefinitions,
        selectedValues,
        debouncedPriceRange,
      ),
    [attributeDefinitions, selectedValues, debouncedPriceRange],
  );

  const attributeFilters = useMemo(
    () => ({ attributes, variantAttributes }),
    [attributes, variantAttributes],
  );

  const hasActiveFilters =
    Object.keys(selectedValues).length > 0 ||
    priceRange.min !== "" ||
    priceRange.max !== "";

  function handleSelectValue(definitionId: number, values: string[]) {
    setSelectedValues((current) => {
      if (!values.length) {
        const next = { ...current };
        delete next[definitionId];
        return next;
      }
      return { ...current, [definitionId]: values };
    });
  }

  function handleClearFilters() {
    setSelectedValues({});
    setPriceRange({ min: "", max: "" });
  }

  return (
    <section
      className={
        hasFilters
          ? styles.categoryAdvertisementsPage
          : styles.categoryAdvertisementsPageSingle
      }
    >
      {hasFilters && (
        <CatalogFilters
          definitions={attributeDefinitions}
          optionsByDefinition={optionsByDefinition}
          selectedValues={selectedValues}
          onSelectValue={handleSelectValue}
          priceRange={priceRange}
          onPriceChange={setPriceRange}
          onReset={handleClearFilters}
        />
      )}
      <AdvertisementsGrid
        categoryId={categoryId}
        isLoadingCategory={isLoadingCategory}
        attributeFilters={attributeFilters}
        minPrice={minPrice}
        maxPrice={maxPrice}
        hasActiveFilters={hasActiveFilters}
        onResetFilters={handleClearFilters}
      />
    </section>
  );
}
