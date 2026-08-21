import type { AdvertisementFormData } from "@/features/advertisement/advertisement-form/model/schemas/schema";
import type { AdvertisementDto } from "@/features/advertisement/advertisement-form/model/types";
import AdvertisementForm from "@/features/advertisement/advertisement-form/ui/AdvertisementForm";
import { useCreateAdvertisement } from "@/features/advertisement/create-advertisement/model/hooks/useCreateAdvertisement";
import type { ImageItem } from "@/shared/lib/types/image";
import { useNavigate } from "react-router-dom";

export default function CreateAdvertisementPage() {
  const navigate = useNavigate();
  const mutation = useCreateAdvertisement();

  async function handleCreate(
    data: AdvertisementFormData,
    images: ImageItem[],
  ) {
    const dto: AdvertisementDto = {
      ...data,
      previousPrice: data.price,
      sellerId: 8,
      currencyId: 0,
      categoryId: 1,
      conditionId: 0,
      images: images.map((x) => x.file!),
      properties: {
        Brand: "Apple",
      },
    };

    const advertisement = await mutation.mutateAsync(dto);

    navigate(`/advertisement/${advertisement.id}`);
  }

  return (
    <AdvertisementForm onSubmit={handleCreate} isLoading={mutation.isPending} />
  );
}
