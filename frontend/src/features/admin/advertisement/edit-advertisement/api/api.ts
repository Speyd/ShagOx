import { api } from "@/shared/api/api";
import type { AttributeInfo } from "@/entities/advertisement/model/types";
import { toProductAttributes, toVariantAttributes } from "../lib/mapping";
import type {
  AdminAdvertisement,
  EditorChanges,
  EditorValues,
} from "../model/types";

export async function getAdminAdvertisement(id: number) {
  const { data } = await api.get<AdminAdvertisement>(`/admin/advertisements/${id}`, {
    params: { status: "All" },
  });
  return data;
}

function buildUpdateForm(
  ad: AdminAdvertisement,
  values: EditorValues,
  changes: EditorChanges,
  definitions: AttributeInfo[],
) {
  const form = new FormData();

  form.append("Title", values.title.trim());
  form.append("Description", values.description.trim());
  if (values.categoryId) form.append("CategoryId", values.categoryId);
  if (values.currencyId) form.append("CurrencyId", values.currencyId);
  if (values.conditionId) form.append("ConditionId", values.conditionId);
  if (values.statusId && values.statusId !== String(ad.status?.id))
    form.append("StatusId", values.statusId);

  form.append(
    "Attributes",
    JSON.stringify(toProductAttributes(values.attributes, definitions)),
  );

  const changedVariants = Object.fromEntries(
    values.variants
      .filter(
        (variant) =>
          variant.id &&
          !changes.deletedVariantIds.includes(variant.id) &&
          (variant.price !== variant.initialPrice ||
            variant.stock !== variant.initialStock),
      )
      .map((variant) => [
        variant.id,
        { Price: Number(variant.price), Stock: Number(variant.stock) },
      ]),
  );

  if (Object.keys(changedVariants).length > 0) {
    form.append("Variants", JSON.stringify(changedVariants));
  }

  const pictures = [
    ...values.images.map((image, order) => ({
      id: image.imageId,
      file: image.file,
      order,
      isDeleted: false,
    })),
    ...changes.deletedImageIds.map((id) => ({
      id,
      file: undefined,
      order: 0,
      isDeleted: true,
    })),
  ];

  pictures.forEach((picture, index) => {
    if (picture.id != null) form.append(`Pictures[${index}].Id`, String(picture.id));
    if (picture.file) form.append(`Pictures[${index}].File`, picture.file);
    form.append(`Pictures[${index}].Order`, String(picture.order));
    form.append(`Pictures[${index}].IsDeleted`, String(picture.isDeleted));
  });

  return form;
}

export async function saveAdvertisement(
  ad: AdminAdvertisement,
  values: EditorValues,
  changes: EditorChanges,
  definitions: AttributeInfo[],
) {
  await api.put(
    `/admin/advertisements/${ad.id}`,
    buildUpdateForm(ad, values, changes, definitions),
  );

  const newVariants = values.variants.filter((variant) => !variant.id);

  for (const variant of newVariants) {
    await api.post("/admin/advertisement-variants", {
      advertisementId: ad.id,
      price: Number(variant.price),
      previousPrice: Number(variant.previousPrice || 0),
      stock: Number(variant.stock),
      attributes: toVariantAttributes(variant.options, definitions),
    });
  }

  for (const id of changes.deletedVariantIds) {
    await api.delete(`/admin/advertisement-variants/${id}`);
  }
}
