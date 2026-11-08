import { Link, useNavigate, useParams } from "react-router-dom";
import { useQuery } from "@tanstack/react-query";
import { Avatar, Badge, Loader, Text } from "@mantine/core";
import { ExternalLink } from "lucide-react";
import { useAdminDelete, useAdminList, useAdminVisibility } from "@/shared/api/crudHooks";
import { formatDate } from "@/shared/lib/formatDate";
import Button from "@/shared/ui/buttons/button";
import {
  AdvertisementEditor,
  getAdminAdvertisement,
} from "@/features/admin/advertisement/edit-advertisement";
import { AdminPageHeader } from "@/widgets/admin/page-header";
import { confirmAction } from "@/widgets/admin/row-actions";
import styles from "../AdminPages.module.css";
import localStyles from "./AdvertisementEditPage.module.css";

export default function AdvertisementEditPage() {
  const id = Number(useParams().id);
  const navigate = useNavigate();

  const { data: ad, isLoading, dataUpdatedAt } = useQuery({
    queryKey: ["admin", "advertisement", id],
    queryFn: () => getAdminAdvertisement(id),
    enabled: Number.isFinite(id),
  });

  const { data: reviews } = useAdminList("/admin/review-comment", {
    pageSize: 1,
    status: "All",
    filters: { CommentableEntityId: id },
  });

  const deleteAd = useAdminDelete("/admin/advertisements", "оголошення");
  const visibility = useAdminVisibility("/admin/advertisements");

  if (isLoading) return <Loader mx="auto" display="block" mt="xl" />;

  if (!ad) {
    return (
      <div className={styles.page}>
        <AdminPageHeader title="Оголошення не знайдено" backTo="/admin/advertisements" />
      </div>
    );
  }

  const hidden = ad.entityStatus === "Hidden";
  const stock = ad.variants.reduce((sum, variant) => sum + variant.stock, 0);

  return (
    <div className={styles.page}>
      <AdminPageHeader
        title="Редагування оголошення"
        description={`#${ad.id} · ${ad.title}`}
        backTo="/admin/advertisements"
        backLabel="До оголошень"
        actions={
          <Link to={`/advertisement/${ad.id}`} target="_blank" className={localStyles.external}>
            <ExternalLink size={16} />
            Переглянути на сайті
          </Link>
        }
      />

      <div className={localStyles.layout}>
        <AdvertisementEditor
          key={dataUpdatedAt}
          advertisement={ad}
          onCancel={() => navigate("/admin/advertisements")}
        />

        <aside className={localStyles.aside}>
          <section className={styles.card}>
            <Text fw={700} c="var(--color-base)">
              Продавець
            </Text>
            {ad.seller ? (
              <Link to={`/admin/users/${ad.seller.id}`} className={localStyles.person}>
                <Avatar radius="xl" color="blue">
                  {ad.seller.userName?.[0]?.toUpperCase()}
                </Avatar>
                <div className={styles.grow}>
                  <Text size="sm" fw={600} truncate c="var(--color-base)">
                    {[ad.seller.firstName, ad.seller.lastName].filter(Boolean).join(" ") ||
                      `@${ad.seller.userName}`}
                  </Text>
                  <Text size="xs" c="dimmed" truncate>
                    {ad.seller.email ?? ad.seller.phone}
                  </Text>
                </div>
              </Link>
            ) : (
              <Text size="sm" c="dimmed">
                —
              </Text>
            )}
          </section>

          <section className={styles.card}>
            <Text fw={700} c="var(--color-base)">
              Зведення
            </Text>
            <dl className={localStyles.facts}>
              <dt>Створено</dt>
              <dd>{formatDate(ad.createdAt)}</dd>
              <dt>Статус</dt>
              <dd>
                <Badge variant="light" radius="sm">
                  {ad.status?.lable || ad.status?.code}
                </Badge>
              </dd>
              <dt>Видимість</dt>
              <dd>
                <Badge variant="light" color={hidden ? "gray" : "teal"} radius="sm">
                  {hidden ? "Приховане" : "Активне"}
                </Badge>
              </dd>
              <dt>Варіантів</dt>
              <dd>{ad.variants.length}</dd>
              <dt>На складі</dt>
              <dd>{stock} шт.</dd>
              <dt>Відгуків</dt>
              <dd>{reviews?.totalCount ?? "…"}</dd>
              {ad.soldAt && (
                <>
                  <dt>Продано</dt>
                  <dd>{formatDate(ad.soldAt)}</dd>
                </>
              )}
              {ad.buyer && (
                <>
                  <dt>Покупець</dt>
                  <dd>
                    <Link to={`/admin/users/${ad.buyer.id}`} className={styles.link}>
                      @{ad.buyer.userName}
                    </Link>
                  </dd>
                </>
              )}
            </dl>
          </section>

          <section className={`${styles.card} ${localStyles.danger}`}>
            <Text fw={700} c="red.8">
              Модерація
            </Text>
            <Text size="sm" c="dimmed">
              Приховане оголошення не показується в каталозі. Видалення незворотне.
            </Text>
            <div className={localStyles.dangerActions}>
              <Button
                size="sm"
                variant="outline"
                loading={visibility.isPending}
                onClick={() => visibility.mutate({ id: ad.id, hidden })}
              >
                {hidden ? "Показати" : "Приховати"}
              </Button>
              <Button
                size="sm"
                variant="danger"
                loading={deleteAd.isPending}
                onClick={() =>
                  confirmAction({
                    title: "Видалити оголошення",
                    message: `«${ad.title}» буде видалено разом з варіантами та фото.`,
                    onConfirm: () =>
                      deleteAd.mutate(ad.id, {
                        onSuccess: () => navigate("/admin/advertisements"),
                      }),
                  })
                }
              >
                Видалити
              </Button>
            </div>
          </section>
        </aside>
      </div>
    </div>
  );
}
