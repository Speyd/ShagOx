import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import styles from "./AdvertisementForm.module.css";
import Input from "@/shared/ui/input";
import TextArea from "@/shared/ui/text-area";
import Button from "@/shared/ui/button";
import ImageUploader from "@/shared/ui/image-uploader";
import { useEffect, useState } from "react";
import type { ImageItem } from "@/shared/lib/types/image";
import {
  advertisementSchema,
  type AdvertisementFormData,
} from "../model/schemas/schema";
import { useGetAdminCategories } from "@/entities/category/model/useGetAdminCategories";

type AdvertisementFormProps = {
  defaultValues?: AdvertisementFormData;
  defaultImages?: ImageItem[];
  onSubmit: (data: AdvertisementFormData, images: ImageItem[]) => void;
  onImageDelete?: (image: ImageItem) => void;
  isLoading?: boolean;
};

export default function AdvertisementForm({
  defaultValues,
  defaultImages,
  onSubmit,
  onImageDelete,
  isLoading,
}: AdvertisementFormProps) {
  const [images, setImages] = useState<ImageItem[]>([]);
  const [imagesError, setImagesError] = useState<string>("");

  const { data: categories } = useGetAdminCategories(1, 10);

  console.log(categories);

  useEffect(() => {
    if (!defaultImages) return;

    setImages(defaultImages);
  }, [defaultImages]);

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<AdvertisementFormData>({
    resolver: zodResolver(advertisementSchema),
    defaultValues,
  });

  function handleImageDelete(image: ImageItem) {
    onImageDelete?.(image);

    if (image.file) {
      URL.revokeObjectURL(image.url);
    }

    setImages((prev) => prev.filter((item) => item.id !== image.id));
  }

  return (
    <form
      onSubmit={handleSubmit((data) => {
        if (images.length < 2) {
          setImagesError("Додайте мінімум 2 фотографії");
          return;
        }

        setImagesError("");

        onSubmit(data, images);
      })}
      className={styles.form}
    >
      <div className={styles.inputWrapper}>
        <label className={styles.label}>Назва</label>

        <Input {...register("title")} />

        {errors.title && <p className="error">{errors.title.message}</p>}
      </div>

      <div className={styles.inputWrapper}>
        <label className={styles.label}>Опис</label>

        <TextArea {...register("description")} />

        {errors.description && (
          <p className="error">{errors.description.message}</p>
        )}
      </div>

      <div className={styles.inputWrapper}>
        <label className={styles.label}>Ціна</label>
        <Input
          type="number"
          {...register("price", { valueAsNumber: true })}
          placeholder="Enter price"
        />
        {errors.price && <p className="error">{errors.price.message}</p>}
      </div>

      <ImageUploader
        images={images}
        setImages={setImages}
        onDelete={onImageDelete ? handleImageDelete : undefined}
        setImagesError={setImagesError}
      />

      {imagesError && <p className="error">{imagesError}</p>}

      <Button type="submit" disabled={isLoading}>
        {isLoading ? "Збереження..." : "Зберегти"}
      </Button>
    </form>
  );
}
