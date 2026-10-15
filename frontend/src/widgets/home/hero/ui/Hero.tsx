import { Image, Text, Title } from "@mantine/core";
import styles from "./Hero.module.css";
import heroImage from "@/shared/assets/images/hero-headphones.png";
import Container from "@/shared/ui/container";
import { useNavigate } from "react-router-dom";

export default function Hero() {
  const navigate = useNavigate();
  return (
    <section className={styles.hero}>
      <Image src={heroImage} alt="Навушники" className={styles.image} />

      <div className={styles.overlay}>
        <Container>
          <div className={styles.content}>
            <Title className={styles.title} fz={90} fw={600} lh={0.95}>
              Технології
              <br />
              майбутнього
              <br />
              вже тут
            </Title>

            <Text className={styles.description} fz={25} fw={500}>
              Відчуйте досконалість у кожній деталі
            </Text>

            <button
              className={styles.heroButton}
              onClick={() => navigate("/catalog")}
            >
              Обрати техніку
            </button>
          </div>
        </Container>
      </div>
    </section>
  );
}