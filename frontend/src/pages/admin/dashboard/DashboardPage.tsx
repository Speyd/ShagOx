import type { ReactNode } from "react";
import { Link } from "react-router-dom";
import { AreaChart, BarChart } from "@mantine/charts";
import { Avatar, Badge, Skeleton, Text } from "@mantine/core";
import {
  BookOpen,
  FolderTree,
  MapPin,
  Megaphone,
  Package,
  ShoppingBag,
  Star,
  Users,
  type LucideIcon,
} from "lucide-react";
import { formatDate } from "@/shared/lib/formatDate";
import { getAdvertisementPrice } from "@/shared/lib/types/advertisements";
import { AdminPageHeader } from "@/widgets/admin/page-header";
import { useDashboardStats } from "./useDashboardStats";
import styles from "./DashboardPage.module.css";

const SERIES_USERS = "#2a78d6";
const SERIES_ADS = "#eb6834";
const SERIES_SINGLE = "#32639d";

type StatTileProps = {
  label: string;
  value?: ReactNode;
  hint?: ReactNode;
  icon: LucideIcon;
  to?: string;
  loading?: boolean;
};

function StatTile({ label, value, hint, icon: Icon, to, loading }: StatTileProps) {
  const content = (
    <>
      <div className={styles.statTop}>
        <Text size="sm" c="dimmed" fw={500}>
          {label}
        </Text>
        <span className={styles.statIcon}>
          <Icon size={18} />
        </span>
      </div>
      {loading ? (
        <Skeleton h={34} w={90} />
      ) : (
        <Text className={styles.statValue}>{value}</Text>
      )}
      {hint && !loading && (
        <Text size="xs" c="dimmed">
          {hint}
        </Text>
      )}
    </>
  );

  return to ? (
    <Link to={to} className={`${styles.stat} ${styles.statLink}`}>
      {content}
    </Link>
  ) : (
    <div className={styles.stat}>{content}</div>
  );
}

type PanelProps = {
  title: string;
  subtitle?: string;
  to?: string;
  className?: string;
  children: ReactNode;
};

function Panel({ title, subtitle, to, className, children }: PanelProps) {
  return (
    <section className={`${styles.panel} ${className ?? ""}`}>
      <header className={styles.panelHeader}>
        <div>
          <Text fw={700} c="var(--color-base)">
            {title}
          </Text>
          {subtitle && (
            <Text size="xs" c="dimmed">
              {subtitle}
            </Text>
          )}
        </div>
        {to && (
          <Link to={to} className={styles.more}>
            Усі →
          </Link>
        )}
      </header>
      {children}
    </section>
  );
}

function Empty({ text }: { text: string }) {
  return (
    <Text size="sm" c="dimmed" ta="center" py="lg">
      {text}
    </Text>
  );
}

const plural = (n: number) => n.toLocaleString("uk-UA");

