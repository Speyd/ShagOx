import { useParams } from "react-router-dom";
import { Paper, Stack, Text } from "@mantine/core";
import { AdminPageHeader } from "@/widgets/admin/page-header";
import { AdminUpdateCategoryForm } from "@/features/admin/category/update-category/ui";

export default function UpdateCategoriesPage() {
  const { id } = useParams<{ id: string }>();
  const categoryId = Number(id);

  return (
    <Stack gap="lg">
      <AdminPageHeader
        title="Редагування категорії"
        description={`Категорія #${id}`}
        backTo="/admin/categories"
      />
      <Paper withBorder radius="lg" p="xl">
        {Number.isInteger(categoryId) ? (
          <AdminUpdateCategoryForm categoryId={categoryId} />
        ) : (
          <Text c="red">Некоректний ID категорії</Text>
        )}
      </Paper>
    </Stack>
  );
}
