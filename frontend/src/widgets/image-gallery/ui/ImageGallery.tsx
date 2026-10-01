import { useState } from "react";
import { Image } from "@mantine/core";
import styles from "./ImageGallery.module.css";

type ImageGalleryProps = {
  images: string[];
};

export default function ImageGallery({ images }: ImageGalleryProps) {
  const fallbackImage = "/placeholder-image.png";
  const galleryImages = images && images.length > 0 ? images : [fallbackImage];

  const [selectedImage, setSelectedImage] = useState<string>(galleryImages[0]);

  const activeImage = galleryImages.includes(selectedImage)
    ? selectedImage
    : galleryImages[0];

  return (
    <div className={styles.galleryWrapper}>
      <div className={styles.mainImageContainer}>
        <Image
          src={activeImage}
          alt="Main preview"
          className={styles.mainImage}
          radius="lg"
          fit="contain"
        />
      </div>

      {galleryImages.length > 1 && (
        <div className={styles.thumbnailsContainer}>
          {galleryImages.map((image, index) => {
            const isSelected = image === activeImage;

            return (
              <button
                key={`${image}-${index}`}
                type="button"
                className={`${styles.thumbnailButton} ${
                  isSelected ? styles.selected : ""
                }`}
                onClick={() => setSelectedImage(image)}
              >
                <Image
                  src={image}
                  alt={`Thumbnail ${index + 1}`}
                  className={styles.thumbnailImage}
                  radius="md"
                  fit="contain"
                />
              </button>
            );
          })}
        </div>
      )}
    </div>
  );
}
