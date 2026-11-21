import { useGetUser } from "./useGetUser";
import { getUserDisplayName } from "../../lib/displayName";

/** Ім'я користувача для показу (наприклад, продавця в кошику). */
export function useUserDisplayName(userId: number | null | undefined) {
  const { data } = useGetUser(userId ?? 0, userId != null);
  return getUserDisplayName(data);
}