export default function DashboardPage() {
  const { stats, catalogCounts, isLoading } = useDashboardStats();

  return (
    <div className={styles.page}>
      <AdminPageHeader
        title="Огляд"
        description={`Стан маркетплейсу на ${formatDate(new Date().toISOString())}`}
      />

      <div className={styles.stats}>
        <StatTile
          label="Користувачі"
          icon={Users}
          to="/admin/users"
          loading={isLoading}
          value={plural(stats.usersTotal)}
          hint={`+${stats.usersLast30} за 30 днів`}
        />
        <StatTile
          label="Оголошення"
          icon={Megaphone}
          to="/admin/advertisements"
          loading={isLoading}
          value={plural(stats.adsTotal)}
          hint={`+${stats.adsLast30} за 30 днів`}
        />
        <StatTile
          label="Товарів на складі"
          icon={Package}
          loading={isLoading}
          value={plural(stats.totalStock)}
          hint="Сума залишків усіх варіантів"
        />
        <StatTile
          label="Продано"
          icon={ShoppingBag}
          loading={isLoading}
          value={plural(stats.soldCount)}
          hint={
            stats.adsTotal
              ? `${Math.round((stats.soldCount / stats.adsTotal) * 100)}% оголошень`
              : "—"
          }
        />
        <StatTile
          label="Відгуки"
          icon={Star}
          to="/admin/reviews"
          loading={isLoading}
          value={plural(stats.reviewsTotal)}
          hint={
            stats.reviewsTotal
              ? `Середня оцінка ${stats.averageRating.toFixed(2)} з 5`
              : "Ще немає оцінок"
          }
        />
        <StatTile
          label="Категорії"
          icon={FolderTree}
          to="/admin/categories"
          loading={isLoading}
          value={plural(stats.categoriesTotal)}
          hint={
            catalogCounts
              ? `${catalogCounts.productTypes} типів · ${catalogCounts.dictionaries} довідників`
              : undefined
          }
        />
      </div>

      <div className={styles.grid}>
        <Panel
          title="Зростання"
          subtitle="Нові користувачі та оголошення за останні 12 місяців"
          className={styles.wide}
        >
          {isLoading ? (
            <Skeleton h={280} />
          ) : (
            <AreaChart
              h={280}
              data={stats.growth}
              dataKey="month"
              series={[
                { name: "users", label: "Користувачі", color: SERIES_USERS },
                { name: "ads", label: "Оголошення", color: SERIES_ADS },
              ]}
              curveType="monotone"
              strokeWidth={2}
              fillOpacity={0.12}
              withLegend
              legendProps={{ verticalAlign: "top", height: 36 }}
              gridAxis="y"
              tickLine="none"
              yAxisProps={{ allowDecimals: false }}
              withDots={false}
              activeDotProps={{ r: 5, strokeWidth: 2, stroke: "#fff" }}
            />
          )}
        </Panel>

        <Panel title="Оцінки відгуків" subtitle="Кількість відгуків за оцінкою" to="/admin/reviews">
          {isLoading ? (
            <Skeleton h={280} />
          ) : stats.reviewsTotal === 0 ? (
            <Empty text="Відгуків ще немає" />
          ) : (
            <BarChart
              h={280}
              data={stats.grades}
              dataKey="grade"
              orientation="vertical"
              series={[{ name: "count", label: "Відгуків", color: SERIES_SINGLE }]}
              yAxisProps={{ width: 48 }}
              gridAxis="x"
              tickLine="none"
              barProps={{ radius: [0, 4, 4, 0] }}
              maxBarWidth={22}
              withBarValueLabel
              xAxisProps={{ allowDecimals: false }}
            />
          )}
        </Panel>

        <Panel
          title="Оголошення за категоріями"
          subtitle="Найбільші категорії каталогу"
          to="/admin/categories"
          className={styles.wide}
        >
          {isLoading ? (
            <Skeleton h={300} />
          ) : stats.adsByCategory.length === 0 ? (
            <Empty text="Оголошень ще немає" />
          ) : (
            <BarChart
              h={Math.max(220, stats.adsByCategory.length * 38)}
              data={stats.adsByCategory}
              dataKey="category"
              orientation="vertical"
              series={[{ name: "count", label: "Оголошень", color: SERIES_SINGLE }]}
              yAxisProps={{ width: 150 }}
              gridAxis="x"
              tickLine="none"
              barProps={{ radius: [0, 4, 4, 0] }}
              maxBarWidth={22}
              withBarValueLabel
              xAxisProps={{ allowDecimals: false }}
            />
          )}
        </Panel>

        <Panel title="Каталог і локації">
          <ul className={styles.list}>
            <li className={styles.listItem}>
              <span className={styles.listIcon}>
                <BookOpen size={16} />
              </span>
              <Link to="/admin/product-types" className={styles.listLink}>
                Типи товарів
              </Link>
              <Text fw={700}>{catalogCounts?.productTypes ?? "…"}</Text>
            </li>
            <li className={styles.listItem}>
              <span className={styles.listIcon}>
                <FolderTree size={16} />
              </span>
              <Link to="/admin/categories" className={styles.listLink}>
                Категорії
              </Link>
              <Text fw={700}>{stats.categoriesTotal}</Text>
            </li>
            <li className={styles.listItem}>
              <span className={styles.listIcon}>
                <BookOpen size={16} />
              </span>
              <Link to="/admin/dictionaries" className={styles.listLink}>
                Довідники атрибутів
              </Link>
              <Text fw={700}>{catalogCounts?.dictionaries ?? "…"}</Text>
            </li>
            <li className={styles.listItem}>
              <span className={styles.listIcon}>
                <MapPin size={16} />
              </span>
              <Link to="/admin/cities" className={styles.listLink}>
                Міста
              </Link>
              <Text fw={700}>{catalogCounts?.cities ?? "…"}</Text>
            </li>
          </ul>
        </Panel>

        <Panel title="Нові користувачі" to="/admin/users">
          {stats.latestUsers.length === 0 && !isLoading && <Empty text="Поки порожньо" />}
          <ul className={styles.list}>
            {stats.latestUsers.map((user) => (
              <li key={user.id}>
                <Link to={`/admin/users/${user.id}`} className={styles.entry}>
                  <Avatar src={user.avatar?.url} radius="xl" size={36} color="blue">
                    {user.userName?.[0]?.toUpperCase()}
                  </Avatar>
                  <div className={styles.grow}>
                    <Text size="sm" fw={600} truncate c="var(--color-base)">
                      @{user.userName}
                    </Text>
                    <Text size="xs" c="dimmed" truncate>
                      {user.email ?? user.phone}
                    </Text>
                  </div>
                  <Text size="xs" c="dimmed">
                    {new Date(user.registeredAt).toLocaleDateString("uk-UA")}
                  </Text>
                </Link>
              </li>
            ))}
          </ul>
        </Panel>

        <Panel title="Нові оголошення" to="/admin/advertisements">
          {stats.latestAds.length === 0 && !isLoading && <Empty text="Поки порожньо" />}
          <ul className={styles.list}>
            {stats.latestAds.map((ad) => (
              <li key={ad.id}>
                <Link to={`/admin/advertisements/${ad.id}`} className={styles.entry}>
                  <span className={styles.thumb}>
                    {ad.pictures?.[0]?.url && <img src={ad.pictures[0].url} alt="" />}
                  </span>
                  <div className={styles.grow}>
                    <Text size="sm" fw={600} truncate c="var(--color-base)">
                      {ad.title}
                    </Text>
                    <Text size="xs" c="dimmed">
                      {getAdvertisementPrice(ad).toLocaleString("uk-UA")} {ad.currency?.symbol}
                    </Text>
                  </div>
                  {ad.soldAt && (
                    <Badge size="xs" color="orange" variant="light">
                      Продано
                    </Badge>
                  )}
                </Link>
              </li>
            ))}
          </ul>
        </Panel>

        <Panel title="Топ продавців" subtitle="За кількістю оголошень">
          {stats.topSellers.length === 0 && !isLoading && <Empty text="Поки порожньо" />}
          <ol className={styles.list}>
            {stats.topSellers.map(({ user, count }, index) => (
              <li key={user.id}>
                <Link to={`/admin/users/${user.id}`} className={styles.entry}>
                  <span className={styles.rank}>{index + 1}</span>
                  <div className={styles.grow}>
                    <Text size="sm" fw={600} truncate c="var(--color-base)">
                      @{user.userName}
                    </Text>
                  </div>
                  <Text size="sm" fw={700}>
                    {count}
                  </Text>
                </Link>
              </li>
            ))}
          </ol>
        </Panel>

        <Panel title="Закінчуються на складі" subtitle="Залишок 2 шт. або менше" className={styles.full}>
          {stats.outOfStock.length === 0 && !isLoading && (
            <Empty text="Усі товари в достатній кількості" />
          )}
          <ul className={styles.list}>
            {stats.outOfStock.map(({ ad, stock }) => (
              <li key={ad.id}>
                <Link to={`/admin/advertisements/${ad.id}`} className={styles.entry}>
                  <span className={styles.thumb}>
                    {ad.pictures?.[0]?.url && <img src={ad.pictures[0].url} alt="" />}
                  </span>
                  <div className={styles.grow}>
                    <Text size="sm" fw={600} truncate c="var(--color-base)">
                      {ad.title}
                    </Text>
                    <Text size="xs" c="dimmed">
                      @{ad.seller?.userName}
                    </Text>
                  </div>
                  <Badge color={stock === 0 ? "red" : "yellow"} variant="light">
                    {stock === 0 ? "Немає в наявності" : `${stock} шт.`}
                  </Badge>
                </Link>
              </li>
            ))}
          </ul>
        </Panel>
      </div>
    </div>
  );
}
