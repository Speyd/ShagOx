import { useForm } from "react-hook-form";
import useUpdateAdvertisement from "../hooks/useUpdateAdvertisement";
import { useGetAdvertisement } from "@/entities/Advertisement/hooks/useGetAdvertisement";
import { useEffect, useState } from "react";
import Button from "@/shared/ui/Button";
import { zodResolver } from "@hookform/resolvers/zod";
import { updateAdvertisementSchema } from "../model/schema";
import styles from "./UpdateAdvertisementForm.module.css";
import Input from "@/shared/ui/Input";
import TextArea from "@/shared/ui/TextArea";
import ImageUploader from "@/shared/ui/ImageUploader";
import type { ImageItem } from "@/types/advertisements";
import type { UpdateAdvertisementDto } from "../model/types";
import { useUpdateImagesOrder } from "../hooks/useUpdateImagesOrder";

type UpdateAdvertisementFormProps = {
  advertisementId: number;
};

export default function UpdateAdvertisementForm({
  advertisementId,
}: UpdateAdvertisementFormProps) {
  const mutation = useUpdateAdvertisement();
  const updateOrderMutation = useUpdateImagesOrder();
  const { data: advertisement } = useGetAdvertisement(advertisementId);

  const [images, setImages] = useState<ImageItem[]>([]);
  const [deletedImages, setDeletedImages] = useState<number[]>([]);
  const [imagesError, setImagesError] = useState<string>("");

  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm({
    resolver: zodResolver(updateAdvertisementSchema),
  });

  const removeImage = (image: ImageItem) => {
    if (image.imageId) {
      setDeletedImages((prev) => [...prev, image.imageId!]);
    }

    setImages((prev) => prev.filter((x) => x.id !== image.id));
  };

  useEffect(() => {
    if (advertisement) {
      setImages(
        advertisement.images.map((image) => ({
          id: crypto.randomUUID(),
          imageId: image.id,
          url: image.url,
        })),
      );

      reset({
        title: advertisement.title,
        description: advertisement.description,
        price: advertisement.price,
      });
    }
  }, [advertisement, reset]);

  const onSubmit = async (data: UpdateAdvertisementDto) => {
    if (images.length < 2) {
      setImagesError("Потрібно додати мінімум 2 фотографії");
      return;
    } else if (images.length > 10) {
      setImagesError("Максимум 10 фотографій");
      return;
    }

    await mutation.mutateAsync({
      id: advertisementId,
      data: {
        ...data,

        newImages: images.filter((x) => x.file).map((x) => x.file!),

        deletedImageIds: deletedImages,
      },
    });

    await updateOrderMutation.mutateAsync({
      advertisementId,
      imageIds: images.filter((x) => x.imageId).map((x) => x.imageId!),
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
            {...register("price")}
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
