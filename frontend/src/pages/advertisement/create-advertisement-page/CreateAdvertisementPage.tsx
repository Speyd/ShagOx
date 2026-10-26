import { useNavigate } from "react-router-dom";
import { toast } from "sonner";
import { Button } from "@mantine/core";
import Container from "@/shared/ui/container";
import {
  CreateAdvertisementForm,
  useCreateAdvertisement,
} from "@/features/advertisement/create-advertisement";
import type { CreateAdvertisementSchema } from "@/features/advertisement/create-advertisement/model/schemas/createAdvertisementSchema";
import type { ImageFileItem } from "@/features/advertisement/create-advertisement/model/types";
import styles from "./CreateAdvertisementPage.module.css";

export default function CreateAdvertisementPage() {
  const navigate = useNavigate();
  const { mutateAsync, isPending } = useCreateAdvertisement();

  const handleSubmit = async (
    data: CreateAdvertisementSchema,
    images: ImageFileItem[],
  ) => {
    const categoryId = Number(data.categoryId);
    const conditionId = Number(data.conditionId);

    if (isNaN(categoryId) || isNaN(conditionId)) {
      toast.error("Оберіть категорію та стан товару");
      return;
    }

    // Build attributes object, merge brand/tags into it if provided
    const attributes: Record<string, unknown> = {
      ...data.attributes,
    };
    if (data.brand.trim()) {
      attributes["brand"] = data.brand.trim();
    }
    if (data.tags.trim()) {
      attributes["tags"] = data.tags
        .split(",")
        .map((t) => t.trim())
        .filter(Boolean);
    }
    if (data.hashtags.length > 0) {
      attributes["hashtags"] = data.hashtags;
    }

    // Variant attributes (e.g. color, storage size)
    const variantAttributes: Record<string, unknown> = {
      ...data.variantAttributes,
    };

    const payload = {
      title: data.title.trim(),
      description: data.description.trim(),
      popularity: 0,
      currencyId: 1, // UAH by default
      conditionId,
      categoryId,
      statusId: 0, // ACTIVE
      pictures: images.map((img) => img.file),
      attributes,
      variants: [
        {
          advertisementId: 0, // placeholder, assigned by backend
          price: Number(data.price),
          previousPrice: Number(data.price),
          stock: Number(data.stock),
          attributes: variantAttributes,
        },
      ],
    };

    try {
      const result = await mutateAsync(payload);
      navigate(`/advertisement/${result.id}`);
    } catch {
      // Error toast is handled in the mutation hook
    }
  };

  const handleCancel = () => {
    navigate(-1);
  };

  return (
    <div className={styles.page}>
      <Container>
        <div className={styles.topBar}>
          <div />
          <div className={styles.topBarActions}>
            <Button
              variant="default"
              size="md"
              onClick={handleCancel}
              disabled={isPending}
            >
              Скасувати
            </Button>
            <Button
              variant="filled"
              size="md"
              type="submit"
              form="create-advertisement-form"
              loading={isPending}
              bg="var(--color-base)"
            >
              Опублікувати товар
            </Button>
          </div>
        </div>

        <div className={styles.headerCard}>
          <h2 className={styles.pageTitle}>Додати товар</h2>
          <p className={styles.pageSubtitle}>
            Заповніть інформацію про товар та додайте фото
          </p>
        </div>
        
        <div className={styles.layout}>
          <div className={styles.mainColumn}>
            <CreateAdvertisementForm
              formId="create-advertisement-form"
              onSubmit={handleSubmit}
              isLoading={isPending}
            />
          </div>
        </div>
      </Container>
    </div>
  );
}
