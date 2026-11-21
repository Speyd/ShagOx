import { useNavigate } from "react-router-dom";
import { Button } from "@mantine/core";

import Container from "@/shared/ui/layout/container";
import {
  CreateAdvertisementForm,
  useCreateAdvertisement,
  type CreateAdvertisementPayload,
} from "@/features/advertisement/create-advertisement";

import styles from "./CreateAdvertisementPage.module.css";

const FORM_ID = "create-advertisement-form";

export default function CreateAdvertisementPage() {
  const navigate = useNavigate();
  const { mutate, isPending } = useCreateAdvertisement();

  const handleSubmit = (payload: CreateAdvertisementPayload) =>
    mutate(payload, {
      onSuccess: ({ id }) => navigate(`/advertisement/${id}`),
    });

  return (
    <div className={styles.page}>
      <Container>
        <div className={styles.topBar}>
          <div className={styles.topBarActions}>
            <Button
              variant="default"
              size="md"
              onClick={() => navigate(-1)}
              disabled={isPending}
            >
              Скасувати
            </Button>
            <Button
              type="submit"
              form={FORM_ID}
              size="md"
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
            Заповніть інформацію про товар, його варіанти та додайте фото
          </p>
        </div>

        <CreateAdvertisementForm formId={FORM_ID} onSubmit={handleSubmit} />
      </Container>
    </div>
  );
}
