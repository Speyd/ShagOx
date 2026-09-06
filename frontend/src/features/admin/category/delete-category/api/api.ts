import { api } from "@/shared/api/api";

export async function deleteAdminCategory(id: number): Promise<void> {
  await api.delete(`/admin/categories/${id}`);
}
