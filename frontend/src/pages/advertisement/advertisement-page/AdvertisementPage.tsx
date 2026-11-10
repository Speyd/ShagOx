import { useParams } from "react-router-dom";
import styles from "./AdvertisementPage.module.css";
import { useGetAdvertisement } from "@/entities/advertisement/model/hooks/useGetAdvertisement";
import Container from "@/shared/ui/layout/container";
import Breadcrumbs from "@/shared/ui/navigation/breadcrumbs";
import { ImageGallery } from "@/shared/ui/data-display/image-gallery";
import ReviewsBlock from "@/widgets/advertisement/reviews/reviews-block";
import ReviewsSection from "@/widgets/advertisement/reviews/reviews-section";
import AdvertisementSlider from "@/widgets/advertisement/advertisement-slider/ui/AdvertisementSlider";
import { useEffect } from "react";
import { useRecentlyViewedStore } from "@/features/recently-viewed/ui/useRecentlyViewedStore.ts";
import AdvertisementDetailsSkeleton from "@/widgets/advertisement/advertisement-detail-skeleton/ui/AdvertisementDetailsSkeleton";
import { AdvertisementInfo } from "@/widgets/advertisement/advertisement-info";
import { useGetCatalogAdvertisements } from "@/entities/advertisement/model/hooks/useGetCatalogAdvertisements";

export function AdvertisementPage() {
  const { id } = useParams<{ id: string }>();

  const addProduct = useRecentlyViewedStore((state) => state.addProduct);

  const { data: advertisement, isLoading } = useGetAdvertisement(Number(id));
  const { data: advertisements } = useGetCatalogAdvertisements({
    pageSize: 20,
  });

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
              <AdvertisementInfo
                key={advertisement.id}
                advertisement={advertisement}
              />
            </div>
          </section>

          <section className={styles.bottomSection}>
            <ReviewsBlock advertisementId={advertisement.id} />
            <ReviewsSection
              advertisementId={advertisement.id}
              sellerId={advertisement.seller?.id}
            />
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
