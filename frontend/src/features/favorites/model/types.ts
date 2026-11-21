import type { User } from "@/features/auth/model/types";
import type { ShortAdvertisement } from "@/shared/lib/types/advertisements";

export type FavoriteCreateRequest = {
  userId: number;
  advertisementId: number;
};

export type Favorite = {
  id: number;
  user: User;
  advertisement: ShortAdvertisement;
};
