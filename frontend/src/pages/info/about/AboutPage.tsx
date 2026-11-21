import { Text, Title, SimpleGrid, Group, Image } from "@mantine/core";
import {
  ShieldCheck,
  Zap,
  Truck,
  Store,
  CheckCircle2,
  Headphones,
} from "lucide-react";
import { useNavigate } from "react-router-dom";
import Button from "@/shared/ui/buttons/button";
import logo from "@/shared/assets/images/logo.png";
import styles from "./AboutPage.module.css";
import Container from "@/shared/ui/layout/container";

const STATS = [
  { value: "100K+", label: "Товарів у каталозі" },
  { value: "500+", label: "Перевірених продавців" },
  { value: "250K+", label: "Задоволених покупців" },
  { value: "99%", label: "Успішних доставок" },
];

const FEATURES = [
  {
    icon: ShieldCheck,
    title: "100% Гарантія якості",
    description:
      "Кожен продавець та товар проходять суворий модераційний відбір перед публікацією.",
  },
  {
    icon: Zap,
    title: "Швидкий пошук та порівняння",
    description:
      "Зручні фільтри та інструменти порівняння допоможуть обрати ідеальну техніку за секунди.",
  },
  {
    icon: Truck,
    title: "Надійна доставка",
    description:
      "Співпрацюємо з провідними логістичними службами для оперативного вручення замовлень.",
  },
  {
    icon: Headphones,
    title: "Підтримка 24/7",
    description:
      "Наша команда завжди на зв'язку, щоб допомогти з будь-яким питанням чи спірною ситуацією.",
  },
];

const VALUES = [
  "Прозорість цін без прихованих комісій",
  "Безпечні платежі та захист покупців",
  "Тільки оригінальна гаджети та електроніка",
  "Зручний повернення та гарантійний сервіс",
];

export default function AboutPage() {
  const navigate = useNavigate();

  return (
    <main className={styles.aboutPage}>
      <Container className={styles.container}>
        <section className={styles.heroSection}>
          <div className={styles.heroContent}>
            <div className={styles.badge}>
              <Store size={16} />
              <span>Маркетплейс техніки майбутнього</span>
            </div>

            <Title className={styles.heroTitle}>
              Твій надійний провідник у світі{" "}
              <span className={styles.highlight}>інновацій</span>
            </Title>

            <Text className={styles.heroDescription}>
              <strong>Marketly</strong> — це сучасний маркетплейс, що об'єднує
              найкращих продавців електроніки та тисячі покупців по всій країні.
              Ми робимо процес купівлі та продажу техніки максимально простим,
              безпечним і вигідним.
            </Text>

            <Group gap="md" className={styles.heroButtons}>
              <Button
                onClick={() => navigate("/catalog")}
                className={styles.primaryBtn}
              >
                Перейти до каталогу
              </Button>
              <Button
                onClick={() => navigate("/support")}
                variant="outline"
                className={styles.secondaryBtn}
              >
                Зв'язатися з нами
              </Button>
            </Group>
          </div>

          <div className={styles.logoCard}>
            <Image
              src={logo}
              alt="Marketly Logo"
              className={styles.logoImage}
            />
          </div>
        </section>

        <section className={styles.statsSection}>
          <SimpleGrid
            cols={{ base: 2, md: 4 }}
            spacing={{ base: "md", md: "xl" }}
          >
            {STATS.map((stat) => (
              <div key={stat.label} className={styles.statCard}>
                <Text className={styles.statValue}>{stat.value}</Text>
                <Text className={styles.statLabel}>{stat.label}</Text>
              </div>
            ))}
          </SimpleGrid>
        </section>

        <section className={styles.featuresSection}>
          <div className={styles.sectionHeader}>
            <Title order={2} className={styles.sectionTitle}>
              Чому обирають Marketly
            </Title>
            <Text className={styles.sectionSubtitle}>
              Ми створюємо найкращі умови для купівлі електроніки та гаджетів
            </Text>
          </div>

          <SimpleGrid cols={{ base: 1, sm: 2, lg: 4 }} spacing="lg">
            {FEATURES.map((feature) => {
              const Icon = feature.icon;
              return (
                <div key={feature.title} className={styles.featureCard}>
                  <div className={styles.iconWrapper}>
                    <Icon size={26} />
                  </div>
                  <Text className={styles.featureTitle}>{feature.title}</Text>
                  <Text className={styles.featureDescription}>
                    {feature.description}
                  </Text>
                </div>
              );
            })}
          </SimpleGrid>
        </section>

        <section className={styles.valuesSection}>
          <div className={styles.valuesCard}>
            <div className={styles.valuesContent}>
              <Title order={2} className={styles.valuesTitle}>
                Наша місія
              </Title>
              <Text className={styles.valuesText}>
                Надати кожному доступ до сучасної якісної техніки за чесною
                ціною, гарантуючи абсолютну безпеку угод та високий рівень
                сервісу на кожному етапі замовлення.
              </Text>

              <div className={styles.valuesList}>
                {VALUES.map((val) => (
                  <div key={val} className={styles.valueItem}>
                    <CheckCircle2 size={20} className={styles.checkIcon} />
                    <Text className={styles.valueText}>{val}</Text>
                  </div>
                ))}
              </div>
            </div>
          </div>
        </section>

        <section className={styles.ctaSection}>
          <Title order={2} className={styles.ctaTitle}>
            Готові оновити свої гаджети?
          </Title>
          <Text className={styles.ctaText}>
            Знайдіть потрібну техніку за найкращими цінами вже зараз.
          </Text>
          <Button
            onClick={() => navigate("/catalog")}
            className={styles.ctaButton}
          >
            Розпочати покупки
          </Button>
        </section>
      </Container>
    </main>
  );
}
