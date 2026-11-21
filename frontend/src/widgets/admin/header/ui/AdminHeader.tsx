import { LogOut, Menu } from "lucide-react";
import { useNavigate } from "react-router-dom";
import { ActionIcon, Avatar, Tooltip } from "@mantine/core";
import { useDisclosure } from "@mantine/hooks";
import { useAuthStore } from "@/features/auth";
import { useLogout } from "@/features/auth/model/hooks/useLogout";
import ConfirmModal from "@/shared/ui/feedback/confirm-modal";
import styles from "./AdminHeader.module.css";

type AdminHeaderProps = {
  onMenuClick: () => void;
};

export default function AdminHeader({ onMenuClick }: AdminHeaderProps) {
  const user = useAuthStore((state) => state.user);
  const logoutStore = useAuthStore((state) => state.logout);
  const logoutMutation = useLogout();
  const navigate = useNavigate();
  const [opened, { open, close }] = useDisclosure(false);

  const handleLogout = () => {
    logoutMutation.mutate(undefined, {
      onSuccess: () => {
        logoutStore();
        close();
        navigate("/", { replace: true });
      },
    });
  };

  return (
    <header className={styles.header}>
      <button
        type="button"
        className={styles.menuButton}
        onClick={onMenuClick}
        aria-label="Відкрити меню"
      >
        <Menu size={24} />
      </button>

      <div className={styles.right}>
        <div className={styles.user}>
          <Avatar color="blue" radius="xl">
            {user?.userName?.[0]?.toUpperCase()}
          </Avatar>
          <div className={styles.userInfo}>
            <span className={styles.name}>{user?.userName}</span>
            <span className={styles.role}>Адміністратор</span>
          </div>
        </div>

        <Tooltip label="Вийти">
          <ActionIcon
            variant="light"
            color="red"
            size="lg"
            radius="md"
            onClick={open}
            aria-label="Вийти"
          >
            <LogOut size={18} />
          </ActionIcon>
        </Tooltip>
      </div>

      <ConfirmModal
        opened={opened}
        onClose={close}
        onConfirm={handleLogout}
        title="Вихід з акаунту"
        message="Ви впевнені, що хочете вийти з акаунту?"
        confirmLabel="Вийти"
        confirmColor="red"
        isLoading={logoutMutation.isPending}
      />
    </header>
  );
}
