import Container from "@/shared/ui/container";
import styles from "./ProfilePage.module.css";
import { Text } from "@mantine/core";

export default function ProfilePage() {

  return <Container>
    <div className={styles.profilePage}><Text fw={700} fz={40}>Мій профіль</Text></div>
  </Container>;
}
