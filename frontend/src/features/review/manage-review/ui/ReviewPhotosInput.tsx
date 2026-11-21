import { useEffect, useMemo } from "react";
import { FileButton, Text } from "@mantine/core";
import { ImagePlus, X } from "lucide-react";
import { toast } from "sonner";
import type { CommentImage } from "@/entities/review";
import { REVIEW_MAX_PHOTOS } from "../model/schema";
import styles from "./ReviewForm.module.css";

const MAX_FILE_SIZE = 10 * 1024 * 1024;
const ACCEPT = "image/png,image/jpeg,image/webp";

type ReviewPhotosInputProps = {
  existing?: CommentImage[];
  onRemoveExisting?: (id: number) => void;
  value: File[];
  onChange: (files: File[]) => void;
  error?: string;
};

export function ReviewPhotosInput({
  existing = [],
  onRemoveExisting,
  value,
  onChange,
  error,
}: ReviewPhotosInputProps) {
  const previews = useMemo(
    () => value.map((file) => URL.createObjectURL(file)),
    [value],
  );

  useEffect(
    () => () => previews.forEach((url) => URL.revokeObjectURL(url)),
    [previews],
  );

  const total = existing.length + value.length;

  const addFiles = (files: File[]) => {
    const valid = files.filter((file) => {
      if (file.size <= MAX_FILE_SIZE) return true;
      toast.error(`Файл «${file.name}» більший за 10 МБ`);
      return false;
    });

    const free = REVIEW_MAX_PHOTOS - total;
    if (valid.length > free) {
      toast.error(`Можна додати не більше ${REVIEW_MAX_PHOTOS} фото`);
    }
    onChange([...value, ...valid.slice(0, free)]);
  };

  return (
    <div>
      <Text fz={14} fw={600} mb={6}>
        Фото{" "}
        <Text span c="dimmed" fz={13} fw={400}>
          (необов'язково, до {REVIEW_MAX_PHOTOS})
        </Text>
      </Text>

      <div className={styles.photos}>
        {existing.map((picture) => (
          <div key={`existing-${picture.id}`} className={styles.photo}>
            <img src={picture.url} alt="" />
            <button
              type="button"
              className={styles.removePhoto}
              aria-label="Видалити фото"
              onClick={() => onRemoveExisting?.(picture.id)}
            >
              <X size={12} />
            </button>
          </div>
        ))}

        {previews.map((url, index) => (
          <div key={url} className={styles.photo}>
            <img src={url} alt="" />
            <button
              type="button"
              className={styles.removePhoto}
              aria-label="Видалити фото"
              onClick={() => onChange(value.filter((_, i) => i !== index))}
            >
              <X size={12} />
            </button>
          </div>
        ))}

        {total < REVIEW_MAX_PHOTOS && (
          <FileButton onChange={addFiles} accept={ACCEPT} multiple>
            {(props) => (
              <button {...props} type="button" className={styles.addPhoto}>
                <ImagePlus size={20} />
              </button>
            )}
          </FileButton>
        )}
      </div>

      {error && (
        <Text c="red" fz={12} mt={4}>
          {error}
        </Text>
      )}
    </div>
  );
}
