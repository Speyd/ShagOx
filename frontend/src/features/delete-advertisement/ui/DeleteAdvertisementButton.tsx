import Button from "@/shared/ui/Button";
import { useDeleteAdvertisement } from "../hooks/useDeleteAdvertisement";

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
    <div>
      <Button onClick={() => handleDelete()}>Видалити оголошення</Button>
    </div>
  );
}
