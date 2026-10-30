import styles from "./CategoriesBlock.module.css";
import Container from "@/shared/ui/container";
import { Image, Text } from "@mantine/core";
import { Carousel } from "@mantine/carousel";
import { ChevronLeft, ChevronRight } from "lucide-react";

import smartphone from "@/shared/assets/icons/mobile-phone.png";
import laptop from "@/shared/assets/icons/laptop.png";
import headphones from "@/shared/assets/icons/headphones.png";
import camera from "@/shared/assets/icons/camera.png";
import joystick from "@/shared/assets/icons/joystick.png";
import smartwatch from "@/shared/assets/icons/smartwatch.png";
import monitor from "@/shared/assets/icons/monitor.png";
import tablet from "@/shared/assets/icons/tablet.png";
import { Link } from "react-router-dom";
import { useGetCategories } from "@/entities/advertisement/model/hooks/useGetCategories";

const categories = [
  {
    title: "Смартфони",
    icon: smartphone,
    keys: ["смартфон", "smartphone", "phone"],
  },
  { title: "Ноутбуки", icon: laptop, keys: ["ноутбук", "laptop"] },
  { title: "Аудіо", icon: headphones, keys: ["аудіо", "audio", "headphone"] },
  { title: "Камери", icon: camera, keys: ["камера", "camera"] },
  { title: "Геймінг", icon: joystick, keys: ["геймінг", "gaming"] },
  {
    title: "Телевізори",
    icon: monitor,
    keys: ["телевізор", "телевизор", "tv", "television"],
  },
  { title: "Планшети", icon: tablet, keys: ["планшет", "tablet"] },
  {
    title: "Аксесуари",
    icon: smartwatch,
    keys: ["аксесуар", "accessor"],
  },
];

export default function CategoriesBlock() {
  const { data: availableCategories = [] } = useGetCategories();

  const normalize = (value: string) => value.trim().toLocaleLowerCase();

  return (
    <Container>
      <section className={styles.categoriesBlock}>
        <Text fw={700} fz={{ base: 20, sm: 24 }}>
          Категорії
        </Text>

        <Carousel
          slideSize={{
            base: "33.333333%",
            xs: "25%",
            sm: "20%",
            md: "16.666667%",
          }}
          slideGap={{ base: "xs", sm: "md" }}
          nextControlIcon={<ChevronRight size={20} />}
          previousControlIcon={<ChevronLeft size={20} />}
          classNames={{
            root: styles.carouselRoot,
            controls: styles.carouselControls,
            control: styles.carouselControl,
          }}
        >
          {categories.map((category) => {
            const matchedCategory = availableCategories.find((item) => {
              const categoryName = normalize(item.label);
              return category.keys.some((key) =>
                categoryName.includes(normalize(key)),
              );
            });
            const categoryLink = matchedCategory
              ? `/catalog?category=${matchedCategory.id}`
              : "/catalog";

            return (
              <Carousel.Slide
                key={category.title}
                className={styles.itemContainer}
              >
                <Link to={categoryLink} className={styles.categoryItem}>
                  <div className={styles.categoryCard}>
                    <Image
                      src={category.icon}
                      alt={category.title}
                      className={styles.icon}
                    />
                    <Text className={styles.categoryTitle} fw={600}>
                      {category.title}
                    </Text>
                  </div>
                </Link>
              </Carousel.Slide>
            );
          })}
        </Carousel>
      </section>
    </Container>
  );
}
