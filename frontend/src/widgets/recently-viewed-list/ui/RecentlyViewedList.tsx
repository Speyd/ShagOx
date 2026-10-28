import { useRecentlyViewedStore } from "@/features/recently-viewed/ui/useRecentlyViewedStore.ts";
import AdvertisementSlider from "@/widgets/advertisement/advertisement-slider/ui/AdvertisementSlider";

export default function RecentlyViewedSection() {
  const items = useRecentlyViewedStore((state) => state.items);

  if (items.length === 0) {
    return null;
  }

  return (
    <AdvertisementSlider items={items || []} title="Нещодавно переглянуті" />
  );
}
