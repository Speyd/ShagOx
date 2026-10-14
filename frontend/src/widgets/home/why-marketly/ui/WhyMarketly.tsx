import { Image, Text } from "@mantine/core";
import styles from "./WhyMarketly.module.css";
import Container from "@/shared/ui/container";

import shield from "@/shared/assets/icons/shield.png";
import creditCard from "@/shared/assets/icons/credit-card.png";
import giftbox from "@/shared/assets/icons/giftbox.png";
import sync from "@/shared/assets/icons/sync.png";

const benefits = [
  {
    icon: shield,
    title: "Гарантія якості",
    description: (
      <>
        Офіційна гарантія
        <br />
        від виробника
      </>
    ),
  },
  {
    icon: creditCard,
    title: "Безпечна оплата",
    description: (
      <>
        Зручні та надійні <br />
        способи оплати
      </>
    ),
  },
  {
    icon: sync,
    title: "Легке повернення",
    description: (
      <>
        Повернення протягом
        <br /> 14 днів
      </>
    ),
  },
  {
    icon: giftbox,
    title: "Бонуси та кешбек",
    description: (
      <>
        Накопичуйте та
        <br /> економте більше
      </>
    ),
  },
];

export default function WhyMarketly() {
  return (
    <Container>
      <section className={styles.whyMarketly}>
        <div className={styles.content}>
          <div className={styles.titleContainer}>
            <Text fw={700} fz={28}>
              Чому обирають
            </Text>
            <Text className={styles.title} fw={700} fz={24}>
              Marketly?
            </Text>
          </div>

          <div className={styles.benefits}>
            {benefits.map((benefit) => (
              <div key={benefit.title} className={styles.benefit}>
                <Image
                  src={benefit.icon}
                  alt={benefit.title}
                  className={styles.icon}
                  w={65}
                  h={65}
                />
                <div className={styles.benefitText}>
                  <Text className={styles.benefitTitle} fw={600} fz={16}>
                    {benefit.title}
                  </Text>
                  <Text className={styles.description} fz={14}>
                    {benefit.description}
                  </Text>
                </div>
              </div>
            ))}
          </div>
        </div>
      </section>
    </Container>
  );
}
