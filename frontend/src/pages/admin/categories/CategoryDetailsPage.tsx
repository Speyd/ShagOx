import { useMemo } from "react";
import { useParams } from "react-router-dom";
import { useQuery } from "@tanstack/react-query";
import type { ColumnDef } from "@tanstack/react-table";
import { Badge, Loader, Text } from "@mantine/core";
import { BookOpen, Layers, Megaphone, Tag } from "lucide-react";
import { fetchById } from "@/shared/api/crud";
import { useAdminList, useAdminOptions } from "@/shared/api/crudHooks";
import type {
  AdminAttributeDefinition,
  AdminCategory,
  AdminDictionary,
  AdminProductType,
  AttributeKind,
} from "@/shared/lib/types/admin";
import { AdminPageHeader } from "@/widgets/admin/page-header";
import { InfoTile } from "@/widgets/admin/info-tile";
import { CrudPage, CODE_PATTERN, idColumn, nameColumn } from "@/widgets/admin/crud-page";
import type { FormValues } from "@/widgets/admin/crud-page";
import styles from "../AdminPages.module.css";

const TYPE_LABELS: Record<AttributeKind, string> = {
  String: "Текст",
  Integer: "Ціле число",
  Decimal: "Дробове число",
  Boolean: "Так / Ні",
  Select: "Вибір зі списку",
};

const TYPE_OPTIONS = Object.entries(TYPE_LABELS).map(([value, label]) => ({
  value,
  label,
}));

const toNumberOrNull = (value: unknown) =>
  value === null || value === undefined || value === "" ? null : Number(value);

