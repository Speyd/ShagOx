import { Outlet } from "react-router-dom";
import styles from "./MainLayout.module.css";

import Footer from "@/widgets/footer";
import Header from "@/widgets/header";

export default function MainLayout() {
  return (
    <div className={styles.layout}>
      <Header />

      <main className={styles.content}>

        <Outlet />

      </main>

      <Footer />
    </div>
  );
}