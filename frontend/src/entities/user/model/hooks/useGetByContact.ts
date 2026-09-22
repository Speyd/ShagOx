import { useQuery } from "@tanstack/react-query";
import { getByContanct } from "../../api/api";
import type { User } from "@/shared/lib/types/user";

export function useGetByContact(contact: string) {
  return useQuery<User>({
    queryKey: ["user", contact],
    queryFn: () => getByContanct(contact),
  });
}
