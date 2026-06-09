import Container from "@/shared/ui/Container";
import Footer from "@/widgets/Footer";
import Header from "@/widgets/Header";
import { Outlet } from "react-router-dom";
import styles from "./MainLayout.module.css";

export default function MainLayout() {
  return (
    <div className={styles.layout}>
      <Header />

      <main className={styles.content}>
        <Container>
          <Outlet />
        </Container>
      </main>

      <Footer />
    </div>
  );
}
