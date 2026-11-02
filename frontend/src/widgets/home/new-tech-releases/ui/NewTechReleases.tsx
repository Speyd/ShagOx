import { Text } from "@mantine/core";
import { useNavigate } from "react-router-dom";
import styles from "./NewTechReleases.module.css";
import ReleaseCard from "./ReleaseCard";
import Container from "@/shared/ui/layout/container";

const releases = [
  {
    title: "iPhone 15 Pro Max",
    description: "The latest iPhone with Pro Max features.",
    image:
      "https://res.cloudinary.com/dvq5kvhpy/image/upload/v1793649332/594345298_tik9id.webp",
    price: 1120,
    searchQuery: "iPhone 15 Pro Max",
  },
  {
    title: "Samsung Galaxy S23 Ultra",
    description: "The latest Samsung Galaxy with Ultra features.",
    image:
      "https://res.cloudinary.com/dvq5kvhpy/image/upload/v1793649480/532097917_m1aysg.webp",
    price: 320,
    searchQuery: "Samsung Galaxy S23 Ultra",
  },
  {
    title: "Google Pixel 7 Pro",
    description: "The latest Google Pixel with Pro features.",
    image:
      "https://res.cloudinary.com/dvq5kvhpy/image/upload/v1793649509/308403743_dkfsum.webp",
    price: 5000,
    searchQuery: "Google Pixel 7 Pro",
  },
];

export default function NewTechReleases() {
  const navigate = useNavigate();

  const handleCardClick = (query: string) => {
    navigate(`/catalog?search=${encodeURIComponent(query)}`);
  };

  return (
    <Container>
      <section className={styles.newTechReleases}>
        <div className={styles.header}>
          <Text fw={700} fz={{ base: 20, sm: 24 }}>
            Новинки техніки
          </Text>
        </div>

        <div className={styles.content}>
          {releases.map((release) => (
            <ReleaseCard
              key={release.title}
              {...release}
              onClick={() => handleCardClick(release.searchQuery)}
            />
          ))}
        </div>
      </section>
    </Container>
  );
}
