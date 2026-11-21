import { Loader, SimpleGrid } from "@mantine/core";
import { Sliders } from "lucide-react";
import type { AttributeInfo } from "@/entities/advertisement/model/types";
import type { SelectOption } from "../../model/types";
import AttributeField from "../fields/AttributeField";
import FormCard from "../shared/FormCard";
import styles from "../CreateAdvertisementForm.module.css";

type AttributesSectionProps = {
  hasCategory: boolean;
  isLoading: boolean;
  isError: boolean;
  attributes: AttributeInfo[];
  optionsByKey: Record<string, SelectOption[]>;
};

export default function AttributesSection({
  hasCategory,
  isLoading,
  isError,
  attributes,
  optionsByKey,
}: AttributesSectionProps) {
  const renderContent = () => {
    if (!hasCategory)
      return (
        <p className={styles.notice}>
          Оберіть категорію, щоб заповнити характеристики товару.
        </p>
      );

    if (isLoading)
      return (
        <div className={styles.loadingBox}>
          <Loader size="sm" />
          <p className={styles.notice}>Завантаження характеристик...</p>
        </div>
      );

    if (isError)
      return (
        <p className={styles.errorMessage}>
          Не вдалося завантажити характеристики категорії.
        </p>
      );

    if (attributes.length === 0)
      return (
        <p className={styles.notice}>
          Для цієї категорії немає додаткових характеристик.
        </p>
      );

    return (
      <SimpleGrid cols={{ base: 1, sm: 2 }} spacing="md">
        {attributes.map((definition) => (
          <AttributeField
            key={definition.id}
            definition={definition}
            options={optionsByKey[definition.key]}
          />
        ))}
      </SimpleGrid>
    );
  };

  return (
    <FormCard
      title="Характеристики"
      subtitle="Допоможуть покупцям знайти товар через фільтри"
      icon={<Sliders size={18} />}
    >
      {renderContent()}
    </FormCard>
  );
}
