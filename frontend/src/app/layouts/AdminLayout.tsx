import { Suspense } from "react";
import { Outlet } from "react-router-dom";
import { Drawer, Loader } from "@mantine/core";
import { useDisclosure } from "@mantine/hooks";
import AdminSidebar from "@/widgets/admin/sidebar/ui/AdminSidebar";
import AdminHeader from "@/widgets/admin/header/ui/AdminHeader";
import styles from "./AdminLayout.module.css";

export default function AdminLayout() {
  const [opened, { open, close }] = useDisclosure(false);

  return (
    <div className={styles.layout}>
      <div className={styles.sidebar}>
        <AdminSidebar />
      </div>

      <Drawer
        opened={opened}
        onClose={close}
        size={272}
        padding={0}
        withCloseButton={false}
      >
        <AdminSidebar onNavigate={close} />
      </Drawer>

      <div className={styles.content}>
        <AdminHeader onMenuClick={open} />
        <main className={styles.main}>
          <Suspense fallback={<Loader mx="auto" display="block" mt="xl" />}>
            <Outlet />
          </Suspense>
        </main>
      </div>
    </div>
  );
}
