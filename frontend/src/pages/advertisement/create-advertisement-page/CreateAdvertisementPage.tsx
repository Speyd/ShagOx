import type { AdvertisementFormData } from "@/features/advertisement/advertisement-form/model/schemas/schema";
import type { AdvertisementDto } from "@/features/advertisement/advertisement-form/model/types";
import AdvertisementForm from "@/features/advertisement/advertisement-form/ui/AdvertisementForm";
import { useCreateAdvertisement } from "@/features/advertisement/create-advertisement/model/hooks/useCreateAdvertisement";
import type { ImageItem } from "@/shared/lib/types/image";
import { useNavigate } from "react-router-dom";
import styles from "./CreateAdvertisementPage.module.css";
import { useAuthStore } from "@/features/auth/store/useAuthStore";

export default function CreateAdvertisementPage() {
  const navigate = useNavigate();
  const user = useAuthStore((state) => state.user);
  const mutation = useCreateAdvertisement();

  async function handleCreate(
    data: AdvertisementFormData,
    images: ImageItem[],
  ) {
    const validFiles = images
      .map((x) => x.file)
      .filter((file): file is File => file instanceof File);

    const dto: AdvertisementDto = {
      ...data,
      previousPrice: data.price,
      sellerId: user?.id ?? 0,
      currencyId: 0,
      categoryId: 1,
      conditionId: 0,
      stock: 1,
      images: validFiles,
      properties: {
        Brand: "Apple",
      },
    };

    const advertisement = await mutation.mutateAsync(dto);

    navigate(`/advertisement/${advertisement.id}`);
  }

  return (
    <div className={styles.createAdvertisementPage}>
      <div className={styles.title}>
        <h2>Створення оголошення</h2>
        <p>Заповніть форму та завантажте фотографії.</p>
      </div>

      <AdvertisementForm
        onSubmit={handleCreate}
        isLoading={mutation.isPending}
      />
    </div>
  );
}
