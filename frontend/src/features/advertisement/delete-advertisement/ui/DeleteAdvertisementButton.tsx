import { useDeleteAdvertisement } from "../model/hooks/useDeleteAdvertisement";
import { Trash2 } from "lucide-react";
import IconButton from "@/shared/ui/icon-button/IconButton";
import { useNavigate } from "react-router-dom";

type DeleteAdvertisementButtonProps = {
  id: number;
};

export default function DeleteAdvertisementButton({
  id,
}: DeleteAdvertisementButtonProps) {
  const deleteMutation = useDeleteAdvertisement();
  const navigate = useNavigate();

  const handleDelete = () => {
    deleteMutation.mutate(id);
    navigate("/");
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
