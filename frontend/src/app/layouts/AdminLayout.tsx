import { Outlet } from "react-router-dom";
import styles from "./AdminLayout.module.css";
import AdminSidebar from "@/widgets/admin-layout/AdminSidebar";
import AdminHeader from "@/widgets/admin-layout/AdminHeader";

export default function AdminLayout() {
  return (
    <div className={styles.layout}>
      <AdminSidebar />

      <div className={styles.content}>
        <AdminHeader />

        <main className={styles.main}>
          <Outlet />
        </main>
      </div>
    </div>
  );
}
