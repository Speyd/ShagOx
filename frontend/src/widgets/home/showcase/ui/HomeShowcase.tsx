import Container from "@/shared/ui/layout/container";
import AdvertisementSlider from "@/widgets/advertisement/advertisement-slider/ui/AdvertisementSlider";
import { useGetCatalogShowcase } from "@/entities/advertisement/model/hooks/useGetCatalogShowcase";
import type { CatalogSort } from "@/entities/advertisement/api/api";

type Props = {
  title: string;
  sort: Exclude<CatalogSort, "Default">;
  pageSize?: number;
};

/**
 * Real products on the home page (newest / discounted).
 * The slider hides itself when the list is empty.
 */
export default function HomeShowcase({ title, sort, pageSize = 12 }: Props) {
  const { data = [], isLoading } = useGetCatalogShowcase(sort, pageSize);

  if (!isLoading && data.length === 0) return null;

  return (
    <Container>
      <AdvertisementSlider title={title} items={data} isLoading={isLoading} />
    </Container>
  );
}
