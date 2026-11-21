import { api } from "@/shared/api/api";

export async function adminDeleteUser(id: number): Promise<void> {
  await api.delete(`/admin/users/${id}`);
}
