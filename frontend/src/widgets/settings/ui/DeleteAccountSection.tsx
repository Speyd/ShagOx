import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { Trash2 } from "lucide-react";
import { toast } from "sonner";
import Button from "@/shared/ui/buttons/button";
import ConfirmModal from "@/shared/ui/feedback/confirm-modal";
import SettingsSection from "./SettingsSection";
import styles from "./Settings.module.css";

export default function DeleteAccountSection() {
  const navigate = useNavigate();
  const [opened, setOpened] = useState(false);

  const handleConfirm = () => {
    setOpened(false);
    toast.info(
      "Видалення акаунта поки виконується через службу підтримки. Залиште запит — ми зв'яжемося з вами.",
    );
    navigate("/support");
  };

  return (
    <SettingsSection
      id="delete-account"
      title="Видалення акаунта"
      description="Акаунт, ваші оголошення, історія замовлень та збережені товари буде видалено безповоротно. Активні замовлення потрібно завершити до видалення."
      icon={Trash2}
      danger
    >
      <div className={styles.sectionActions}>
        <Button variant="danger" onClick={() => setOpened(true)}>
          Видалити акаунт
        </Button>
      </div>

      <ConfirmModal
        opened={opened}
        onClose={() => setOpened(false)}
        onConfirm={handleConfirm}
        icon={Trash2}
        iconProps={{ size: 32, color: "#dc2626" }}
        title="Видалити акаунт?"
        message="Цю дію неможливо скасувати. Усі ваші дані буде втрачено."
        confirmLabel="Видалити"
      />
    </SettingsSection>
  );
}
