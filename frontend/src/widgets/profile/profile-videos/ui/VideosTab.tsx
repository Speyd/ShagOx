import { Paper, Select, Tabs, Text } from "@mantine/core";
import styles from "./VideosTab.module.css";
import { useEffect, useState } from "react";
import { useOutletContext } from "react-router-dom";
import type { ProfileContextType } from "@/app/layouts/ProfileLayout";
import { ArrowRight, ChevronDown } from "lucide-react";

export default function VideosTab() {
  const { setOrientation } = useOutletContext<ProfileContextType>();
  const [activeTab, setActiveTab] = useState<string | null>("all");
  const [sort, setSort] = useState<string | null>("newest");

  const SORT_OPTIONS = [
    { value: "newest", label: "Найновіші" },
    { value: "oldest", label: "Найстаріші" },
    { value: "popular", label: "Популярні" },
  ];

  useEffect(() => {
    setOrientation("row");
  }, [setOrientation]);

  return (
    <Paper p="xl" radius="lg" className={styles.videosTab}>
      <div className={styles.header}>
        <div className={styles.title}>
          <Text fz={24} fw={700}>
            Мої відео
          </Text>
          <Text fz={12} fw={600}>
            47 відео
          </Text>
        </div>

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
            Всі відео
          </Tabs.Tab>
          <Tabs.Tab value="published" className={styles.tab}>
            Опубліковані
          </Tabs.Tab>
          <Tabs.Tab value="draft" className={styles.tab}>
            Чернетки
          </Tabs.Tab>
          <Tabs.Tab value="scheduled" className={styles.tab}>
            Заплановані
          </Tabs.Tab>
        </Tabs.List>
      </Tabs>
    </Paper>
  );
}