export default function CategoryDetailsPage() {
  const categoryId = Number(useParams().id);

  const { data: category, isLoading } = useQuery({
    queryKey: ["admin", "category", categoryId],
    queryFn: () => fetchById<AdminCategory>("/admin/categories", categoryId),
    enabled: Number.isFinite(categoryId),
  });

  const { data: productTypes = [] } = useAdminOptions<AdminProductType>(
    "/admin/product-types",
  );
  const { data: dictionaries = [] } = useAdminOptions<AdminDictionary>(
    "/admin/attribute-dictionaries",
  );
  const { data: adsCount } = useAdminList("/admin/advertisements", {
    pageSize: 1,
    status: "All",
    filters: { CategoryId: categoryId },
  });
  const { data: attributesCount } = useAdminList("/admin/attribute-definitions", {
    pageSize: 1,
    status: "ActiveAndHidden",
    filters: { CategoryId: categoryId },
  });

  const dictionaryOptions = useMemo(
    () => dictionaries.map((d) => ({ value: String(d.id), label: d.code })),
    [dictionaries],
  );

  const columns = useMemo<ColumnDef<AdminAttributeDefinition>[]>(() => {
    const dictionaryCodes = new Map(dictionaries.map((d) => [d.id, d.code]));
    return [
      idColumn(),
      nameColumn("Атрибут", (item) => item.label, (item) => item.key),
      {
        accessorKey: "type",
        header: "Тип",
        cell: ({ row }) => (
          <div className={styles.cellStack}>
            <Badge variant="light" radius="sm" color="indigo">
              {TYPE_LABELS[row.original.type] ?? row.original.type}
            </Badge>
            {row.original.dictionaryId && (
              <Text size="xs" c="dimmed">
                Довідник: {dictionaryCodes.get(row.original.dictionaryId) ?? `#${row.original.dictionaryId}`}
              </Text>
            )}
          </div>
        ),
      },
      {
        id: "flags",
        header: "Параметри",
        cell: ({ row }) => {
          const item = row.original;
          return (
            <div className={styles.badges}>
              {item.required && (
                <Badge size="sm" variant="dot" color="red">
                  Обов'язковий
                </Badge>
              )}
              {item.isVariant && (
                <Badge size="sm" variant="dot" color="grape">
                  Варіант
                </Badge>
              )}
              {item.multiple && (
                <Badge size="sm" variant="dot" color="cyan">
                  Кілька значень
                </Badge>
              )}
              {!item.required && !item.isVariant && !item.multiple && (
                <Text size="xs" c="dimmed">
                  —
                </Text>
              )}
            </div>
          );
        },
      },
      {
        id: "range",
        header: "Діапазон",
        cell: ({ row }) => {
          const { min, max } = row.original;
          if (min == null && max == null) return <Text size="sm" c="dimmed">—</Text>;
          return (
            <Text size="sm">
              {min ?? "−∞"} … {max ?? "∞"}
            </Text>
          );
        },
      },
    ];
  }, [dictionaries]);

  if (isLoading) return <Loader mx="auto" display="block" mt="xl" />;

  if (!category) {
    return (
      <div className={styles.page}>
        <AdminPageHeader title="Категорію не знайдено" backTo="/admin/categories" />
      </div>
    );
  }

  const productType = productTypes.find((type) => type.id === category.productTypeId);

  const validate = (values: FormValues) => {
    const errors: Record<string, string> = {};
    if (values.type === "Select" && !values.dictionaryId)
      errors.dictionaryId = "Для типу «Вибір зі списку» потрібен довідник";
    const min = toNumberOrNull(values.min);
    const max = toNumberOrNull(values.max);
    if (min !== null && max !== null && min > max)
      errors.max = "Максимум має бути не меншим за мінімум";
    return errors;
  };

  return (
    <div className={styles.page}>
      <AdminPageHeader
        title={category.label || category.code}
        description="Налаштування характеристик, які продавці заповнюють в оголошеннях"
        backTo="/admin/categories"
        backLabel="До категорій"
      />

      <div className={styles.infoGrid}>
        <InfoTile icon={<Tag size={18} />} label="Код" value={category.code} mono />
        <InfoTile
          icon={<BookOpen size={18} />}
          label="Тип товару"
          value={productType?.label || productType?.name || `#${category.productTypeId}`}
        />
        <InfoTile
          icon={<Layers size={18} />}
          label="Атрибутів"
          value={attributesCount?.totalCount ?? "…"}
        />
        <InfoTile
          icon={<Megaphone size={18} />}
          label="Оголошень"
          value={adsCount?.totalCount ?? "…"}
        />
      </div>

      <section className={styles.card}>
        <CrudPage<AdminAttributeDefinition>
          embedded
          title="Атрибути категорії"
          description="Варіантні атрибути (колір, пам'ять) формують варіанти товару; решта — характеристики"
          entityName="атрибут"
          endpoint="/admin/attribute-definitions"
          commandEndpoint="/admin/attributes-definitions"
          filters={{ CategoryId: categoryId }}
          searchParam="Key"
          searchPlaceholder="Пошук за ключем..."
          columns={columns}
          minWidth={900}
          validate={validate}
          fields={[
            {
              name: "key",
              label: "Ключ",
              kind: "text",
              required: true,
              maxLength: 100,
              pattern: CODE_PATTERN,
              patternMessage: "Лише латиниця, цифри, «-», «_» або «.»",
              hint: "Напр. color, memory, screen_size",
              placeholder: "color",
            },
            {
              name: "type",
              label: "Тип значення",
              kind: "select",
              required: true,
              options: TYPE_OPTIONS,
              searchable: false,
            },
            {
              name: "dictionaryId",
              label: "Довідник значень",
              kind: "select",
              options: dictionaryOptions,
              hint: "Обов'язковий для типу «Вибір зі списку»",
              fullWidth: true,
            },
            { name: "min", label: "Мінімум", kind: "number", hint: "Для числових типів" },
            { name: "max", label: "Максимум", kind: "number", hint: "Для числових типів" },
            {
              name: "required",
              label: "Обов'язковий",
              kind: "switch",
              hint: "Продавець має заповнити поле",
            },
            {
              name: "isVariant",
              label: "Варіантний",
              kind: "switch",
              hint: "Значення формують варіанти товару",
            },
            {
              name: "multiple",
              label: "Кілька значень",
              kind: "switch",
              hint: "Можна обрати декілька значень",
            },
          ]}
          getDefaults={(item) => ({
            key: item?.key ?? "",
            type: item?.type ?? "String",
            dictionaryId: item?.dictionaryId ? String(item.dictionaryId) : null,
            min: item?.min ?? null,
            max: item?.max ?? null,
            required: item?.required ?? false,
            isVariant: item?.isVariant ?? false,
            multiple: item?.multiple ?? false,
          })}
          toPayload={(values, item) => {
            const min = toNumberOrNull(values.min);
            const max = toNumberOrNull(values.max);
            return {
              categoryId,
              key: values.key,
              type: values.type,
              dictionaryId: values.dictionaryId ? Number(values.dictionaryId) : null,
              required: Boolean(values.required),
              isVariant: Boolean(values.isVariant),
              multiple: Boolean(values.multiple),
              min: item && min !== null ? Math.round(min) : min,
              max: item && max !== null ? Math.round(max) : max,
            };
          }}
          translations={{
            path: "/admin/attribute-definitions/translations",
            codeParam: "AttributeDefinitionKey",
            codeOf: (values) => String(values.key),
          }}
        />
      </section>
    </div>
  );
}
