import { useRef, type ChangeEvent, type DragEvent } from "react";
import { UploadCloud, Image as ImageIcon, X } from "lucide-react";
import { toast } from "sonner";
import type { ImageFileItem } from "../../model/types";
import styles from "../CreateAdvertisementForm.module.css";

type PhotosSectionProps = {
  images: ImageFileItem[];
  setImages: React.Dispatch<React.SetStateAction<ImageFileItem[]>>;
  error?: string;
};

const MAX_PHOTOS = 10;
const MAX_FILE_SIZE_BYTES = 10 * 1024 * 1024; // 10MB

export default function PhotosSection({
  images,
  setImages,
  error,
}: PhotosSectionProps) {
  const fileInputRef = useRef<HTMLInputElement>(null);

  const handleFiles = (files: FileList | File[]) => {
    const fileArray = Array.from(files);
    const validNewImages: ImageFileItem[] = [];

    for (const file of fileArray) {
      if (!file.type.startsWith("image/")) {
        toast.error(`Файл "${file.name}" не є зображенням.`);
        continue;
      }

      if (file.size > MAX_FILE_SIZE_BYTES) {
        toast.error(`Файл "${file.name}" перевищує максимальний розмір 10 МБ.`);
        continue;
      }

      if (images.length + validNewImages.length >= MAX_PHOTOS) {
        toast.error(`Максимальна кількість фотографій: ${MAX_PHOTOS}`);
        break;
      }

      const previewUrl = URL.createObjectURL(file);
      validNewImages.push({
        id: `${file.name}-${Date.now()}-${Math.random()}`,
        file,
        previewUrl,
      });
    }

    if (validNewImages.length > 0) {
      setImages((prev) => [...prev, ...validNewImages]);
    }
  };

  const handleFileInputChange = (e: ChangeEvent<HTMLInputElement>) => {
    if (e.target.files) {
      handleFiles(e.target.files);
    }
    e.target.value = "";
  };

  const handleDrop = (e: DragEvent<HTMLDivElement>) => {
    e.preventDefault();
    e.stopPropagation();
    if (e.dataTransfer.files) {
      handleFiles(e.dataTransfer.files);
    }
  };

  const handleDragOver = (e: DragEvent<HTMLDivElement>) => {
    e.preventDefault();
    e.stopPropagation();
  };

  const handleRemoveImage = (idToRemove: string) => {
    setImages((prev) => {
      const item = prev.find((img) => img.id === idToRemove);
      if (item) {
        URL.revokeObjectURL(item.previewUrl);
      }
      return prev.filter((img) => img.id !== idToRemove);
    });
  };

  // Ensure at least 4 slots for preview display (matching design)
  const displaySlots = Array.from({ length: Math.max(4, images.length) }, (_, i) => images[i] ?? null);

  return (
    <div className={styles.card}>
      <h3 className={styles.cardTitle}>
        Фото товару <span className={styles.requiredStar}>*</span>
      </h3>
      <p className={styles.cardSubtitle}>
        Додайте якісні фото товару. Перше фото буде основним.
      </p>

      {/* Hidden file input */}
      <input
        ref={fileInputRef}
        type="file"
        multiple
        accept="image/png,image/jpeg,image/webp,image/jpg"
        onChange={handleFileInputChange}
        className={styles.hiddenInput}
      />

      {/* Dropzone */}
      <div
        className={`${styles.dropzone} ${error ? styles.dropzoneError : ""}`}
        onDrop={handleDrop}
        onDragOver={handleDragOver}
        onClick={() => fileInputRef.current?.click()}
      >
        <div className={styles.uploadIconCircle}>
          <UploadCloud size={28} className={styles.uploadIcon} />
        </div>
        <p className={styles.uploadMainText}>
          <span className={styles.uploadLink}>Завантажити фото</span> або перетягніть файл сюди
        </p>
        <span className={styles.uploadSubText}>PNG, JPG до 10MB</span>
      </div>

      {error && <p className={styles.errorMessage}>{error}</p>}

      {/* 4 Thumbnails slots below dropzone */}
      <div className={styles.thumbnailGrid}>
        {displaySlots.map((item, index) => {
          if (item) {
            return (
              <div key={item.id} className={styles.thumbnailSlotFilled}>
                <img
                  src={item.previewUrl}
                  alt={`Фото ${index + 1}`}
                  className={styles.thumbnailImage}
                />
                {index === 0 && (
                  <span className={styles.mainBadge}>Головне</span>
                )}
                <button
                  type="button"
                  className={styles.removeImageBtn}
                  onClick={(e) => {
                    e.stopPropagation();
                    handleRemoveImage(item.id);
                  }}
                  title="Видалити фото"
                  aria-label="Видалити фото"
                >
                  <X size={13} />
                </button>
              </div>
            );
          }

          return (
            <div
              key={`empty-${index}`}
              className={styles.thumbnailSlotEmpty}
              onClick={() => fileInputRef.current?.click()}
              title="Додати фото"
            >
              <ImageIcon size={20} className={styles.placeholderIcon} />
            </div>
          );
        })}
      </div>
    </div>
  );
}

