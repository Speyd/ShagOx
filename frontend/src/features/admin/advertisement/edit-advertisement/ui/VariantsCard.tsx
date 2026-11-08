import { useState } from "react";
import { ActionIcon, Badge, NumberInput, Select, Table, Text, Tooltip } from "@mantine/core";
import { Layers, Plus, Trash2 } from "lucide-react";
import { toast } from "sonner";
import type { AttributeInfo } from "@/entities/advertisement/model/types";
import type { SelectOption } from "@/features/advertisement/create-advertisement/model/types";
import { getAttributeLabel, hasPredefinedOptions } from "@/features/advertisement/create-advertisement/lib/attributes";
import Button from "@/shared/ui/buttons/button";
import { FormField, TextField } from "@/shared/ui/form";
import { variantKey } from "../lib/mapping";
import type { EditorVariant } from "../model/types";
import EditorCard from "./EditorCard";
import styles from "./AdvertisementEditor.module.css";

export type VariantErrors = Record<string, Partial<Record<"price" | "stock" | "previousPrice", string>>>;

type VariantsCardProps = {
  variants: EditorVariant[];
  onChange: (variants: EditorVariant[]) => void;
  onRemove: (variant: EditorVariant) => void;
  variantAttributes: AttributeInfo[];
  optionsByKey: Record<string, SelectOption[]>;
  currencySymbol?: string;
  errors: VariantErrors;
};

type Draft = {
  options: Record<string, string>;
  price: number | "";
  previousPrice: number | "";
  stock: number | "";
};

const emptyDraft = (): Draft => ({ options: {}, price: "", previousPrice: "", stock: 1 });

const toNumber = (value: string | number) => (value === "" ? "" : Number(value));

