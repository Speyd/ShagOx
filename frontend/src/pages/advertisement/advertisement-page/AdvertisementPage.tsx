import { useParams } from "react-router-dom";
import styles from "./AdvertisementPage.module.css";
import { useGetAdvertisement } from "@/entities/advertisement/model/hooks/useGetAdvertisement";
import Container from "@/shared/ui/container";
import Breadcrumbs from "@/widgets/breadcrumbs";
import { ImageGallery } from "@/widgets/image-gallery";
import AdvertisementInfo from "@/widgets/advertisement/advertisement-info";
import ReviewsBlock from "@/widgets/reviews/reviews-block";
import ReviewsSection from "@/widgets/reviews/reviews-section";
import AdvertisementSlider from "@/widgets/advertisement/advertisement-slider/ui/AdvertisementSlider";
import { useGetAdvertisements } from "@/entities/advertisement/model/hooks/useGetAdvertisements";
import { useEffect } from "react";
import { useRecentlyViewedStore } from "@/features/recently-viewed/ui/useRecentlyViewedStore.ts";
import AdvertisementDetailsSkeleton from "@/widgets/advertisement/advertisement-detail-skeleton/ui/AdvertisementDetailsSkeleton";

export default function AdvertisementPage() {
  const { id } = useParams<{ id: string }>();

  const addProduct = useRecentlyViewedStore((state) => state.addProduct);

  const { data: advertisement, isLoading } = useGetAdvertisement(Number(id));
  const { data: advertisements } = useGetAdvertisements();

  useEffect(() => {
    if (advertisement) {
      addProduct(advertisement);
    }
  }, [advertisement, addProduct]);

  if (isLoading) {
    return (
      <section className={styles.wrapper}>
        <AdvertisementDetailsSkeleton />
      </section>
    );
  }

  if (!advertisement) {
    return <div>Advertisement not found</div>;
  }

  return (
    <section className={styles.wrapper}>
      <Container>
        <div className={styles.pageContent}>
          <Breadcrumbs
            category={advertisement.category}
            lastItemLabel={advertisement.title}
          />

          <section className={styles.topSection}>
            <ImageGallery
              images={advertisement.pictures?.map((image) => image.url) || []}
            />
            <div className={styles.infoColumn}>
              <AdvertisementInfo advertisement={advertisement} />
            </div>
          </section>

          <section className={styles.bottomSection}>
            <ReviewsBlock />
            <ReviewsSection />
            <AdvertisementSlider
              items={advertisements?.items || []}
              title="Вам також може сподобатись"
            />
          </section>
        </div>
      </Container>
    </section>
  );
}
