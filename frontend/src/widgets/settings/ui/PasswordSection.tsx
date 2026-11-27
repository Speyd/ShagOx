import { KeyRound } from "lucide-react";
import { ChangePasswordForm } from "@/features/auth";
import SettingsSection from "./SettingsSection";
import styles from "./Settings.module.css";

export default function PasswordSection() {
  return (
    <SettingsSection
      id="password"
      title="Зміна пароля"
      description="Регулярна зміна пароля допомагає захистити ваш обліковий запис від несанкціонованого доступу. Для підтвердження ми надішлемо код на вашу пошту або телефон."
      icon={KeyRound}
    >
      <ChangePasswordForm
        className={styles.narrowForm}
        submitLabel="Оновити пароль"
        showPasswordHint
      />
    </SettingsSection>
  );
}
