import { api } from "@/shared/api/api";
import type { User } from "../model/types";

export async function me(): Promise<User> {
  const response = await api.get("api/users/me");
  return response.data;
}
