import { useEffect, useRef, type DragEvent } from "react";
import { Image as ImageIcon, UploadCloud, X } from "lucide-react";
import { useFormContext, useWatch } from "react-hook-form";
import { toast } from "sonner";
import clsx from "clsx";
import { MAX_PHOTOS, type AdvertisementFormValues } from "../../model/schema";
import type { ImageFileItem } from "../../model/types";
import FormCard from "../shared/FormCard";
import styles from "../CreateAdvertisementForm.module.css";

const MAX_FILE_SIZE = 10 * 1024 * 1024;
const ACCEPTED_TYPES = ["image/png", "image/jpeg", "image/webp"];
const MIN_SLOTS = 4;

export default function PhotosSection() {
  const {
    control,
    getValues,
    setValue,
    formState: { errors, isSubmitted },
  } = useFormContext<AdvertisementFormValues>();
  const images = useWatch({ control, name: "images" });
  const inputRef = useRef<HTMLInputElement>(null);

  // Звільняємо object URL прев'ю, коли форма зникає
  useEffect(
    () => () =>
      getValues("images").forEach((img) => URL.revokeObjectURL(img.previewUrl)),
    [getValues],
  );

  const updateImages = (next: ImageFileItem[]) =>
    setValue("images", next, { shouldValidate: isSubmitted });

  const addFiles = (files: FileList | null) => {
    if (!files) return;

    const current = getValues("images");
    const added: ImageFileItem[] = [];

    for (const file of Array.from(files)) {
      if (current.length + added.length >= MAX_PHOTOS) {
        toast.error(`Максимальна кількість фотографій: ${MAX_PHOTOS}`);
        break;
      }
      if (!ACCEPTED_TYPES.includes(file.type)) {
        toast.error(`Файл «${file.name}» має бути PNG, JPG або WEBP`);
        continue;
      }
      if (file.size > MAX_FILE_SIZE) {
        toast.error(`Файл «${file.name}» більший за 10 МБ`);
        continue;
      }
      added.push({
        id: crypto.randomUUID(),
        file,
        previewUrl: URL.createObjectURL(file),
      });
    }

    if (added.length > 0) updateImages([...current, ...added]);
  };

  const removeImage = (image: ImageFileItem) => {
    URL.revokeObjectURL(image.previewUrl);
    updateImages(images.filter((img) => img.id !== image.id));
  };

  const makeMain = (image: ImageFileItem) =>
    updateImages([image, ...images.filter((img) => img.id !== image.id)]);

  const openFileDialog = () => inputRef.current?.click();

  const handleDrop = (event: DragEvent<HTMLDivElement>) => {
    event.preventDefault();
    addFiles(event.dataTransfer.files);
  };

  const emptySlots = Math.max(MIN_SLOTS - images.length, 0);
  const error = errors.images?.message;

  return (
    <FormCard
      title="Фото товару"
      subtitle="Перше фото буде головним. Натисніть на фото, щоб зробити його головним."
      required
    >
      <input
        ref={inputRef}
        type="file"
        multiple
        accept={ACCEPTED_TYPES.join(",")}
        className={styles.hiddenInput}
        onChange={(event) => {
          addFiles(event.target.files);
          event.target.value = "";
        }}
      />

      <div
        role="button"
        tabIndex={0}
        className={clsx(styles.dropzone, error && styles.dropzoneError)}
        onClick={openFileDialog}
        onKeyDown={(event) => event.key === "Enter" && openFileDialog()}
        onDragOver={(event) => event.preventDefault()}
        onDrop={handleDrop}
      >
        <div className={styles.uploadIconCircle}>
          <UploadCloud size={28} />
        </div>
        <p className={styles.uploadMainText}>
          <span className={styles.uploadLink}>Завантажити фото</span> або
          перетягніть файли сюди
        </p>
        <span className={styles.uploadSubText}>
          PNG, JPG, WEBP до 10 МБ · {images.length}/{MAX_PHOTOS}
        </span>
      </div>

      <div className={styles.thumbnailGrid}>
        {images.map((image, index) => (
          <div
            key={image.id}
            className={styles.thumbnail}
            onClick={() => makeMain(image)}
            title={index === 0 ? "Головне фото" : "Зробити головним"}
          >
            <img
              src={image.previewUrl}
              alt={`Фото ${index + 1}`}
              className={styles.thumbnailImage}
            />
            {index === 0 && <span className={styles.mainBadge}>Головне</span>}
            <button
              type="button"
              className={styles.removeImageBtn}
              aria-label="Видалити фото"
              onClick={(event) => {
                event.stopPropagation();
                removeImage(image);
              }}
            >
              <X size={13} />
            </button>
          </div>
        ))}

        {Array.from({ length: emptySlots }, (_, i) => (
          <div
            key={`empty-${i}`}
            className={styles.thumbnailEmpty}
            onClick={openFileDialog}
            title="Додати фото"
          >
            <ImageIcon size={20} />
          </div>
        ))}
      </div>

      {error && <p className={styles.errorMessage}>{error}</p>}
    </FormCard>
  );
}
