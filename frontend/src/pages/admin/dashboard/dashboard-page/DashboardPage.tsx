import type { ReactNode } from "react";
import { Link } from "react-router-dom";
import {
  Avatar,
  Badge,
  Group,
  Image,
  Paper,
  SimpleGrid,
  Skeleton,
  Stack,
  Text,
} from "@mantine/core";
import { FolderTree, Megaphone, Users, type LucideIcon } from "lucide-react";
import { useGetUsers } from "@/entities/user/model/hooks/useGetUsers";
import { useGetAdminAdvertisements } from "@/entities/advertisement/model/hooks/useGetAdminAdvertisements";
import { useGetAdminCategories } from "@/entities/category/model/useGetAdminCategories";
import { getAdvertisementPrice } from "@/shared/lib/types/advertisements";
import { AdminPageHeader } from "@/widgets/admin/page-header";
import styles from "./DashboardPage.module.css";

type StatCardProps = {
  label: string;
  value?: number;
  icon: LucideIcon;
  to: string;
};

function StatCard({ label, value, icon: Icon, to }: StatCardProps) {
  return (
    <Link to={to} className={styles.stat}>
      <div className={styles.statIcon}>
        <Icon size={24} />
      </div>
      <div>
        <Text c="dimmed" size="sm">
          {label}
        </Text>
        {value === undefined ? (
          <Skeleton h={30} w={60} mt={4} />
        ) : (
          <Text className={styles.statValue}>{value}</Text>
        )}
      </div>
    </Link>
  );
}

type PanelProps = {
  title: string;
  to: string;
  children: ReactNode;
};

function Panel({ title, to, children }: PanelProps) {
  return (
    <Paper withBorder radius="lg" p="lg" className={styles.panel}>
      <Group justify="space-between" mb="md">
        <Text fw={600} size="lg" c="var(--color-base)">
          {title}
        </Text>
        <Link to={to} className={styles.more}>
          Усі →
        </Link>
      </Group>
      <Stack gap="sm">{children}</Stack>
    </Paper>
  );
}

export default function DashboardPage() {
  const { data: users } = useGetUsers(1, 5);
  const { data: advertisements } = useGetAdminAdvertisements(1, 5);
  const { data: categories } = useGetAdminCategories(1, 1);

  return (
    <Stack gap="xl">
      <AdminPageHeader
        title="Огляд"
        description="Загальна інформація про маркетплейс"
      />

      <SimpleGrid cols={{ base: 1, sm: 3 }}>
        <StatCard
          label="Користувачі"
          value={users?.totalCount}
          icon={Users}
          to="/admin/users"
        />
        <StatCard
          label="Оголошення"
          value={advertisements?.totalCount}
          icon={Megaphone}
          to="/admin/advertisements"
        />
        <StatCard
          label="Категорії"
          value={categories?.totalCount}
          icon={FolderTree}
          to="/admin/categories"
        />
      </SimpleGrid>

      <SimpleGrid cols={{ base: 1, md: 2 }}>
        <Panel title="Користувачі" to="/admin/users">
          {users?.items.map((user) => (
            <Group key={user.id} wrap="nowrap">
              <Avatar src={user.avatar?.url} radius="xl">
                {user.userName[0]?.toUpperCase()}
              </Avatar>
              <div className={styles.grow}>
                <Text fw={500} truncate>
                  @{user.userName}
                </Text>
                <Text size="xs" c="dimmed" truncate>
                  {user.email ?? user.phone}
                </Text>
              </div>
              <Text size="xs" c="dimmed">
                {new Date(user.registeredAt).toLocaleDateString("uk-UA")}
              </Text>
            </Group>
          ))}
        </Panel>

        <Panel title="Оголошення" to="/admin/advertisements">
          {advertisements?.items.map((ad) => (
            <Group key={ad.id} wrap="nowrap">
              <div className={styles.imageContainer}>
                <Image
                  src={ad.pictures?.[0]?.url}
                  alt={ad.title || "Ad preview"}
                  fit="contain"
                  w="100%"
                  h="100%"
                />
              </div>
              <div className={styles.grow}>
                <Text fw={500} truncate>
                  {ad.title}
                </Text>
                <Text size="xs" c="dimmed">
                  {ad.currency?.symbol}
                  {getAdvertisementPrice(ad)}
                </Text>
              </div>
              <Badge color={ad.soldAt ? "red" : "green"} variant="light">
                {ad.soldAt ? "Продано" : "Активне"}
              </Badge>
            </Group>
          ))}
        </Panel>
      </SimpleGrid>
    </Stack>
  );
}
