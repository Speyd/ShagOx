import { useParams } from "react-router-dom";
import { Paper, Stack } from "@mantine/core";
import AdminUpdateUserForm from "@/features/admin/user/update-user/ui/AdminUpdateUserForm";
import { AdminPageHeader } from "@/widgets/admin/page-header";

export default function UpdateUserPage() {
  const { id } = useParams();

  return (
    <Stack gap="lg">
      <AdminPageHeader
        title="Редагування користувача"
        description={`Користувач #${id}`}
        backTo="/admin/users"
      />
      <Paper withBorder radius="lg" p="xl">
        <AdminUpdateUserForm id={Number(id)} />
      </Paper>
    </Stack>
  );
}
