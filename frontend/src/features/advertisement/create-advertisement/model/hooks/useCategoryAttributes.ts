import { useMemo } from "react";
import { useGetAttributeDictionaryValues } from "@/entities/advertisement/model/hooks/useGetAttributeDictionaryValues";
import { BOOLEAN_OPTIONS } from "../../lib/attributes";
import type { SelectOption } from "../types";

/** Атрибути категорії, поділені на характеристики товару та варіантні атрибути. */
export function useCategoryAttributes(categoryId?: number) {
  const { data, isLoading, isError } =
    useGetAttributeDictionaryValues(categoryId);

  const attributes = useMemo(() => {
    const items = data ?? [];
    const definitions = items.map((item) => item.attribute);
    const optionsByKey: Record<string, SelectOption[]> = {};

    for (const { attribute, values } of items) {
      if (attribute.type === "Boolean") {
        optionsByKey[attribute.key] = BOOLEAN_OPTIONS;
      } else if (values) {
        optionsByKey[attribute.key] = values.map((value) => ({
          value: String(value.id),
          label: value.label || value.value || value.code,
        }));
      }
    }

    return {
      definitions,
      productAttributes: definitions.filter((d) => !d.isVariant),
      variantAttributes: definitions.filter((d) => d.isVariant),
      optionsByKey,
    };
  }, [data]);

  return { ...attributes, isLoading, isError };
}
