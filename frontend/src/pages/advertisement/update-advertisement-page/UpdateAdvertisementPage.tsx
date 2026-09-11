import { useState } from "react";
import { Navigate, useNavigate, useParams } from "react-router-dom";
import type { ImageItem } from "@/shared/lib/types/image";
import type { AdvertisementFormData } from "@/features/advertisement/advertisement-form/model/schemas/schema";
import useUpdateAdvertisement from "@/features/advertisement/update-advertisement/model/hooks/useUpdateAdvertisement";
import { useGetAdvertisement } from "@/entities/advertisement/model/hooks/useGetAdvertisement";
import AdvertisementForm from "@/features/advertisement/advertisement-form/ui/AdvertisementForm";
import type { UpdateAdvertisementRequestDto } from "@/features/advertisement/update-advertisement/model/types";
import styles from "./UpdateAdvertisementPage.module.css";
import { useAuthStore } from "@/features/auth/store/useAuthStore";

export default function UpdateAdvertisementPage() {
  const { id } = useParams();
  const navigate = useNavigate();

  const user = useAuthStore((state) => state.user);

  const { data: advertisement, isLoading } = useGetAdvertisement(Number(id));

  const mutation = useUpdateAdvertisement();
  const [deletedImageIds, setDeletedImageIds] = useState<number[]>([]);

  function handleImageDelete(image: ImageItem) {
    if (image.imageId == null) return;

    setDeletedImageIds((previous) =>
      previous.includes(image.imageId!)
        ? previous
        : [...previous, image.imageId!],
    );
  }

  async function handleUpdate(
    data: AdvertisementFormData,
    images: ImageItem[],
  ) {
    if (!advertisement) return;

    const imagesChanged =
      deletedImageIds.length > 0 ||
      images.length !== advertisement.images.length ||
      images.some(
        (image, index) =>
          image.file != null ||
          image.imageId == null ||
          image.imageId !== advertisement.images[index]?.id,
      );

    const dto: UpdateAdvertisementRequestDto = {
      title: data.title,
      description: data.description,
      price: data.price,
      properties: advertisement.properties,

      images: imagesChanged
        ? [
            ...images.map((image, index) => ({
              id: image.imageId,
              file: image.file,
              order: index,
              isDeleted: false,
            })),
            ...deletedImageIds.map((imageId) => ({
              id: imageId,
              order: 0,
              isDeleted: true,
            })),
          ]
        : undefined,
    };

    await mutation.mutateAsync({
      id: advertisement.id,
      data: dto,
    });

    navigate(`/advertisement/${advertisement.id}`);
  }

  if (isLoading) return <p>Loading...</p>;
  if (!advertisement) return <p>Advertisement not found</p>;
  if (user?.id !== advertisement.seller.id) {
    return <Navigate to={`/advertisement/${advertisement.id}`} replace />;
  }

  return (
    <div className={styles.updateAdvertisementPage}>
      <div className={styles.title}>
        <h2>Редагування оголошення</h2>
        <p>Внесіть необхідні зміни та збережіть їх.</p>
      </div>

      <AdvertisementForm
        key={advertisement.id}
        defaultValues={{
          title: advertisement.title,
          description: advertisement.description,
          price: advertisement.price,
        }}
        defaultImages={advertisement.images.map((image) => ({
          id: image.id.toString(),
          imageId: image.id,
          url: image.url,
        }))}
        onSubmit={handleUpdate}
        onImageDelete={handleImageDelete}
        isLoading={mutation.isPending}
      />
    </div>
  );
}
