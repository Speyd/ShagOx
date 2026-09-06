import { useMutation } from "@tanstack/react-query";
import type { UpdateCategoryRequestDto } from "../types";
import { adminUpdateCategory } from "../../api/api";
import { toast } from "sonner";

export function useAdminUpdateCategory() {
  return useMutation({
    mutationFn: ({
      id,
      data,
    }: {
      id: number;
      data: UpdateCategoryRequestDto;
    }) => adminUpdateCategory(id, data),
    onSuccess: () => toast.success("Ви успішно оновили категорію!"),
    onError: () => {
      toast.error("Не вдалося оновити категорію. Спробуйте ще раз.");
    },
  });
}
