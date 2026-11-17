import { Navigate, useNavigate, useParams } from "react-router-dom";
import { Button, Loader } from "@mantine/core";
import Container from "@/shared/ui/layout/container";
import { EmptyState } from "@/shared/ui/data-display/empty-state/EmptyState";
import { useGetAdvertisement } from "@/entities/advertisement/model/hooks/useGetAdvertisement";
import { useAuthStore } from "@/features/auth";
import {
  UpdateAdvertisementForm,
  useUpdateAdvertisement,
  type UpdateAdvertisementRequestDto,
} from "@/features/advertisement/update-advertisement";
import styles from "@/pages/advertisement/create-advertisement-page/CreateAdvertisementPage.module.css";

const FORM_ID = "update-advertisement-form";

export default function UpdateAdvertisementPage() {
  const { id } = useParams();
  const navigate = useNavigate();
  const user = useAuthStore((state) => state.user);

  const { data: advertisement, isLoading } = useGetAdvertisement(Number(id));
  const { mutate, isPending } = useUpdateAdvertisement();

  const handleSubmit = (data: UpdateAdvertisementRequestDto) => {
    if (!advertisement) return;
    mutate(
      { id: advertisement.id, data },
      { onSuccess: () => navigate("/profile") },
    );
  };

  if (isLoading) {
    return (
      <div className={styles.page}>
        <Container>
          <div className={styles.headerCard}>
            <Loader size="sm" />
          </div>
        </Container>
      </div>
    );
  }

  if (!advertisement) {
    return (
      <div className={styles.page}>
        <Container>
          <EmptyState
            title="Оголошення не знайдено"
            description="Можливо, його було видалено."
            buttonLabel="До моїх товарів"
            onButtonClick={() => navigate("/profile")}
          />
        </Container>
      </div>
    );
  }

  if (user?.id !== advertisement.seller.id) {
    return <Navigate to={`/advertisement/${advertisement.id}`} replace />;
  }

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
              Зберегти зміни
            </Button>
          </div>
        </div>

        <div className={styles.headerCard}>
          <h2 className={styles.pageTitle}>Редагування товару</h2>
          <p className={styles.pageSubtitle}>
            Оновіть опис, ціни, залишки та фото товару
          </p>
        </div>

        <UpdateAdvertisementForm
          key={advertisement.id}
          formId={FORM_ID}
          advertisement={advertisement}
          onSubmit={handleSubmit}
        />
      </Container>
    </div>
  );
}
