import { useState } from "react";
import { Carousel } from "@mantine/carousel";
import { Text, UnstyledButton } from "@mantine/core";
import { ArrowLeft, ArrowRight } from "lucide-react";
import type { EmblaCarouselType } from "embla-carousel";
import styles from "./AdvertisementSlider.module.css";
import type { Advertisement } from "@/shared/lib/types/advertisements";
import AdvertisementCard from "@/entities/advertisement/ui/AdvertisementCard";

interface AdvertisementSliderProps {
  title: string;
  items: Advertisement[];
  isLoading?: boolean;
}

export default function AdvertisementSlider({
  title,
  items,
  isLoading = false,
}: AdvertisementSliderProps) {
  const [embla, setEmbla] = useState<EmblaCarouselType | null>(null);
  const [canScrollPrev, setCanScrollPrev] = useState(false);
  const [canScrollNext, setCanScrollNext] = useState(true);

  const handleSelect = () => {
    if (!embla) return;
    setCanScrollPrev(embla.canScrollPrev());
    setCanScrollNext(embla.canScrollNext());
  };

  if (isLoading) {
    return <div>Loading...</div>;
  }

  if (!items.length) {
    return null;
  }

  return (
    <section className={styles.wrapper}>
      <div className={styles.header}>
        <Text fw={600} size="xl" c="#0f172a">
          {title}
        </Text>
      </div>

      <Carousel
        getEmblaApi={setEmbla}
        onSlideChange={handleSelect}
        slideSize={{ base: "100%", sm: "50%", md: "25%" }}
        slideGap="md"
        withControls={false}
        className={styles.carousel}
      >
        {items.map((item) => (
          <Carousel.Slide key={item.id} className={styles.slide}>
            <AdvertisementCard {...item} />
          </Carousel.Slide>
        ))}
      </Carousel>

      <div className={styles.controls} style={{ justifyContent: "flex-end" }}>
        <UnstyledButton
          className={styles.navButton}
          onClick={() => embla?.scrollPrev()}
          disabled={!canScrollPrev}
          aria-label="Previous slide"
        >
          <ArrowLeft size={20} />
        </UnstyledButton>

        <UnstyledButton
          className={styles.navButton}
          onClick={() => embla?.scrollNext()}
          disabled={!canScrollNext}
          aria-label="Next slide"
        >
          <ArrowRight size={20} />
        </UnstyledButton>
      </div>
    </section>
  );
}
