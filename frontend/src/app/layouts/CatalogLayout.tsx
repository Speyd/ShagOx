import { Outlet } from "react-router-dom";
import Container from "@/shared/ui/container";
import Breadcrumbs from "@/widgets/breadcrumbs";
import { Text } from "@mantine/core";
import styles from "./CatalogLayout.module.css";

export default function CatalogLayout() {
  return (
    <section className={styles.catalogLayout}>
      <Container>
        <div className={styles.catalogContainer}>
          <Breadcrumbs />
          <div className={styles.catalogHeader}>
            <Text fw={700} fz={40}>
              Каталог
            </Text>
          </div>
          <div className={styles.catalogContent}>
            <Outlet />
          </div>
        </div>
      </Container>
    </section>
  );
}
