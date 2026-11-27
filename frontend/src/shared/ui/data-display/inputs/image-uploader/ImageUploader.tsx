import styles from "./ImageUploader.module.css";
import {
  DndContext,
  PointerSensor,
  useSensor,
  useSensors,
} from "@dnd-kit/core";
import { SortableContext, rectSortingStrategy } from "@dnd-kit/sortable";
import SortableImage from "./SortableImage";
import type { DragEndEvent } from "@dnd-kit/core";
import { arrayMove } from "@dnd-kit/sortable";
import { UploadCloud } from "lucide-react";
import clsx from "clsx";
import type { ImageItem } from "@/shared/lib/types/image";

const MAX_IMAGES = 10;
const MAX_FILE_SIZE = 10 * 1024 * 1024;

type ImageUploaderProps = {
  images: ImageItem[];
  setImages: React.Dispatch<React.SetStateAction<ImageItem[]>>;
  setImagesError: (error: string) => void;
  onDelete?: (image: ImageItem) => Promise<void> | void;
  hasError?: boolean;
};

export default function ImageUploader({
  images,
  setImages,
  onDelete,
  setImagesError,
  hasError,
}: ImageUploaderProps) {
  const sensors = useSensors(
    useSensor(PointerSensor, { activationConstraint: { distance: 5 } }),
  );

  function addFiles(fileList: FileList | null) {
    const files = Array.from(fileList ?? []).filter((file) =>
      file.type.startsWith("image/"),
    );

    if (!files.length) return;

    const validFiles = files.filter((file) => file.size <= MAX_FILE_SIZE);
    const availableSlots = MAX_IMAGES - images.length;

    if (availableSlots <= 0) {
      setImagesError(`Можна додати максимум ${MAX_IMAGES} фотографій`);
      return;
    }

    const filesToAdd = validFiles.slice(0, availableSlots);

    if (validFiles.length < files.length) {
      setImagesError("Деякі файли більші за 10 МБ і не були додані");
    } else if (validFiles.length > availableSlots) {
      setImagesError(`Можна додати ще тільки ${availableSlots} фото`);
    } else {
      setImagesError("");
    }

    const newImages: ImageItem[] = filesToAdd.map((file) => ({
      id: crypto.randomUUID(),
      url: URL.createObjectURL(file),
      file,
    }));

    setImages((prev) => [...prev, ...newImages]);
  }

  function handleFiles(event: React.ChangeEvent<HTMLInputElement>) {
    addFiles(event.target.files);
    event.target.value = "";
  }

  function handleDrop(event: React.DragEvent<HTMLLabelElement>) {
    event.preventDefault();
    addFiles(event.dataTransfer.files);
  }

  async function handleDelete(image: ImageItem) {
    if (onDelete) {
      await onDelete(image);
      return;
    }

    setImages((prev) => {
      if (image.file) {
        URL.revokeObjectURL(image.url);
      }

      return prev.filter((img) => img.id !== image.id);
    });
  }

  function handleDragEnd(event: DragEndEvent) {
    const { active, over } = event;

    if (!over) return;

    if (active.id === over.id) return;

    setImages((prev) => {
      const oldIndex = prev.findIndex((image) => image.id === active.id);
      const newIndex = prev.findIndex((image) => image.id === over.id);

      return arrayMove(prev, oldIndex, newIndex);
    });
  }

  return (
    <div className={styles.imageUploader}>
      <label
        className={clsx(styles.dropzone, hasError && styles.dropzoneError)}
        onDragOver={(event) => event.preventDefault()}
        onDrop={handleDrop}
      >
        <span className={styles.iconCircle}>
          <UploadCloud size={22} />
        </span>
        <span className={styles.mainText}>
          <span className={styles.link}>Завантажити фото</span> або
          перетягніть сюди
        </span>
        <span className={styles.subText}>
          До 10 МБ · {images.length}/{MAX_IMAGES}
        </span>
        <input
          type="file"
          multiple
          accept="image/*"
          hidden
          onChange={handleFiles}
        />
      </label>

      {images.length > 0 && (
        <DndContext sensors={sensors} onDragEnd={handleDragEnd}>
          <SortableContext items={images} strategy={rectSortingStrategy}>
            <div className={styles.images}>
              {images.map((image, index) => (
                <SortableImage
                  key={image.id}
                  image={image}
                  isMain={index === 0}
                  onDelete={() => handleDelete(image)}
                />
              ))}
            </div>
          </SortableContext>
        </DndContext>
      )}

      {images.length > 1 && (
        <p className={styles.hint}>
          Перетягуйте фото, щоб змінити порядок. Перше фото — головне.
        </p>
      )}
    </div>
  );
}
