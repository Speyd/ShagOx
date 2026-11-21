import Container from "@/shared/ui/layout/container";
import AdvertisementSlider from "@/widgets/advertisement/advertisement-slider/ui/AdvertisementSlider";
import { useGetCatalogAdvertisements } from "@/entities/advertisement/model/hooks/useGetCatalogAdvertisements";

type Props = {
  title: string;
  newest?: boolean;
  discounts?: boolean;
  pageSize?: number;
};

export default function HomeShowcase({
  title,
  newest,
  discounts,
  pageSize = 12,
}: Props) {
  const { data, isLoading } = useGetCatalogAdvertisements({
    pageSize,
    newest,
    discounts,
  });
  const items = data?.items ?? [];

  if (!isLoading && items.length === 0) return null;

  return (
    <Container>
      <AdvertisementSlider title={title} items={items} isLoading={isLoading} />
    </Container>
  );
}
