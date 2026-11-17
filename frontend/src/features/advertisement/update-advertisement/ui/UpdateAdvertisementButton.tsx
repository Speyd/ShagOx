import { Pencil } from "lucide-react";
import { useNavigate } from "react-router-dom";
import IconButton from "@/shared/ui/buttons/icon-button/IconButton";

type UpdateButtonProps = {
  id: number;
};

export default function UpdateAdvertisementButton({ id }: UpdateButtonProps) {
  const navigate = useNavigate();

  return (
    <IconButton
      type="button"
      onClick={() => navigate(`/update-advertisement/${id}`)}
      aria-label="Редагувати оголошення"
      title="Редагувати"
    >
      <Pencil size={18} />
    </IconButton>
  );
}
