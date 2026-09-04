import { LogOut, ShieldCheck } from "lucide-react";
import { useAuthStore } from "@/features/auth/store/useAuthStore";

import styles from "./AdminHeader.module.css";
import { useNavigate } from "react-router-dom";
import { useLogout } from "@/features/auth/model/hooks/useLogout";
import { Button } from "@mantine/core";

export default function AdminHeader() {
  const user = useAuthStore((state) => state.user);
  const navigate = useNavigate();
  const logoutStore = useAuthStore((state) => state.logout);
  const logoutMutation = useLogout();

  const handleLogout = () => {
    logoutMutation.mutate(undefined, {
      onSuccess: () => {
        logoutStore();
        navigate("/", { replace: true });
      },
    });
  };

  return (
    <header className={styles.header}>
      <div className={styles.left}>
        <ShieldCheck size={24} color="#2563eb" />

        <div>
          <h1 className={styles.title}>Admin Panel</h1>
          <p className={styles.subtitle}>Manage marketplace content</p>
        </div>
      </div>

      <div className={styles.right}>
        <div className={styles.user}>
          <span className={styles.name}>{user?.name}</span>
          <span className={styles.role}>Administrator</span>
        </div>

        <Button onClick={() => handleLogout()}>
          <LogOut size={18} />
          Logout
        </Button>
      </div>
    </header>
  );
}
