import type { ImageItem } from "@/types/advertisements";
import { useSortable } from "@dnd-kit/sortable";
import { CSS } from "@dnd-kit/utilities";
import styles from "./SortableImage.module.css";
import IconButton from "../IconButton";
import { X } from "lucide-react";

type SortableImageProps = {
  image: ImageItem;
  onDelete: () => void;
};

export default function SortableImage({ image, onDelete }: SortableImageProps) {
  const { attributes, listeners, setNodeRef, transform, transition } =
    useSortable({
      id: image.id,
    });

  const style = {
    transform: CSS.Transform.toString(transform),
    transition,
  };

  return (
    <div ref={setNodeRef} style={style} className={styles.item}>
      <div {...attributes} {...listeners} className={styles.dragHandle}>
        <img src={image.url} alt="" className={styles.image} />
      </div>

      <IconButton
        type="button"
        onClick={onDelete}
        className={styles.deleteButton}
      >
        <X />
      </IconButton>
    </div>
  );
}
