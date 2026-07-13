import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { useCreateAdvertisement } from "../hooks/useCreateAdvertisement";
import {
  createAdvertisementSchema,
  type CreateAdvertisementDto,
} from "../model/schema";
import styles from "./CreateAdvertisementForm.module.css";
import Input from "@/shared/ui/Input";
import TextArea from "@/shared/ui/TextArea";
import Button from "@/shared/ui/Button";
import ImageUploader from "@/shared/ui/ImageUploader";
import { useState } from "react";
import type { ImageItem } from "@/types/advertisements";

export default function CreateAdvertisementForm() {
  const mutation = useCreateAdvertisement();
  const [images, setImages] = useState<ImageItem[]>([]);
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm({
    resolver: zodResolver(createAdvertisementSchema),
  });

  const onSubmit = async (data: CreateAdvertisementDto) => {
    console.log(data);
    await mutation.mutateAsync({
      ...data,
      previousPrice: data.price,
      currencyId: 0,
      categoryId: 1,
      conditionId: 0,
      sellerId: 8,
      images: images.map((x) => x.file!),
      properties: {
        Brand: "Apple",
      },
    });
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)} className={styles.form}>
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
        <Input type="number" {...register("price")} placeholder="Enter price" />

        {errors.price && <p className="error">{errors.price.message}</p>}
      </div>

      <ImageUploader images={images} setImages={setImages} />

      <Button type="submit" disabled={mutation.isPending}>
        {mutation.isPending ? "Створюємо..." : "Створити"}
      </Button>
    </form>
  );
}
