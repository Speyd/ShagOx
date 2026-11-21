import { useEffect, useMemo, useState } from "react";
import { FileButton } from "@mantine/core";
import { ImagePlus, X } from "lucide-react";
import Button from "@/shared/ui/buttons/button";
import styles from "./ImageField.module.css";

type Props = {
  id: string;
  /** Existing image URL (string), a new File, or nothing. */
  value: unknown;
  onChange: (value: File | string | null) => void;
  accept?: string;
  disabled?: boolean;
};

/** Image picker for admin forms: preview + choose / replace / clear a new file. */
export default function ImageField({
  id,
  value,
  onChange,
  accept = "image/png,image/jpeg,image/webp",
  disabled,
}: Props) {
  const file = value instanceof File ? value : null;
  const existingUrl = typeof value === "string" && value ? value : null;
  const [initialUrl] = useState(existingUrl);
  const fileUrl = useMemo(
    () => (file ? URL.createObjectURL(file) : null),
    [file],
  );

  useEffect(() => {
    if (!fileUrl) return;
    return () => URL.revokeObjectURL(fileUrl);
  }, [fileUrl]);

  const preview = fileUrl ?? existingUrl;

  return (
    <div className={styles.root} id={id}>
      <div className={styles.preview}>
        {preview ? (
          <img src={preview} alt="" className={styles.image} />
        ) : (
          <ImagePlus size={28} className={styles.placeholder} />
        )}
      </div>

      <div className={styles.actions}>
        <FileButton onChange={onChange} accept={accept} disabled={disabled}>
          {(props) => (
            <Button {...props} type="button" variant="outline" size="sm">
              {preview ? "Замінити" : "Обрати зображення"}
            </Button>
          )}
        </FileButton>

        {file && (
          <Button
            type="button"
            variant="ghost"
            size="sm"
            leftSection={X}
            iconProps={{ size: 14 }}
            onClick={() => onChange(initialUrl)}
            disabled={disabled}
          >
            Скасувати вибір
          </Button>
        )}

        {file && <span className={styles.fileName}>{file.name}</span>}
      </div>
    </div>
  );
}
