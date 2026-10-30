import { AdvertisementsGrid, CatalogFilters } from "@/widgets/catalog";
import styles from "./CategoryAdvertisementsPage.module.css";
import { useParams } from "react-router-dom";
import { useEffect, useState, useMemo } from "react";
import { useDebouncedValue, useDisclosure } from "@mantine/hooks";
import { useGetCategoryByCode } from "@/entities/category/model/useGetCategoryByCode";
import {
  priceRangeSchema,
  type PriceRange,
} from "@/widgets/catalog/model/priceRangeSchema";
import { useGetAttributeDictionaryValues } from "@/entities/advertisement/model/hooks/useGetAttributeDictionaryValues";
import { Button, Drawer } from "@mantine/core";
import { SlidersHorizontal } from "lucide-react";

export type AttributeInfo = {
  id: number;
  categoryId: number;
  categoryCode: string;
  key: string;
  dictionaryId: number | null;
  type: string;
  required: boolean;
  min: number | null;
  max: number | null;
  isVariant: boolean;
  multiple: boolean;
  lable: string;
};

export type DictionaryValueItem = {
  id: number;
  dictionaryId: number;
  code: string;
  value: string | null;
  label: string;
};

export type CategoryAttributeValueResponse = {
  attribute: AttributeInfo;
  values: DictionaryValueItem[] | null;
};

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
  items: CategoryAttributeValueResponse[],
  selectedValues: Record<number, string[]>,
  priceRange: PriceRange,
) {
  const attributes: Record<string, unknown[]> = {};
  const variantAttributes: Record<string, unknown> = {};

  for (const item of items) {
    const { attribute } = item;
    const values = selectedValues[attribute.id];
    if (!values || values.length === 0) continue;

    const parsedValues = values.map((val) =>
      parseAttributeValue(val, attribute.type),
    );
    const target = attribute.isVariant ? variantAttributes : attributes;
    target[attribute.key] = parsedValues;
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
  const [drawerOpened, { open: openDrawer, close: closeDrawer }] =
    useDisclosure(false);

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

  const { data: categoryAttributeValues = [], isLoading: isLoadingAttributes } =
    useGetAttributeDictionaryValues(categoryId);

  useEffect(() => {
    setSelectedValues({});
    setPriceRange({ min: "", max: "" });
  }, [categoryId]);

  const optionsByDefinition = useMemo((): Record<
    number,
    { value: string; label: string }[]
  > => {
    if (!categoryAttributeValues.length) return {};

    const result: Record<number, { value: string; label: label }> = {};

    for (const item of categoryAttributeValues) {
      const { attribute, values } = item;

      if (values && values.length > 0) {
        result[attribute.id] = values.map((val) => ({
          value: val.code || val.label,
          label: val.label,
        }));
      }
    }

    return result;
  }, [categoryAttributeValues]);

  const attributeDefinitions = useMemo(
    () => categoryAttributeValues.map((item) => item.attribute),
    [categoryAttributeValues],
  );

  const hasFilters = useMemo(
    () => Object.keys(optionsByDefinition).length > 0,
    [optionsByDefinition],
  );

  const { attributes, variantAttributes, minPrice, maxPrice } = useMemo(
    () =>
      buildAttributeFilters(
        categoryAttributeValues,
        selectedValues,
        debouncedPriceRange,
      ),
    [categoryAttributeValues, selectedValues, debouncedPriceRange],
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
        <>
          <div className={styles.mobileFilterButtonWrapper}>
            <Button
              leftSection={<SlidersHorizontal size={18} />}
              variant="outline"
              color="gray"
              onClick={openDrawer}
              className={styles.mobileFilterBtn}
            >
              Фільтри {hasActiveFilters && "•"}
            </Button>
          </div>

          <aside className={styles.desktopFilters}>
            <CatalogFilters
              definitions={attributeDefinitions}
              optionsByDefinition={optionsByDefinition}
              selectedValues={selectedValues}
              onSelectValue={handleSelectValue}
              priceRange={priceRange}
              onPriceChange={setPriceRange}
              onReset={handleClearFilters}
            />
          </aside>

          <Drawer
            opened={drawerOpened}
            onClose={closeDrawer}
            title="Фільтри"
            padding="md"
            size="85%"
            position="left"
            withCloseButton={false}
            zIndex={1002}
            className={styles.mobileDrawer}
          >
            <CatalogFilters
              definitions={attributeDefinitions}
              optionsByDefinition={optionsByDefinition}
              selectedValues={selectedValues}
              onSelectValue={handleSelectValue}
              priceRange={priceRange}
              onPriceChange={setPriceRange}
              onReset={() => {
                handleClearFilters();
                closeDrawer();
              }}
            />
          </Drawer>
        </>
      )}

      <AdvertisementsGrid
        categoryId={categoryId}
        isLoadingCategory={isLoadingCategory || isLoadingAttributes}
        attributeFilters={attributeFilters}
        minPrice={minPrice}
        maxPrice={maxPrice}
        hasActiveFilters={hasActiveFilters}
        onResetFilters={handleClearFilters}
      />
    </section>
  );
}
