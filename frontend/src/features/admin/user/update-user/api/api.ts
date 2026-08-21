import { api } from "@/shared/api/api";
import type { UpdateUserRequestDto } from "../model/types";

export async function adminUpdateUser(id: number, data: UpdateUserRequestDto) {
  const formData = new FormData();

  formData.append("surname", data.surname);
  formData.append("name", data.name);
  formData.append("phone", data.phone);
  formData.append("email", data.email);

  if (data.cityId !== null) {
    formData.append("cityId", String(data.cityId));
  }

  if (data.avatar) {
    formData.append("avatar", data.avatar);
  }

  const response = await api.put(`/admin/users/${id}`, formData);

  return response.data;
}
