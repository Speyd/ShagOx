import { useEffect, useRef } from "react";
import { useForm, useWatch, type Resolver } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import {
  createAdvertisementSchema,
  DEFAULT_VALUES,
  type AdvertisementFormValues,
} from "../schema";
import { useCategoryAttributes } from "./useCategoryAttributes";
import { useFormLookups } from "./useFormLookups";

/**
 * Стан форми створення оголошення.
 * Схема валідації перебудовується щоразу, коли змінюються атрибути обраної категорії.
 */
export function useAdvertisementForm() {
  const schemaRef = useRef(createAdvertisementSchema([]));

  const resolver: Resolver<AdvertisementFormValues> = (values, ctx, options) =>
    zodResolver(schemaRef.current)(values, ctx, options);

  const form = useForm<AdvertisementFormValues>({
    defaultValues: DEFAULT_VALUES,
    resolver,
  });

  const categoryId = useWatch({ control: form.control, name: "categoryId" });
  const category = useCategoryAttributes(
    categoryId ? Number(categoryId) : undefined,
  );
  const lookups = useFormLookups();

  useEffect(() => {
    schemaRef.current = createAdvertisementSchema(category.definitions);
  }, [category.definitions]);

  useEffect(() => {
    if (lookups.defaultCurrencyId && !form.getValues("currencyId")) {
      form.setValue("currencyId", lookups.defaultCurrencyId);
    }
  }, [lookups.defaultCurrencyId, form]);

  return { form, category, lookups, categoryId };
}
