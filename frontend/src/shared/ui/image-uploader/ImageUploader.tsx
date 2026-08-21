import styles from "./ImageUploader.module.css";
import { DndContext } from "@dnd-kit/core";
import { SortableContext, rectSortingStrategy } from "@dnd-kit/sortable";
import SortableImage from "./SortableImage";
import type { DragEndEvent } from "@dnd-kit/core";
import { arrayMove } from "@dnd-kit/sortable";
import type { ImageItem } from "@/shared/lib/types/image";

type ImageUploaderProps = {
  images: ImageItem[];
  setImages: React.Dispatch<React.SetStateAction<ImageItem[]>>;
  onDelete?: (image: ImageItem) => Promise<void> | void;
};

export default function ImageUploader({
  images,
  setImages,
  onDelete,
}: ImageUploaderProps) {
  function handleFiles(event: React.ChangeEvent<HTMLInputElement>) {
    const files = Array.from(event.target.files ?? []);

    if (!files.length) return;

    const newImages: ImageItem[] = files.map((file) => ({
      id: crypto.randomUUID(),
      url: URL.createObjectURL(file),
      file,
    }));

    setImages((prev) => [...prev, ...newImages]);

    event.target.value = "";
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
      <label className={styles.dropzone}>
        Додати фото
        <input
          type="file"
          multiple
          accept="image/*"
          hidden
          onChange={handleFiles}
        />
      </label>

      <DndContext onDragEnd={handleDragEnd}>
        <SortableContext items={images} strategy={rectSortingStrategy}>
          <div className={styles.images}>
            {images.map((image) => (
              <SortableImage
                key={image.id}
                image={image}
                onDelete={() => handleDelete(image)}
              />
            ))}
          </div>
        </SortableContext>
      </DndContext>
    </div>
  );
}
