import { useEffect, type Dispatch, type SetStateAction } from "react";
import { useFormContext, useWatch } from "react-hook-form";
import { toast } from "sonner";
import ImageUploader from "@/shared/ui/data-display/inputs/image-uploader";
import type { ImageItem } from "@/shared/lib/types/image";
import type { AdvertisementFormValues } from "../../model/schema";
import FormCard from "../shared/FormCard";
import styles from "../CreateAdvertisementForm.module.css";

export default function PhotosSection() {
  const {
    control,
    getValues,
    setValue,
    formState: { errors, isSubmitted },
  } = useFormContext<AdvertisementFormValues>();
  const images = useWatch({ control, name: "images" });

  // Звільняємо object URL прев'ю, коли форма зникає
  useEffect(
    () => () =>
      getValues("images").forEach((img) => URL.revokeObjectURL(img.previewUrl)),
    [getValues],
  );

  // Форма зберігає { file, previewUrl }, а ImageUploader працює з { file, url }
  const uploaderImages: ImageItem[] = images.map((img) => ({
    id: img.id,
    url: img.previewUrl,
    file: img.file,
  }));

  const setUploaderImages: Dispatch<SetStateAction<ImageItem[]>> = (action) => {
    const next =
      typeof action === "function" ? action(uploaderImages) : action;

    setValue(
      "images",
      next.map((img) => ({ id: img.id, file: img.file!, previewUrl: img.url })),
      { shouldValidate: isSubmitted },
    );
  };

  const error = errors.images?.message;

  return (
    <FormCard
      title="Фото товару"
      subtitle="Додайте до 10 фото товару. Перше фото буде головним."
      required
    >
      <ImageUploader
        images={uploaderImages}
        setImages={setUploaderImages}
        setImagesError={(message) => message && toast.error(message)}
        hasError={Boolean(error)}
      />

      {error && <p className={styles.errorMessage}>{error}</p>}
    </FormCard>
  );
}
