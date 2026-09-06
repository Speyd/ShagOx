import { useMutation, useQueryClient } from "@tanstack/react-query";
import { toast } from "sonner";
import { deleteAdminCategory } from "../../api/api";

export function useDeleteCategory() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: deleteAdminCategory,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["categories"] });
      toast.success("Категорія успішно видалена!");
    },
    onError: () => {
      toast.error("Не вдалося видалити категорію. Спробуйте ще раз.");
    },
  });
}
