import { api } from "@/shared/api/api";
import type { UpdateCategoryRequestDto } from "../model/types";

export async function adminUpdateCategory(
  id: number,
  data: UpdateCategoryRequestDto,
) {
  const response = await api.put(`/admin/categories/${id}`, data);

  return response.data;
}
