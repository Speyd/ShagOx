import { Trash2 } from "lucide-react";
import { modals } from "@mantine/modals";
import { Text } from "@mantine/core";
import IconButton from "@/shared/ui/buttons/icon-button/IconButton";
import { useDeleteAdvertisement } from "../model/hooks/useDeleteAdvertisement";

type DeleteAdvertisementButtonProps = {
  id: number;
  title?: string;
  onDeleted?: () => void;
};

export default function DeleteAdvertisementButton({
  id,
  title,
  onDeleted,
}: DeleteAdvertisementButtonProps) {
  const deleteMutation = useDeleteAdvertisement();

  const handleDelete = () => {
    modals.openConfirmModal({
      title: "Видалити оголошення",
      centered: true,
      children: (
        <Text size="sm">
          {title ? `Видалити «${title}»? ` : "Ви дійсно бажаєте видалити це оголошення? "}
          Цю дію неможливо буде скасувати.
        </Text>
      ),
      labels: {
        confirm: "Видалити",
        cancel: "Скасувати",
      },
      confirmProps: {
        color: "red",
      },
      onConfirm: () => deleteMutation.mutate(id, { onSuccess: onDeleted }),
    });
  };

  return (
    <IconButton
      type="button"
      onClick={handleDelete}
      disabled={deleteMutation.isPending}
      aria-label="Видалити оголошення"
      title="Видалити"
    >
      <Trash2 size={18} />
    </IconButton>
  );
}
