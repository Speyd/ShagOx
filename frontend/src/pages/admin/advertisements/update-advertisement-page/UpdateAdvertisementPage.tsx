import { useNavigate, useParams } from "react-router-dom";
import AdvertisementForm from "@/features/advertisement/advertisement-form/ui/AdvertisementForm";
import { useGetAdvertisement } from "@/entities/advertisement/model/hooks/useGetAdvertisement";
import useAdminUpdateAdvertisement from "@/features/admin/advertisement/update-advertisement/model/hooks/useAdminUpdateAdvertisement";
import type { ImageItem } from "@/shared/lib/types/image";
import type { AdvertisementFormData } from "@/features/advertisement/advertisement-form/model/schemas/schema";
import type { UpdateAdvertisementRequestDto } from "@/features/admin/advertisement/update-advertisement/model/types";

import styles from "./UpdateAdvertisementPage.module.css";
import { getAdvertisementPrice } from "@/shared/lib/types/advertisements";

export default function UpdateAdvertisementPage() {
  const { id } = useParams();
  const navigate = useNavigate();

  const { data: advertisement, isLoading } = useGetAdvertisement(Number(id));

  const mutation = useAdminUpdateAdvertisement();

  async function handleUpdate(
    data: AdvertisementFormData,
    images: ImageItem[],
  ) {
    if (!advertisement) return;

    const dto: UpdateAdvertisementRequestDto = {
      title: data.title,
      description: data.description,
      attributes: advertisement.attributes,
      variants: advertisement.variants[0]
        ? {
            [advertisement.variants[0].id]: {
              price: data.price,
              stock: advertisement.variants[0].stock,
              attributes: {},
            },
          }
        : undefined,

      images: images.map((image, index) => ({
        id: image.imageId,
        file: image.file,
        order: index,
        isDeleted: false,
      })),
    };

    await mutation.mutateAsync({
      id: advertisement.id,
      data: dto,
    });

    navigate("/admin/advertisements");
  }

  if (isLoading) return <p>Loading...</p>;

  if (!advertisement) return <p>Advertisement not found</p>;

  return (
    <div className={styles.updateAdvertisementPage}>
      <div className={styles.title}>
        <h2>Змінити оголошення</h2>
        <p>Заповніть всі поля для зміни оголошення</p>
      </div>

      <AdvertisementForm
        key={advertisement.id}
        defaultValues={{
          title: advertisement.title,
          description: advertisement.description,
          price: getAdvertisementPrice(advertisement),
        }}
        defaultImages={advertisement.images.map((image) => ({
          id: image.id.toString(),
          imageId: image.id,
          url: image.url,
        }))}
        onSubmit={handleUpdate}
        isLoading={mutation.isPending}
      />
    </div>
  );
}
