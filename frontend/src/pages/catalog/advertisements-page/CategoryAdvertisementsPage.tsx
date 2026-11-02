import { useEffect, useState, useMemo } from "react";
import { useParams, useSearchParams } from "react-router-dom";
import { useDebouncedValue, useDisclosure } from "@mantine/hooks";
import { Button, Drawer } from "@mantine/core";
import { SlidersHorizontal } from "lucide-react";

import { AdvertisementsGrid, CatalogFilters } from "@/widgets/catalog";
import { useGetCategoryByCode } from "@/entities/category/model/useGetCategoryByCode";
import { useGetAttributeDictionaryValues } from "@/entities/advertisement/model/hooks/useGetAttributeDictionaryValues";
import {
  type PriceRange,
} from "@/widgets/catalog/model/priceRangeSchema";

import styles from "./CategoryAdvertisementsPage.module.css";
import {
  buildAttributeFilters,
  type CategoryAttributeValueResponse,
} from "./utils/filterHelpers";

export default function CategoryAdvertisementsPage() {
  const { categoryCode } = useParams<{ categoryCode: string }>();
  const [searchParams, setSearchParams] = useSearchParams();

  const searchQuery = searchParams.get("search") || "";
  const [debouncedSearchQuery] = useDebouncedValue(searchQuery, 400);

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

  const optionsByDefinition = useMemo(() => {
    if (!categoryAttributeValues.length) return {};
    const result: Record<number, { value: string; label: string }[]> = {};

    for (const item of categoryAttributeValues as CategoryAttributeValueResponse[]) {
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
    () =>
      categoryAttributeValues.map(
        (item: CategoryAttributeValueResponse) => item.attribute,
      ),
    [categoryAttributeValues],
  );

  const hasFilters = useMemo(
    () => Object.keys(optionsByDefinition).length > 0,
    [optionsByDefinition],
  );

  const { attributes, variantAttributes, minPrice, maxPrice } = useMemo(
    () =>
      buildAttributeFilters(
        categoryAttributeValues as CategoryAttributeValueResponse[],
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
    priceRange.max !== "" ||
    searchQuery !== "";

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
    if (searchQuery) {
      searchParams.delete("search");
      setSearchParams(searchParams);
    }
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
        searchQuery={debouncedSearchQuery} 
        hasActiveFilters={hasActiveFilters}
        onResetFilters={handleClearFilters}
      />
    </section>
  );
}
