import { Image, Text } from "@mantine/core";
import styles from "./PromoBlocks.module.css";
import { useNavigate } from "react-router-dom";
import Container from "@/shared/ui/layout/container";

const promo = [
  {
    label: "Знижки до -20%",
    title: "Камери найкращої якості",
    image:
      "https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791526477/854fee754069713693df4ed25b7eb2584b6f1ed6_1_iobvdp.png",
    link: `/catalog?search=${encodeURIComponent("Canon")}`,
  },
  {
    label: "Ексклюзивна пропозиція",
    title: "Аксесуари Apple",
    image:
      "https://res.cloudinary.com/dvq5kvhpy/image/upload/v1791526478/4fb6af7ab51099af3cdef380b96905d10a9f2253_1_wxhfgj.png",
    link: `/catalog?search=${encodeURIComponent("Apple")}`,
  },
];

export default function PromoBlocks() {
  const navigate = useNavigate();

  return (
    <Container>
      <section className={styles.promoBlock}>
        {promo.map((item, index) => (
          <article
            className={styles.promoBlockCard}
            key={index}
            onClick={() => navigate(item.link)}
            style={{ cursor: "pointer" }}
          >
            <Image
              src={item.image}
              alt={item.title}
              className={styles.image}
              bdrs={12}
            />
            <div className={styles.info}>
              <div className={styles.labelWrapper}>
                <Text className={styles.label}>{item.label}</Text>
              </div>
              <Text component="h3" className={styles.title}>
                {item.title}
              </Text>
              <button
                className={styles.button}
                onClick={() => navigate(item.link)}
              >
                Переглянути
              </button>
            </div>
          </article>
        ))}
      </section>
    </Container>
  );
}
