import { useForm } from "react-hook-form";
import useUpdateAdvertisement from "../model/hooks/useAdminUpdateAdvertisement";
import { useState } from "react";
import Button from "@/shared/ui/button";
import { zodResolver } from "@hookform/resolvers/zod";

import styles from "./UpdateAdvertisementForm.module.css";
import Input from "@/shared/ui/input";
import TextArea from "@/shared/ui/text-area";
import ImageUploader from "@/shared/ui/image-uploader";
import {
  adminUpdateAdvertisementSchema,
  type UpdateAdvertisementDto,
} from "../model/schemas/schema";
import { useGetAdvertisement } from "@/entities/advertisement/model/hooks/useGetAdvertisement";
import type { ImageItem } from "@/shared/lib/types/image";
import type { Advertisement } from "@/shared/lib/types/advertisements";

type UpdateAdvertisementFormProps = {
  advertisementId: number;
};

export default function AdminUpdateAdvertisementForm({
  advertisementId,
}: UpdateAdvertisementFormProps) {
  const { data: advertisement, isLoading } = useGetAdvertisement(advertisementId);

  if (isLoading) return <p>Loading...</p>;
  if (!advertisement) return <p>Advertisement not found</p>;

  return (
    <AdminUpdateAdvertisementFormContent
      key={advertisement.id}
      advertisement={advertisement}
    />
  );
}

function AdminUpdateAdvertisementFormContent({
  advertisement,
}: {
  advertisement: Advertisement;
}) {
  const mutation = useUpdateAdvertisement();

  const [images, setImages] = useState<ImageItem[]>(() =>
    advertisement.images.map((image) => ({
      id: String(image.id),
      url: image.url,
      imageId: image.id,
    })),
  );
  const [deletedImages, setDeletedImages] = useState<number[]>([]);
  const [imagesError, setImagesError] = useState<string>("");

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<UpdateAdvertisementDto>({
    resolver: zodResolver(adminUpdateAdvertisementSchema),
    defaultValues: {
      title: advertisement.title,
      description: advertisement.description,
      price: advertisement.price,
    },
  });

  const removeImage = (image: ImageItem) => {
    if (image.imageId) {
      setDeletedImages((prev) => [...prev, image.imageId!]);
    }

    setImages((prev) => prev.filter((x) => x.id !== image.id));
  };

  const onSubmit = async (data: UpdateAdvertisementDto) => {
    if (images.length < 2) {
      setImagesError("Потрібно додати мінімум 2 фотографії");
      return;
    } else if (images.length > 10) {
      setImagesError("Максимум 10 фотографій");
      return;
    }

    await mutation.mutateAsync({
      id: advertisement.id,

      data: {
        ...data,

        images: [
          ...images.map((image, index) => ({
            id: image.imageId,
            file: image.file,
            order: index,
            isDeleted: false,
          })),

          ...deletedImages.map((id) => ({
            id,
            file: undefined,
            order: 0,
            isDeleted: true,
          })),
        ],
      },
    });

    setImagesError("");
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)} className={styles.form}>
      <div className={styles.inputWrapper}>
        <label className={styles.label}>Title</label>

        <div>
          <Input {...register("title")} placeholder="Enter title" />
          {errors.title && <p className="error">{errors.title.message}</p>}
        </div>
      </div>

      <div className={styles.inputWrapper}>
        <label className={styles.label}>Description</label>

        <div>
          <TextArea
            {...register("description")}
            placeholder="Enter description"
          />
          {errors.description && (
            <p className="error">{errors.description.message}</p>
          )}
        </div>
      </div>

      <div className={styles.inputWrapper}>
        <label className={styles.label}>Price</label>
        <div>
          <Input
            type="number"
            {...register("price", { valueAsNumber: true })}
            placeholder="Enter price"
          />
          {errors.price && <p className="error">{errors.price.message}</p>}
        </div>
      </div>

      <div className={styles.inputWrapper}>
        <label className={styles.label}>Фото</label>
        <p>
          Перше фото буде на обкладинці оголошення. Перетягніть, щоб змінити
          порядок фото.
        </p>

        <div className={styles.images}>
          <ImageUploader
            images={images}
            setImages={setImages}
            setImagesError={setImagesError}
            onDelete={removeImage}
          />

          {imagesError && <p className="error">{imagesError}</p>}
        </div>
      </div>

      <Button type="submit" disabled={mutation.isPending}>
        {mutation.isPending ? "Updating..." : "Update advertisement"}
      </Button>
    </form>
  );
}
