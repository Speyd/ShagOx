import { useParams } from "react-router-dom";
import styles from "./AdvertisementPage.module.css";
import { useGetAdvertisement } from "@/entities/advertisement/model/hooks/useGetAdvertisement";
import Container from "@/shared/ui/container";
import Breadcrumbs from "@/widgets/breadcrumbs";
import { ImageGallery } from "@/widgets/image-gallery";
import AdvertisementInfo from "@/widgets/advertisement-info";
import ReviewsBlock from "@/widgets/reviews-block";
import ReviewsSection from "@/widgets/reviews-section";
import AdvertisementSlider from "@/widgets/advertisement-slider/ui/AdvertisementSlider";
import { useGetAdvertisements } from "@/entities/advertisement/model/hooks/useGetAdvertisements";

export default function AdvertisementPage() {
  const { id } = useParams<{ id: string }>();

  const { data: advertisement, isLoading } = useGetAdvertisement(Number(id));
  const { data: advertisements } = useGetAdvertisements();

  if (isLoading) {
    return <div>Loading...</div>;
  }

  if (!advertisement) {
    return <div>Advertisement not found</div>;
  }

  return (
    <div className={styles.wrapper}>
      <Container>
        <div className={styles.pageContent}>
          <Breadcrumbs lastItemLabel={advertisement?.title} />

          <section className={styles.topSection}>
            <ImageGallery
              images={advertisement.images?.map((image) => image.url) || []}
            />
            <AdvertisementInfo {...advertisement} />
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
    </div>
  );
}