export default function VariantsCard({
  variants,
  onChange,
  onRemove,
  variantAttributes,
  optionsByKey,
  currencySymbol,
  errors,
}: VariantsCardProps) {
  const [draft, setDraft] = useState<Draft>(emptyDraft);
  const [bulkPrice, setBulkPrice] = useState<number | "">("");
  const [bulkStock, setBulkStock] = useState<number | "">("");

  const update = (uid: string, patch: Partial<EditorVariant>) =>
    onChange(variants.map((variant) => (variant.uid === uid ? { ...variant, ...patch } : variant)));

  const optionLabel = (key: string, value: string) =>
    optionsByKey[key]?.find((option) => option.value === value)?.label ?? value;

  const addVariant = () => {
    const missing = variantAttributes.find(
      (definition) => definition.required && !draft.options[definition.key],
    );
    if (missing) {
      toast.error(`Вкажіть «${getAttributeLabel(missing)}» для нового варіанта`);
      return;
    }
    if (draft.price === "" || draft.price <= 0) {
      toast.error("Вкажіть ціну нового варіанта");
      return;
    }
    if (draft.stock === "" || draft.stock < 0) {
      toast.error("Вкажіть кількість нового варіанта");
      return;
    }
    if (draft.previousPrice !== "" && draft.previousPrice <= draft.price) {
      toast.error("Стара ціна має бути більшою за поточну");
      return;
    }

    const key = variantKey(draft.options);
    if (variants.some((variant) => variantKey(variant.options) === key)) {
      toast.error("Варіант з такими параметрами вже існує");
      return;
    }

    onChange([
      ...variants,
      {
        uid: crypto.randomUUID(),
        options: { ...draft.options },
        labels: Object.fromEntries(
          Object.entries(draft.options).map(([k, v]) => [k, optionLabel(k, v)]),
        ),
        price: draft.price,
        previousPrice: draft.previousPrice,
        stock: draft.stock,
      },
    ]);
    setDraft(emptyDraft());
  };

  const applyBulk = () => {
    onChange(
      variants.map((variant) => ({
        ...variant,
        ...(bulkPrice !== "" && { price: bulkPrice }),
        ...(bulkStock !== "" && { stock: bulkStock }),
      })),
    );
    setBulkPrice("");
    setBulkStock("");
  };

  const totalStock = variants.reduce((sum, v) => sum + Number(v.stock || 0), 0);
  const canAddWithoutAttributes = variantAttributes.length === 0 && variants.length === 0;

  return (
    <EditorCard
      title="Варіанти, ціни та наявність"
      subtitle={`Варіантів: ${variants.length} · На складі: ${totalStock} шт.`}
      icon={<Layers size={18} />}
    >
      {variants.length > 1 && (
        <div className={styles.bulk}>
          <FormField label="Ціна для всіх">
            <NumberInput
              value={bulkPrice}
              onChange={(value) => setBulkPrice(toNumber(value))}
              min={0}
              hideControls
              radius="md"
              w={150}
              rightSection={currencySymbol}
            />
          </FormField>
          <FormField label="Кількість для всіх">
            <NumberInput
              value={bulkStock}
              onChange={(value) => setBulkStock(toNumber(value))}
              min={0}
              allowDecimal={false}
              radius="md"
              w={150}
            />
          </FormField>
          <Button
            size="sm"
            variant="light"
            disabled={bulkPrice === "" && bulkStock === ""}
            onClick={applyBulk}
          >
            Застосувати
          </Button>
        </div>
      )}

      <Table.ScrollContainer minWidth={720}>
        <Table verticalSpacing="sm" highlightOnHover>
          <Table.Thead>
            <Table.Tr>
              <Table.Th>Варіант</Table.Th>
              <Table.Th w={160}>Ціна *</Table.Th>
              <Table.Th w={150}>Стара ціна</Table.Th>
              <Table.Th w={130}>Кількість *</Table.Th>
              <Table.Th w={52} />
            </Table.Tr>
          </Table.Thead>
          <Table.Tbody>
            {variants.map((variant) => {
              const rowErrors = errors[variant.uid] ?? {};
              const changed =
                variant.id &&
                (variant.price !== variant.initialPrice || variant.stock !== variant.initialStock);
              const labels = Object.entries(variant.labels).filter(([, label]) => label);

              return (
                <Table.Tr key={variant.uid}>
                  <Table.Td>
                    <div className={styles.variantCell}>
                      <div className={styles.variantBadges}>
                        {labels.length > 0 ? (
                          labels.map(([key, label]) => (
                            <Badge key={key} variant="light" radius="sm">
                              {label}
                            </Badge>
                          ))
                        ) : (
                          <Text size="sm" c="dimmed">
                            Базовий варіант
                          </Text>
                        )}
                      </div>
                      {!variant.id && (
                        <Badge size="xs" color="teal" variant="filled">
                          Новий
                        </Badge>
                      )}
                      {changed && (
                        <Badge size="xs" color="orange" variant="light">
                          Змінено
                        </Badge>
                      )}
                    </div>
                  </Table.Td>
                  <Table.Td>
                    <NumberInput
                      value={variant.price}
                      onChange={(value) => update(variant.uid, { price: toNumber(value) })}
                      min={0}
                      decimalScale={2}
                      thousandSeparator=" "
                      hideControls
                      radius="md"
                      rightSection={currencySymbol}
                      error={rowErrors.price}
                    />
                  </Table.Td>
                  <Table.Td>
                    {variant.id ? (
                      <Tooltip label="Оновлюється автоматично при зниженні ціни">
                        <Text size="sm" c="dimmed" td={variant.previousPrice ? "line-through" : undefined}>
                          {variant.previousPrice
                            ? `${Number(variant.previousPrice).toLocaleString("uk-UA")} ${currencySymbol ?? ""}`
                            : "—"}
                        </Text>
                      </Tooltip>
                    ) : (
                      <NumberInput
                        value={variant.previousPrice}
                        onChange={(value) =>
                          update(variant.uid, { previousPrice: toNumber(value) })
                        }
                        min={0}
                        hideControls
                        radius="md"
                        placeholder="—"
                        error={rowErrors.previousPrice}
                      />
                    )}
                  </Table.Td>
                  <Table.Td>
                    <NumberInput
                      value={variant.stock}
                      onChange={(value) => update(variant.uid, { stock: toNumber(value) })}
                      min={0}
                      allowDecimal={false}
                      radius="md"
                      error={rowErrors.stock}
                    />
                  </Table.Td>
                  <Table.Td>
                    <Tooltip label={variants.length === 1 ? "Потрібен хоча б один варіант" : "Видалити варіант"}>
                      <ActionIcon
                        variant="subtle"
                        color="red"
                        disabled={variants.length === 1}
                        onClick={() => onRemove(variant)}
                        aria-label="Видалити варіант"
                      >
                        <Trash2 size={16} />
                      </ActionIcon>
                    </Tooltip>
                  </Table.Td>
                </Table.Tr>
              );
            })}
          </Table.Tbody>
        </Table>
      </Table.ScrollContainer>

      {(variantAttributes.length > 0 || canAddWithoutAttributes) && (
        <div className={styles.draft}>
          <Text fw={600} size="sm" c="var(--color-base)">
            Додати новий варіант
          </Text>
          <div className={styles.draftGrid}>
            {variantAttributes.map((definition) => (
              <FormField
                key={definition.key}
                label={getAttributeLabel(definition)}
                required={definition.required}
              >
                {hasPredefinedOptions(definition) ? (
                  <Select
                    data={optionsByKey[definition.key] ?? []}
                    value={draft.options[definition.key] ?? null}
                    onChange={(value) =>
                      setDraft((prev) => ({
                        ...prev,
                        options: { ...prev.options, [definition.key]: value ?? "" },
                      }))
                    }
                    placeholder="Оберіть"
                    searchable
                    clearable
                    radius="md"
                  />
                ) : (
                  <TextField
                    value={draft.options[definition.key] ?? ""}
                    onChange={(event) => {
                      const value = event.currentTarget.value;
                      setDraft((prev) => ({
                        ...prev,
                        options: { ...prev.options, [definition.key]: value },
                      }));
                    }}
                    placeholder="Значення"
                  />
                )}
              </FormField>
            ))}
            <FormField label="Ціна" required>
              <NumberInput
                value={draft.price}
                onChange={(value) => setDraft((prev) => ({ ...prev, price: toNumber(value) }))}
                min={0}
                hideControls
                radius="md"
                rightSection={currencySymbol}
              />
            </FormField>
            <FormField label="Стара ціна">
              <NumberInput
                value={draft.previousPrice}
                onChange={(value) =>
                  setDraft((prev) => ({ ...prev, previousPrice: toNumber(value) }))
                }
                min={0}
                hideControls
                radius="md"
                placeholder="—"
              />
            </FormField>
            <FormField label="Кількість" required>
              <NumberInput
                value={draft.stock}
                onChange={(value) => setDraft((prev) => ({ ...prev, stock: toNumber(value) }))}
                min={0}
                allowDecimal={false}
                radius="md"
              />
            </FormField>
          </div>
          <div>
            <Button size="sm" variant="outline" leftIcon={<Plus size={16} />} onClick={addVariant}>
              Додати варіант
            </Button>
          </div>
        </div>
      )}
    </EditorCard>
  );
}
