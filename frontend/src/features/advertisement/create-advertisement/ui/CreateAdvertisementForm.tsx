import { useState, useMemo } from "react";
import type { CreateAdvertisementSchema } from "../model/schemas/createAdvertisementSchema";
import { createAdvertisementSchema } from "../model/schemas/createAdvertisementSchema";
import type { ImageFileItem } from "../model/types";
import { useGetCategories } from "@/entities/advertisement/model/hooks/useGetCategories";
import MainInfoSection from "./sections/MainInfoSection";
import CategoryAttributesSection from "./sections/CategoryAttributesSection";
import PhotosSection from "./sections/PhotosSection";
import AdditionalInfoSection from "./sections/AdditionalInfoSection";
import { useForm, useWatch } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import styles from "./CreateAdvertisementForm.module.css";

type CreateAdvertisementFormProps = {
  onSubmit: (data: CreateAdvertisementSchema, images: ImageFileItem[]) => void;
  isLoading?: boolean;
};

export default function CreateAdvertisementForm({
  onSubmit,
}: CreateAdvertisementFormProps) {
  const [images, setImages] = useState<ImageFileItem[]>([]);
  const [imagesError, setImagesError] = useState<string>("");

  const {
    control,
    handleSubmit,
    setValue,
    formState: { errors },
  } = useForm<CreateAdvertisementSchema>({
    resolver: zodResolver(createAdvertisementSchema),
    defaultValues: {
      title: "",
      categoryId: "",
      price: "" as unknown as number,
      stock: "" as unknown as number,
      description: "",
      hashtags: [],
      brand: "",
      conditionId: "",
      tags: "",
      attributes: {},
      variantAttributes: {},
    },
  });

  const titleValue = useWatch({ control, name: "title" }) || "";
  const descriptionValue = useWatch({ control, name: "description" }) || "";
  const hashtagsValue = useWatch({ control, name: "hashtags" }) || [];

  const { data: categories = [], isLoading: isLoadingCategories } =
    useGetCategories();

  const categoryOptions = useMemo(
    () =>
      categories.map((cat) => ({
        value: String(cat.id),
        label: cat.label,
      })),
    [categories],
  );

  const handleFormSubmit = handleSubmit((data) => {
    if (images.length < 1) {
      setImagesError("Додайте хоча б 1 фотографію товару");
      return;
    }
    setImagesError("");
    onSubmit(data, images);
  });

  return (
    <form onSubmit={handleFormSubmit} className={styles.form}>
      <div className={styles.leftColumn}>
        <MainInfoSection
          control={control}
          titleValue={titleValue}
          descriptionValue={descriptionValue}
          hashtagsValue={hashtagsValue}
          categories={categoryOptions}
          isLoadingCategories={isLoadingCategories}
          errors={errors}
          setValue={setValue}
        />

        <CategoryAttributesSection control={control} />
      </div>

      <div className={styles.rightColumn}>
        <PhotosSection
          images={images}
          setImages={setImages}
          error={imagesError}
        />

        <AdditionalInfoSection control={control} />
      </div>
    </form>
  );
}
