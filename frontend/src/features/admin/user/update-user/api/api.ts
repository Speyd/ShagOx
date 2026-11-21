import { api } from "@/shared/api/api";
import type { UpdateUserRequestDto } from "../model/types";

export async function adminUpdateUser(id: number, data: UpdateUserRequestDto) {
  const formData = new FormData();

  formData.append("FirstName", data.firstName);
  formData.append("LastName", data.lastName);
  formData.append("UserName", data.userName);
  formData.append("Bio", data.bio);

  if (data.cityId !== null) formData.append("CityId", String(data.cityId));
  if (data.avatar) formData.append("Avatar", data.avatar);

  const response = await api.put(`/admin/users/${id}`, formData);
  return response.data;
}
