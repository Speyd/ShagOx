import { useMemo } from "react";
import {
  Select,
  NumberInput,
  TextInput,
  Text,
  Loader,
  SimpleGrid,
} from "@mantine/core";
import { Sliders } from "lucide-react";
import type { Control } from "react-hook-form";
import { useWatch, Controller } from "react-hook-form";
import { useGetAttributeDefinitions } from "@/entities/advertisement/model/hooks/useGetAttributeDefinitions";
import { useGetAttributeDictionaryValues } from "@/entities/advertisement/model/hooks/useGetAttributeDictionaryValues";
import styles from "../CreateAdvertisementForm.module.css";
import type { CreateAdvertisementSchema } from "../../model/schemas/createAdvertisementSchema";

type CategoryAttributesSectionProps = {
  control: Control<CreateAdvertisementSchema>;
};

export default function CategoryAttributesSection({
  control,
}: CategoryAttributesSectionProps) {
  const categoryIdValue = useWatch({ control, name: "categoryId" });
  const categoryId = categoryIdValue ? Number(categoryIdValue) : undefined;

  const { data: attributeDefinitions = [], isLoading: isLoadingDefs } =
    useGetAttributeDefinitions(categoryId);

  const dictionaryIds = useMemo(() => {
    return attributeDefinitions
      .map((def) => def.dictionaryId)
      .filter((id): id is number => typeof id === "number" && id > 0);
  }, [attributeDefinitions]);

  const dictionaryQueries = useGetAttributeDictionaryValues(dictionaryIds);

  const optionsByDictionaryId = useMemo(() => {
    const map = new Map<number, { value: string; label: string }[]>();

    dictionaryIds.forEach((dictId, index) => {
      const query = dictionaryQueries[index];
      if (query?.data) {
        const options = query.data.map((item) => ({
          value: item.code || item.value || String(item.id),
          label: item.label || item.value || item.code || String(item.id),
        }));
        map.set(dictId, options);
      }
    });

    return map;
  }, [dictionaryIds, dictionaryQueries]);

  if (!categoryId) {
    return (
      <div className={styles.card}>
        <div className={styles.cardHeaderRow}>
          <div className={styles.cardHeaderWithIcon}>
            <Sliders size={18} />
            <h3 className={styles.cardTitle}>Характеристики товару</h3>
          </div>
        </div>
        <p className={styles.emptyNotice}>
          Оберіть категорію вище, щоб заповнити специфічні характеристики
          товару.
        </p>
      </div>
    );
  }

  if (isLoadingDefs) {
    return (
      <div className={styles.card}>
        <div className={styles.cardHeaderRow}>
          <div className={styles.cardHeaderWithIcon}>
            <Sliders size={18} />
            <h3 className={styles.cardTitle}>Характеристики товару</h3>
          </div>
        </div>
        <div className={styles.loadingBox}>
          <Loader size="sm" />
          <Text fz={14} c="dimmed" mt={8}>
            Завантаження характеристик категорії...
          </Text>
        </div>
      </div>
    );
  }

  if (attributeDefinitions.length === 0) {
    return (
      <div className={styles.card}>
        <div className={styles.cardHeaderRow}>
          <div className={styles.cardHeaderWithIcon}>
            <Sliders size={18} />
            <h3 className={styles.cardTitle}>Характеристики товару</h3>
          </div>
        </div>
        <p className={styles.emptyNotice}>
          Для обраної категорії немає додаткових характеристик.
        </p>
      </div>
    );
  }

  return (
    <div className={styles.card}>
      <div className={styles.cardHeaderRow}>
        <div className={styles.cardHeaderWithIcon}>
          <Sliders size={18} />
          <h3 className={styles.cardTitle}>Характеристики товару</h3>
        </div>
        <Text fz={13} c="dimmed">
          Заповніть параметри для полегшення пошуку покупцями
        </Text>
      </div>

      <SimpleGrid cols={{ base: 1, sm: 2 }} spacing="md" mt="md">
        {attributeDefinitions.map((def) => {
          const type = def.type.toLowerCase();
          const targetGroup = def.isVariant
            ? "variantAttributes"
            : "attributes";
          const fieldName = `${targetGroup}.${def.key}` as const;
          const label = def.lable || def.key;

          if (def.dictionaryId) {
            const options = optionsByDictionaryId.get(def.dictionaryId) ?? [];
            return (
              <div key={def.id} className={styles.field}>
                <label className={styles.fieldLabel}>
                  {label}{" "}
                  {def.required && (
                    <span className={styles.requiredStar}>*</span>
                  )}
                </label>
                <Controller
                  name={fieldName}
                  control={control}
                  render={({ field }) => (
                    <Select
                      placeholder={`Оберіть ${label.toLowerCase()}`}
                      data={options}
                      value={field.value ? String(field.value) : null}
                      onChange={(val) => field.onChange(val ?? undefined)}
                      searchable
                      clearable
                      size="md"
                    />
                  )}
                />
              </div>
            );
          }

          if (type === "boolean") {
            return (
              <div key={def.id} className={styles.field}>
                <label className={styles.fieldLabel}>
                  {label}{" "}
                  {def.required && (
                    <span className={styles.requiredStar}>*</span>
                  )}
                </label>
                <Controller
                  name={fieldName}
                  control={control}
                  render={({ field }) => (
                    <Select
                      placeholder="Оберіть значення"
                      data={[
                        { value: "true", label: "Так" },
                        { value: "false", label: "Ні" },
                      ]}
                      value={
                        field.value === true
                          ? "true"
                          : field.value === false
                            ? "false"
                            : null
                      }
                      onChange={(val) =>
                        field.onChange(
                          val === "true"
                            ? true
                            : val === "false"
                              ? false
                              : undefined,
                        )
                      }
                      clearable
                      size="md"
                    />
                  )}
                />
              </div>
            );
          }

          if (type === "integer" || type === "decimal") {
            return (
              <div key={def.id} className={styles.field}>
                <label className={styles.fieldLabel}>
                  {label}{" "}
                  {def.required && (
                    <span className={styles.requiredStar}>*</span>
                  )}
                </label>
                <Controller
                  name={fieldName}
                  control={control}
                  render={({ field }) => (
                    <NumberInput
                      placeholder="Введіть значення"
                      value={typeof field.value === "number" ? field.value : ""}
                      onChange={(val) =>
                        field.onChange(val === "" ? undefined : Number(val))
                      }
                      min={def.min ?? undefined}
                      max={def.max ?? undefined}
                      size="md"
                    />
                  )}
                />
              </div>
            );
          }

          return (
            <div key={def.id} className={styles.field}>
              <label className={styles.fieldLabel}>
                {label}{" "}
                {def.required && <span className={styles.requiredStar}>*</span>}
              </label>
              <Controller
                name={fieldName}
                control={control}
                render={({ field }) => (
                  <TextInput
                    placeholder="Введіть значення"
                    value={typeof field.value === "string" ? field.value : ""}
                    onChange={(e) =>
                      field.onChange(e.currentTarget.value || undefined)
                    }
                    size="md"
                  />
                )}
              />
            </div>
          );
        })}
      </SimpleGrid>
    </div>
  );
}
