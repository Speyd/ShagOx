import { useDeleteAdvertisement } from "../hooks/useDeleteAdvertisement";
import { Trash2 } from "lucide-react";
import IconButton from "@/shared/ui/IconButton/IconButton";

type DeleteAdvertisementButtonProps = {
  id: number;
};

export default function DeleteAdvertisementButton({
  id,
}: DeleteAdvertisementButtonProps) {
  const deleteMutation = useDeleteAdvertisement();

  const handleDelete = () => {
    deleteMutation.mutate(id);
  };

  return (
    <IconButton
      onClick={() => handleDelete()}
      disabled={deleteMutation.isPending}
    >
      <Trash2 size={20} />
    </IconButton>
  );
}
