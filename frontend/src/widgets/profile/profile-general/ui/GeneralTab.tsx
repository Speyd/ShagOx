import { useEffect } from "react";
import styles from "./GeneralTab.module.css";
import { useOutletContext } from "react-router-dom";
import type { ProfileContextType } from "@/app/layouts/ProfileLayout";
import { EmptyState } from "@/shared/ui/data-display/empty-state/EmptyState";

export default function GeneralTab() {
  const { setOrientation } = useOutletContext<ProfileContextType>();

  useEffect(() => {
    setOrientation("column");
  }, [setOrientation]);

  return (
    <section className={styles.profileGeneral}>
      <EmptyState
        variant="dashed"
        description="У вас поки що немає створених оголошень."
      />
    </section>
  );
}
