import { useEffect } from "react";
import { useOutletContext } from "react-router-dom";
import type { ProfileContextType } from "@/app/layouts/ProfileLayout";
import { MyAdvertisements } from "@/widgets/profile/profile-advertisements";
import styles from "./GeneralTab.module.css";

export default function GeneralTab() {
  const { setOrientation } = useOutletContext<ProfileContextType>();

  useEffect(() => {
    setOrientation("column");
  }, [setOrientation]);

  return (
    <section className={styles.profileGeneral}>
      <MyAdvertisements />
    </section>
  );
}
