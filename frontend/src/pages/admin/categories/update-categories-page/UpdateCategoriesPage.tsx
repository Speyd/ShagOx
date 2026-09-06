import AdminUpdateCategoryForm from "@/features/admin/category/update-category/ui/AdminUpdateCategoryForm";
import { useParams } from "react-router-dom";

export default function UpdateCategoriesPage() {
  const { id } = useParams<{ id: string }>();
  const categoryId = Number(id);

  if (!Number.isInteger(categoryId)) {
    return <div>Invalid category ID</div>;
  }

  return (
    <div>
      <h1>Update Categories</h1>
      <p>This is the update categories page.</p>
      <AdminUpdateCategoryForm categoryId={categoryId} />
    </div>
  );
}
