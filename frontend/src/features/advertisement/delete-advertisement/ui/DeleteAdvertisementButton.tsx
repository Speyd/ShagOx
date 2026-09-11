import { useDeleteAdvertisement } from "../model/hooks/useDeleteAdvertisement";
import { Trash2 } from "lucide-react";
import IconButton from "@/shared/ui/icon-button/IconButton";
import { useNavigate } from "react-router-dom";

type DeleteAdvertisementButtonProps = {
  id: number;
};

import { modals } from "@mantine/modals";
import { Text } from "@mantine/core";

export default function DeleteAdvertisementButton({
  id,
}: DeleteAdvertisementButtonProps) {
  const deleteMutation = useDeleteAdvertisement();
  const navigate = useNavigate();

  const handleDelete = () => {
    modals.openConfirmModal({
      title: "Видалити оголошення",
      centered: true,
      children: (
        <Text size="sm">
          Ви дійсно бажаєте видалити це оголошення? Цю дію неможливо буде скасувати.
        </Text>
      ),
      labels: {
        confirm: "Видалити",
        cancel: "Скасувати",
      },
      confirmProps: {
        color: "red",
      },
      onConfirm: async () => {
        try {
          await deleteMutation.mutateAsync(id);
          navigate("/");
        } catch (error) {
          void error;
        }
      },
    });
  };

  return (
    <IconButton
      onClick={handleDelete}
      disabled={deleteMutation.isPending}
    >
      <Trash2 size={20} />
    </IconButton>
  );
}
