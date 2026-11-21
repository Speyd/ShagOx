import { Link, useNavigate, useParams } from "react-router-dom";
import { useQuery } from "@tanstack/react-query";
import { Avatar, Badge, Loader, Text } from "@mantine/core";
import { CalendarDays, Clock, Megaphone, MessageSquare } from "lucide-react";
import { api } from "@/shared/api/api";
import { useAdminDelete, useAdminList, useAdminVisibility } from "@/shared/api/crudHooks";
import { formatDate } from "@/shared/lib/formatDate";
import type { Advertisement } from "@/shared/lib/types/advertisements";
import { getAdvertisementPrice } from "@/shared/lib/types/advertisements";
import type { PaginatedResponse } from "@/shared/lib/types/paginatedResponse";
import type { User } from "@/shared/lib/types/user";
import { useGetUser } from "@/entities/user/model/hooks/useGetUser";
import { AdminUpdateUserForm } from "@/features/admin/user/update-user";
import Button from "@/shared/ui/buttons/button";
import { AdminPageHeader } from "@/widgets/admin/page-header";
import { InfoTile } from "@/widgets/admin/info-tile";
import { confirmAction } from "@/widgets/admin/row-actions";
import styles from "../AdminPages.module.css";
import localStyles from "./UserEditPage.module.css";

export default function UserEditPage() {
  const userId = Number(useParams().id);
  const navigate = useNavigate();
  const { data: user, isLoading } = useGetUser(userId);

  const { data: ads } = useQuery({
    queryKey: ["admin", "user-ads", userId],
    queryFn: async () => {
      const { data } = await api.get<PaginatedResponse<Advertisement>>(
        `/users/${userId}/advertisements`,
        { params: { page: 1, pageSize: 6 } },
      );
      return data;
    },
    enabled: Number.isFinite(userId),
  });

  const { data: reviews } = useAdminList("/admin/review-comment", {
    pageSize: 1,
    status: "All",
    filters: { OwnerId: userId },
  });

  const deleteUser = useAdminDelete("/admin/users", "користувача");
  const visibility = useAdminVisibility("/admin/users");

  if (isLoading) return <Loader mx="auto" display="block" mt="xl" />;

  if (!user) {
    return (
      <div className={styles.page}>
        <AdminPageHeader title="Користувача не знайдено" backTo="/admin/users" />
      </div>
    );
  }

  const adminUser = user as User & { entityStatus?: string };
  const hidden = adminUser.entityStatus === "Hidden";
  const fullName = [user.firstName, user.lastName].filter(Boolean).join(" ");

  return (
    <div className={styles.page}>
      <AdminPageHeader
        title={fullName || `@${user.userName}`}
        description={`Користувач #${user.id} · @${user.userName}`}
        backTo="/admin/users"
        backLabel="До користувачів"
      />

      <div className={styles.infoGrid}>
        <InfoTile
          icon={<Megaphone size={18} />}
          label="Оголошень"
          value={ads?.totalCount ?? "…"}
        />
        <InfoTile
          icon={<MessageSquare size={18} />}
          label="Відгуків залишено"
          value={reviews?.totalCount ?? "…"}
        />
        <InfoTile
          icon={<CalendarDays size={18} />}
          label="Зареєстрований"
          value={formatDate(user.registeredAt)}
        />
        <InfoTile
          icon={<Clock size={18} />}
          label="Остання активність"
          value={user.lastSeenAt ? formatDate(user.lastSeenAt) : "—"}
        />
      </div>

      <div className={localStyles.layout}>
        <section className={styles.card}>
          <Text fw={700} size="lg" c="var(--color-base)">
            Профіль
          </Text>
          <AdminUpdateUserForm key={user.id} user={user} />
        </section>

        <aside className={localStyles.aside}>
          <section className={styles.card}>
            <div className={styles.row}>
              <Avatar src={user.avatar?.url} size={56} radius="xl" color="blue">
                {user.userName?.[0]?.toUpperCase()}
              </Avatar>
              <div className={styles.grow}>
                <Text fw={700} c="var(--color-base)" truncate>
                  {fullName || user.userName}
                </Text>
                <Text size="sm" c="dimmed" truncate>
                  {user.email ?? user.phone}
                </Text>
              </div>
            </div>
            <div className={styles.badges}>
              {user.roles.map((role) => (
                <Badge
                  key={role.id}
                  variant="light"
                  color={role.name === "Admin" ? "red" : "blue"}
                  radius="sm"
                >
                  {role.name}
                </Badge>
              ))}
              <Badge variant="light" color={hidden ? "gray" : "teal"} radius="sm">
                {hidden ? "Прихований" : "Активний"}
              </Badge>
            </div>
          </section>

          <section className={styles.card}>
            <div className={localStyles.cardHeader}>
              <Text fw={700} c="var(--color-base)">
                Оголошення користувача
              </Text>
              <Text size="sm" c="dimmed">
                {ads?.totalCount ?? 0}
              </Text>
            </div>
            {ads?.items.length ? (
              <ul className={localStyles.adList}>
                {ads.items.map((ad) => (
                  <li key={ad.id}>
                    <Link to={`/admin/advertisements/${ad.id}`} className={localStyles.adItem}>
                      <span className={styles.thumb}>
                        {ad.pictures?.[0]?.url && <img src={ad.pictures[0].url} alt="" />}
                      </span>
                      <span className={styles.grow}>
                        <Text size="sm" fw={600} truncate c="var(--color-base)">
                          {ad.title}
                        </Text>
                        <Text size="xs" c="dimmed">
                          {getAdvertisementPrice(ad).toLocaleString("uk-UA")} {ad.currency?.symbol}
                        </Text>
                      </span>
                    </Link>
                  </li>
                ))}
              </ul>
            ) : (
              <Text size="sm" c="dimmed">
                Користувач ще не створив жодного оголошення.
              </Text>
            )}
            <Button
              size="sm"
              variant="light"
              onClick={() => navigate(`/admin/advertisements/new?sellerId=${user.id}`)}
            >
              Створити оголошення від імені користувача
            </Button>
          </section>

          <section className={`${styles.card} ${localStyles.danger}`}>
            <Text fw={700} c="red.8">
              Небезпечна зона
            </Text>
            <Text size="sm" c="dimmed">
              Прихований користувач не відображається на сайті. Видалення
              незворотне.
            </Text>
            <div className={localStyles.dangerActions}>
              <Button
                size="sm"
                variant="outline"
                loading={visibility.isPending}
                onClick={() => visibility.mutate({ id: user.id, hidden })}
              >
                {hidden ? "Показати" : "Приховати"}
              </Button>
              <Button
                size="sm"
                variant="danger"
                loading={deleteUser.isPending}
                onClick={() =>
                  confirmAction({
                    title: "Видалити користувача",
                    message: `Користувача @${user.userName} та пов'язані дані буде видалено назавжди.`,
                    onConfirm: () =>
                      deleteUser.mutate(user.id, {
                        onSuccess: () => navigate("/admin/users"),
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
