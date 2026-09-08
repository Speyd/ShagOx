import { Trash2 } from "lucide-react";
import IconButton from "@/shared/ui/icon-button/IconButton";
import { useAdminDeleteAdvertisement } from "../model/hooks/useAdminDeleteAdvertisement";

type AdminDeleteAdvertisementButtonProps = {
  id: number;
};

export default function AdminDeleteAdvertisementButton({
  id,
}: AdminDeleteAdvertisementButtonProps) {
  const deleteMutation = useAdminDeleteAdvertisement();

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
