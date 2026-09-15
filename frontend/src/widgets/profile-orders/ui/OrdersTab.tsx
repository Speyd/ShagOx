import { Paper, Tabs, Text } from "@mantine/core";
import { useEffect, useState } from "react";
import { useOutletContext } from "react-router-dom";
import type { ProfileContextType } from "@/app/layouts/ProfileLayout";
import styles from "./OrdersTab.module.css";

export default function OrdersTab() {
  const { setOrientation } = useOutletContext<ProfileContextType>();

  const [activeTab, setActiveTab] = useState<string | null>("all");

  useEffect(() => {
    setOrientation("row");
  }, [setOrientation]);

  return (
    <Paper p="xl" radius="lg" className={styles.profileOrders}>
      <Text fw={700} fz={24}>
        Мої замовлення
      </Text>
      <Tabs value={activeTab} onChange={setActiveTab}>
        <Tabs.List className={styles.list}>
          <Tabs.Tab value="all" className={styles.tab}>
            Усі замовлення
          </Tabs.Tab>
          <Tabs.Tab value="processing" className={styles.tab}>
            В обробці
          </Tabs.Tab>
          <Tabs.Tab value="sent" className={styles.tab}>
            Відправлені
          </Tabs.Tab>
          <Tabs.Tab value="delivered" className={styles.tab}>
            Доставлені
          </Tabs.Tab>
          <Tabs.Tab value="cancelled" className={styles.tab}>
            Скасовані
          </Tabs.Tab>
        </Tabs.List>
      </Tabs>
    </Paper>
  );
}
