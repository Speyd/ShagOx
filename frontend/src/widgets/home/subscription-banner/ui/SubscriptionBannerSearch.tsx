import Input from "@/shared/ui/data-display/inputs/input";
import styles from "./SubscriptionBannerSearch.module.css";
import Button from "@/shared/ui/buttons/button";
import { ArrowRight } from "lucide-react";

export function SubscriptionBannerSearch() {
  return (
    <div className={styles.inputWrapper}>
      <Input placeholder="Ваш e-mail" className={styles.input} />
      <Button className={styles.button}>
        <ArrowRight />
      </Button>
    </div>
  );
}
