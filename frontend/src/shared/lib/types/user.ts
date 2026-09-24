import type { Avatar } from "./avatar";
import type { City } from "./city";
import type { Role } from "./role";

export type User = {
  id: number;
  firstName: string | null;
  lastName: string | null;
  userName: string;
  bio: string | null;
  phone: string;
  email: string | null;
  avatar: Avatar | null;
  city: City;
  roles: Role[];
  lastSeenAt: string;
  registeredAt: string;
};
