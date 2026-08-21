import type { Avatar } from "./avatar";
import type { City } from "./city";
import type { Role } from "./role";

export type User = {
  id: number;
  surname: string | null;
  name: string;
  phone: string;
  email: string | null;
  avatar: Avatar | null;
  city: City;
  roles: Role[];
  lastSeenAt: string;
  registeredAt: string;
};
