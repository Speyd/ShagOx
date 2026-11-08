import {
  keepPreviousData,
  useMutation,
  useQuery,
  useQueryClient,
} from "@tanstack/react-query";
import { toast } from "sonner";
import {
  changeEntityStatus,
  deleteEntity,
  fetchAll,
  fetchPage,
  type EntityStatus,
  type ListParams,
} from "./crud";
import { getErrorMessage } from "./getErrorMessage";

export const ADMIN_KEY = "admin";

export function useAdminList<T>(path: string, params: ListParams) {
  return useQuery({
    queryKey: [ADMIN_KEY, path, "list", params],
    queryFn: () => fetchPage<T>(path, params),
    placeholderData: keepPreviousData,
  });
}

export function useAdminOptions<T>(
  path: string,
  params: Omit<ListParams, "page" | "pageSize"> = { status: "Active" },
  enabled = true,
) {
  return useQuery({
    queryKey: [ADMIN_KEY, path, "all", params],
    queryFn: () => fetchAll<T>(path, params),
    staleTime: 60_000,
    enabled,
  });
}

export function useInvalidateAll() {
  const queryClient = useQueryClient();
  return () => queryClient.invalidateQueries();
}

export function useAdminAction<TArgs>(
  action: (args: TArgs) => Promise<unknown>,
  messages: { success: string; error: string },
) {
  const invalidate = useInvalidateAll();

  return useMutation({
    mutationFn: action,
    onSuccess: () => {
      invalidate();
      toast.success(messages.success);
    },
    onError: (error) => toast.error(getErrorMessage(error, messages.error)),
  });
}

export function useAdminDelete(path: string, entityName = "запис") {
  return useAdminAction((id: number) => deleteEntity(path, id), {
    success: `Видалено ${entityName}`,
    error: `Не вдалося видалити ${entityName}`,
  });
}

export function useAdminVisibility(path: string) {
  return useAdminAction(
    ({ id, hidden }: { id: number; hidden: boolean }) =>
      changeEntityStatus(path, id, (hidden ? "Active" : "Hidden") as EntityStatus),
    { success: "Видимість змінено", error: "Не вдалося змінити видимість" },
  );
}
