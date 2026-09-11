import { Outlet } from "react-router-dom";
import styles from "./MainLayout.module.css";
import { Header } from "@/widgets/header";
import Footer from "@/widgets/footer";

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