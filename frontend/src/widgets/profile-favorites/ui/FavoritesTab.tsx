import { Paper, Select, Tabs, Text } from "@mantine/core";
import { useEffect, useState } from "react";
import { useOutletContext } from "react-router-dom";
import type { ProfileContextType } from "@/app/layouts/ProfileLayout";
import styles from "./FavoritesTab.module.css";
import { ChevronDown, ArrowRight } from "lucide-react";
import { useGetFavorites } from "@/features/favorites/model/hooks/useGetFavorites";
import AdvertisementCard from "@/entities/advertisement/ui/AdvertisementCard";

export default function FavoritesTab() {
  const { setOrientation } = useOutletContext<ProfileContextType>();
  const [activeTab, setActiveTab] = useState<string | null>("all");
  const [sort, setSort] = useState<string | null>("newest");
  const { data: favorites, isLoading } = useGetFavorites();

  const SORT_OPTIONS = [
    { value: "newest", label: "Найновіші" },
    { value: "oldest", label: "Найстаріші" },
    { value: "popular", label: "Популярні" },
  ];

  useEffect(() => {
    setOrientation("row");
  }, [setOrientation]);

  const advertisements = [...(favorites?.items ?? [])].sort((left, right) => {
    if (sort === "oldest") {
      return (
        new Date(left.advertisement.createdAt).getTime() -
        new Date(right.advertisement.createdAt).getTime()
      );
    }

    if (sort === "popular") {
      return right.advertisement.id - left.advertisement.id;
    }

    return (
      new Date(right.advertisement.createdAt).getTime() -
      new Date(left.advertisement.createdAt).getTime()
    );
  });

  const showProducts = activeTab === "all" || activeTab === "advertisements";
  const showVideos = activeTab === "all" || activeTab === "videos";

  return (
    <Paper p="xl" radius="lg" className={styles.paper}>
      <div className={styles.favoritesTab}>
        <div className={styles.header}>
          <Text fw={700} fz={24}>
            Обране
          </Text>

          <Select
            value={sort}
            onChange={setSort}
            data={SORT_OPTIONS}
            rightSection={<ChevronDown size={16} />}
            rightSectionPointerEvents="none"
            allowDeselect={false}
            withCheckIcon={false}
            radius="md"
            classNames={{ input: styles.input }}
          />
        </div>
        <Tabs value={activeTab} onChange={setActiveTab}>
          <Tabs.List className={styles.list}>
            <Tabs.Tab value="all" className={styles.tab}>
              Усі
            </Tabs.Tab>
            <Tabs.Tab value="advertisements" className={styles.tab}>
              Товари
            </Tabs.Tab>
            <Tabs.Tab value="videos" className={styles.tab}>
              Відео
            </Tabs.Tab>
          </Tabs.List>
        </Tabs>
      </div>

      <div className={styles.favoritesTab}>
        {showProducts && (
          <FavoriteSection
            title="Товари"
            linkLabel="Дивитися всі"
            isLoading={isLoading}
            emptyMessage="У вас поки що немає обраних товарів."
          >
            {advertisements.length > 0 ? (
              <div className={styles.productsGrid}>
                {advertisements.map(({ advertisement }) => (
                  <AdvertisementCard
                    key={advertisement.id}
                    {...advertisement}
                  />
                ))}
              </div>
            ) : null}
          </FavoriteSection>
        )}

        {showVideos && (
          <FavoriteSection
            title="Збережені відео"
            linkLabel="Всі відео"
            emptyMessage="У вас поки що немає збережених відео."
          />
        )}
      </div>
    </Paper>
  );
}

const SORT_OPTIONS = [
  { value: "newest", label: "Найновіші" },
  { value: "oldest", label: "Найстаріші" },
  { value: "popular", label: "Популярні" },
];

type FavoriteSectionProps = {
  title: string;
  linkLabel: string;
  isLoading?: boolean;
  emptyMessage: string;
  children?: React.ReactNode;
};

function FavoriteSection({
  title,
  linkLabel,
  isLoading = false,
  emptyMessage,
  children,
}: FavoriteSectionProps) {
  return (
    <section className={styles.section}>
      <div className={styles.sectionHeader}>
        <Text fw={700} fz={20}>
          {title}
        </Text>
        <button type="button" className={styles.viewAll}>
          {linkLabel}
          <ArrowRight size={16} />
        </button>
      </div>
      {isLoading ? (
        <Text c="dimmed">Завантаження...</Text>
      ) : children ? (
        children
      ) : (
        <div className={styles.emptyState}>
          <Text c="dimmed">{emptyMessage}</Text>
        </div>
      )}
    </section>
  );
}
