import { Pencil } from "lucide-react";
import IconButton from "@/shared/ui/icon-button/IconButton";
import { useNavigate } from "react-router-dom";

type UpdateButtonProps = {
  id: number;
};

export default function UpdateAdvertisementButton({ id }: UpdateButtonProps) {
  const navigate = useNavigate();

  const handleUpdate = () => {
    navigate(`/update-advertisement/${id}`);
  };

  return (
    <IconButton onClick={() => handleUpdate()}>
      <Pencil size={20} />
    </IconButton>
  );
}
