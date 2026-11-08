import { useState } from "react";
import { Image, Modal } from "@mantine/core";
import type { CommentImage } from "../model/types";
import styles from "./Review.module.css";

/** Мініатюри фото з переглядом у модальному вікні. */
export function CommentPhotos({ pictures }: { pictures: CommentImage[] }) {
  const [active, setActive] = useState<string | null>(null);

  if (pictures.length === 0) return null;

  return (
    <>
      <div className={styles.photos}>
        {pictures.map((picture, index) => (
          <button
            key={picture.id}
            type="button"
            className={styles.photoButton}
            onClick={() => setActive(picture.url)}
            aria-label={`Переглянути фото ${index + 1}`}
          >
            <img src={picture.url} alt="" className={styles.photo} />
          </button>
        ))}
      </div>

      <Modal
        opened={active !== null}
        onClose={() => setActive(null)}
        size="auto"
        centered
        withCloseButton={false}
        padding={0}
      >
        {active && <Image src={active} alt="" mah="85vh" fit="contain" />}
      </Modal>
    </>
  );
}
