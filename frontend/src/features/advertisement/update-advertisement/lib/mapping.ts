import type {
  Advertisement,
  VariantAttribute,
} from "@/shared/lib/types/advertisements";
import type { ImageItem } from "@/shared/lib/types/image";
import type { UpdateAdvertisementFormValues } from "../model/schema";
import type { UpdateAdvertisementRequestDto } from "../model/types";

export function getVariantLabel(attributes: VariantAttribute[]): string {
  const parts = attributes
    .map(({ values }) =>
      values.map((value) => value.label || value.value || value.code).join(", "),
    )
    .filter(Boolean);

  return parts.length > 0 ? parts.join(" · ") : "Основний варіант";
}

export function toFormValues(
  advertisement: Advertisement,
): UpdateAdvertisementFormValues {
  return {
    title: advertisement.title,
    description: advertisement.description,
    variants: advertisement.variants.map((variant) => ({
      id: variant.id,
      label: getVariantLabel(variant.attributes),
      price: variant.price,
      stock: variant.stock,
    })),
  };
}

export function toImageItems(advertisement: Advertisement): ImageItem[] {
  return [...advertisement.pictures]
    .sort((left, right) => left.order - right.order)
    .map((picture) => ({
      id: String(picture.id),
      imageId: picture.id,
      url: picture.url,
    }));
}

function hasPicturesChanged(
  advertisement: Advertisement,
  images: ImageItem[],
  deletedImageIds: number[],
) {
  const original = toImageItems(advertisement);

  return (
    deletedImageIds.length > 0 ||
    images.length !== original.length ||
    images.some(
      (image, index) =>
        image.file != null || image.imageId !== original[index]?.imageId,
    )
  );
}

export function toUpdateRequest(
  advertisement: Advertisement,
  values: UpdateAdvertisementFormValues,
  images: ImageItem[],
  deletedImageIds: number[],
): UpdateAdvertisementRequestDto {
  const originalVariants = new Map(
    advertisement.variants.map((variant) => [variant.id, variant]),
  );

  const changedVariants = values.variants.filter((variant) => {
    const original = originalVariants.get(variant.id);
    return (
      original?.price !== Number(variant.price) ||
      original?.stock !== Number(variant.stock)
    );
  });

  return {
    title: values.title.trim(),
    description: values.description.trim(),
    variants:
      changedVariants.length > 0
        ? Object.fromEntries(
            changedVariants.map((variant) => [
              variant.id,
              { price: Number(variant.price), stock: Number(variant.stock) },
            ]),
          )
        : undefined,
    pictures: hasPicturesChanged(advertisement, images, deletedImageIds)
      ? [
          ...images.map((image, order) => ({
            id: image.imageId,
            file: image.file,
            order,
            isDeleted: false,
          })),
          ...deletedImageIds.map((id) => ({ id, order: 0, isDeleted: true })),
        ]
      : undefined,
  };
}
