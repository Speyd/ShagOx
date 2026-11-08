import { FormProvider } from "react-hook-form";
import { toast } from "sonner";
import { toCreatePayload } from "../lib/toCreatePayload";
import { useAdvertisementForm } from "../model/hooks/useAdvertisementForm";
import type { CreateAdvertisementPayload } from "../model/types";
import AttributesSection from "./sections/AttributesSection";
import MainInfoSection from "./sections/MainInfoSection";
import PhotosSection from "./sections/PhotosSection";
import VariantsSection from "./sections/VariantsSection";
import styles from "./CreateAdvertisementForm.module.css";

type CreateAdvertisementFormProps = {
  formId: string;
  onSubmit: (payload: CreateAdvertisementPayload) => void;
};

export default function CreateAdvertisementForm({
  formId,
  onSubmit,
}: CreateAdvertisementFormProps) {
  const { form, category, lookups, categoryId } = useAdvertisementForm();

  const handleSubmit = form.handleSubmit(
    (values) => {
      if (!lookups.publishStatusId) {
        toast.error("Не вдалося завантажити довідники. Оновіть сторінку.");
        return;
      }
      onSubmit(
        toCreatePayload(values, category.definitions, lookups.publishStatusId),
      );
    },
    () => toast.error("Перевірте правильність заповнення полів"),
  );

  return (
    <FormProvider {...form}>
      <form
        id={formId}
        onSubmit={handleSubmit}
        className={styles.form}
        noValidate
      >
        <div className={styles.column}>
          <MainInfoSection conditionOptions={lookups.conditionOptions} />

          <AttributesSection
            hasCategory={Boolean(categoryId)}
            isLoading={category.isLoading}
            isError={category.isError}
            attributes={category.productAttributes}
            optionsByKey={category.optionsByKey}
          />

          <VariantsSection
            variantAttributes={category.variantAttributes}
            optionsByKey={category.optionsByKey}
            currencyOptions={lookups.currencyOptions}
            currencySymbols={lookups.currencySymbols}
          />
        </div>

        <div className={styles.column}>
          <PhotosSection />
        </div>
      </form>
    </FormProvider>
  );
}
