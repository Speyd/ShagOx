import { Text } from "@mantine/core";
import { useEffect } from "react";
import { useOutletContext } from "react-router-dom";
import type { ProfileContextType } from "@/app/layouts/ProfileLayout";
import styles from "./StatisticsTab.module.css";

export default function StatisticsTab() {
  const { setOrientation } = useOutletContext<ProfileContextType>();

  useEffect(() => {
    setOrientation("row");
  }, [setOrientation]);

  return (
    <section className={styles.profileStatistics}>
      <div>
        <Text fw={700} fz={24}>
          Моя статистика
        </Text>
      </div>
    </section>
  );
}
