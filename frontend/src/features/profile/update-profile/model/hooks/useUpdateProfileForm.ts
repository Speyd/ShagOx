import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { useAuthStore } from "@/features/auth";
import {
  updateProfileSchema,
  type UpdateProfileDto,
  type UpdateProfileInput,
} from "../schemas/schema";

type ProfileUser = ReturnType<typeof useAuthStore.getState>["user"];

export const getProfileFormValues = (
  user: ProfileUser,
): UpdateProfileInput => ({
  firstName: user?.firstName ?? "",
  lastName: user?.lastName ?? "",
  userName: user?.userName ?? "",
  bio: user?.bio ?? "",
  email: user?.email ?? "",
  avatar: null,
  cityId: user?.city?.id ?? null,
  currencyId: user?.currencyId ?? null,
});

export default function useUpdateProfileForm(user: ProfileUser) {
  return useForm<UpdateProfileInput, unknown, UpdateProfileDto>({
    resolver: zodResolver(updateProfileSchema),
    defaultValues: getProfileFormValues(user),
  });
}
