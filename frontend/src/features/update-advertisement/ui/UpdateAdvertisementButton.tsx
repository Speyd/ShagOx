import { Pencil } from "lucide-react";
import { useNavigate } from "react-router-dom";
import IconButton from "@/shared/ui/IconButton/IconButton";

type UpdateAdvertisementButtonProps = {
  id: number;
};

export default function UpdateAdvertisementButton({
  id,
}: UpdateAdvertisementButtonProps) {
  const navigate = useNavigate();
  return (
    <IconButton onClick={() => navigate(`/update-advertisement/${id}`)}>
      <Pencil size={20} />
    </IconButton>
  );
}
