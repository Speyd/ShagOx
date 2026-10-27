import { useEffect } from "react";
import styles from "./GeneralTab.module.css";
import { useOutletContext } from "react-router-dom";
import type { ProfileContextType } from "@/app/layouts/ProfileLayout";
import { Text } from "@mantine/core";

export default function GeneralTab() {
  const { setOrientation } = useOutletContext<ProfileContextType>();

  useEffect(() => {
    setOrientation("column");
  }, [setOrientation]);

  return (
    <section className={styles.profileGeneral}>
      <div className="emptyState">
        <Text c="dimmed">У вас поки що немає створених оголошень.</Text>
      </div>
    </section>
  );
}
