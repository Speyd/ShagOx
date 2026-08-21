import AdminUpdateUserForm from "@/features/admin/user/update-user/ui/AdminUpdateUserForm";
import { useParams } from "react-router-dom";

export default function UpdateUserPage() {
  const { id } = useParams();
  return (
    <div>
      UpdateUserPage <AdminUpdateUserForm id={Number(id)} />
    </div>
  );
}
