import { useCallback, useState } from "react";
import { Button, Group, Modal, Slider, Stack } from "@mantine/core";
import Cropper, { type Area } from "react-easy-crop";

type AvatarCropModalProps = {
  opened: boolean;
  image: string | null;
  onClose: () => void;
  onCropComplete: (file: File) => void;
};

export default function AvatarCropModal({
  opened,
  image,
  onClose,
  onCropComplete,
}: AvatarCropModalProps) {
  const [crop, setCrop] = useState({ x: 0, y: 0 });
  const [zoom, setZoom] = useState(1);
  const [croppedAreaPixels, setCroppedAreaPixels] = useState<Area | null>(null);

  const handleCropComplete = useCallback(
    (_croppedArea: Area, croppedAreaPixels: Area) => {
      setCroppedAreaPixels(croppedAreaPixels);
    },
    [],
  );

  const createCroppedImage = async () => {
    if (!image || !croppedAreaPixels) return;

    const file = await getCroppedFile(image, croppedAreaPixels);

    onCropComplete(file);
    onClose();
  };

  return (
    <Modal
      opened={opened}
      onClose={onClose}
      title="Редагування аватара"
      centered
      size="md"
    >
      <Stack>
        <div
          style={{
            position: "relative",
            width: "100%",
            height: 400,
            background: "#111",
          }}
        >
          {image && (
            <Cropper
              image={image}
              crop={crop}
              zoom={zoom}
              aspect={1}
              cropShape="round"
              showGrid={false}
              onCropChange={setCrop}
              onZoomChange={setZoom}
              onCropComplete={handleCropComplete}
            />
          )}
        </div>

        <Slider
          label="Масштаб"
          min={1}
          max={3}
          step={0.1}
          value={zoom}
          onChange={setZoom}
        />

        <Group justify="flex-end">
          <Button variant="default" onClick={onClose}>
            Скасувати
          </Button>

          <Button onClick={createCroppedImage}>Застосувати</Button>
        </Group>
      </Stack>
    </Modal>
  );
}

async function getCroppedFile(imageSrc: string, crop: Area): Promise<File> {
  const image = await createImage(imageSrc);

  const canvas = document.createElement("canvas");
  const ctx = canvas.getContext("2d");

  if (!ctx) {
    throw new Error("Canvas context is not available");
  }

  canvas.width = crop.width;
  canvas.height = crop.height;

  ctx.drawImage(
    image,
    crop.x,
    crop.y,
    crop.width,
    crop.height,
    0,
    0,
    crop.width,
    crop.height,
  );

  return new Promise((resolve, reject) => {
    canvas.toBlob(
      (blob) => {
        if (!blob) {
          reject(new Error("Failed to create image"));
          return;
        }

        resolve(
          new File([blob], "avatar.jpg", {
            type: "image/jpeg",
          }),
        );
      },
      "image/jpeg",
      0.9,
    );
  });
}

function createImage(src: string): Promise<HTMLImageElement> {
  return new Promise((resolve, reject) => {
    const image = new Image();

    image.onload = () => resolve(image);
    image.onerror = reject;

    image.src = src;
  });
}
