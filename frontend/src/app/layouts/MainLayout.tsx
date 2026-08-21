import Container from "@/shared/ui/container";
import Footer from "@/widgets/footer";
import Header from "@/widgets/header";
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
