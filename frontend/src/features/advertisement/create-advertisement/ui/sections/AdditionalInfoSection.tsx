import { TextInput, Select, Text } from "@mantine/core";
import { Controller, type Control } from "react-hook-form";
import {
  CONDITIONS,
  type CreateAdvertisementFormData,
} from "../../model/types";
import styles from "../CreateAdvertisementForm.module.css";

type AdditionalInfoSectionProps = {
  control: Control<CreateAdvertisementFormData>;
};

const conditionOptions = CONDITIONS.map((c) => ({
  value: String(c.id),
  label: c.label,
}));

export default function AdditionalInfoSection({
  control,
}: AdditionalInfoSectionProps) {
  return (
    <div className={styles.card}>
      <h3 className={styles.cardTitle}>Додаткова інформація</h3>

      <div className={styles.fieldsContainer}>
        {/* Brand */}
        <div className={styles.field}>
          <label className={styles.fieldLabel}>Бренд</label>
          <Controller
            name="brand"
            control={control}
            render={({ field }) => (
              <TextInput
                {...field}
                value={field.value ?? ""}
                placeholder="Введіть бренд"
                size="md"
              />
            )}
          />
        </div>

        {/* Condition */}
        <div className={styles.field}>
          <label className={styles.fieldLabel}>
            Стан товару <span className={styles.requiredStar}>*</span>
          </label>
          <Controller
            name="conditionId"
            control={control}
            render={({ field }) => (
              <Select
                placeholder="Оберіть стан"
                data={conditionOptions}
                size="md"
                clearable
                value={field.value ?? null}
                onChange={(val) => field.onChange(val ?? "")}
              />
            )}
          />
        </div>

        {/* Tags */}
        <div className={styles.field}>
          <label className={styles.fieldLabel}>Теги</label>
          <Controller
            name="tags"
            control={control}
            render={({ field }) => (
              <TextInput
                {...field}
                value={field.value ?? ""}
                placeholder="Додайте теги через кому"
                size="md"
              />
            )}
          />
          <Text fz={12} c="dimmed" mt={4}>
            Наприклад: iphone, смартфон, apple
          </Text>
        </div>
      </div>
    </div>
  );
}
