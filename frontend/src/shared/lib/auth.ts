import type { User } from "@/features/auth/model/types";

export function hasRole(user: User | null, role: string) {
  return user?.roles.some((r) => r.name === role) ?? false;
}
