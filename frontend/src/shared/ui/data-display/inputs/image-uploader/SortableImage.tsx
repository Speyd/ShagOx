import { useSortable } from "@dnd-kit/sortable";
import { CSS } from "@dnd-kit/utilities";
import styles from "./SortableImage.module.css";
import { X } from "lucide-react";
import clsx from "clsx";
import type { ImageItem } from "@/shared/lib/types/image";

type SortableImageProps = {
  image: ImageItem;
  isMain?: boolean;
  onDelete: () => void;
};

export default function SortableImage({
  image,
  isMain,
  onDelete,
}: SortableImageProps) {
  const { attributes, listeners, setNodeRef, transform, transition, isDragging } =
    useSortable({
      id: image.id,
    });

  const style = {
    transform: CSS.Transform.toString(transform),
    transition,
  };

  return (
    <div
      ref={setNodeRef}
      style={style}
      className={clsx(styles.item, isDragging && styles.dragging)}
    >
      <div {...attributes} {...listeners} className={styles.dragHandle}>
        <img src={image.url} alt="" className={styles.image} />
      </div>

      {isMain && <span className={styles.mainBadge}>Головне</span>}

      <button
        type="button"
        onClick={onDelete}
        className={styles.deleteButton}
        aria-label="Видалити фото"
      >
        <X size={12} />
      </button>
    </div>
  );
}
