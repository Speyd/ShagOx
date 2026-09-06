import { useEffect } from "react";
import { useForm } from "react-hook-form";
import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import {
  Button,
  Group,
  Paper,
  Stack,
  Text,
  TextInput,
  Title,
} from "@mantine/core";

import { useGetAdminCategory } from "@/entities/category/model/useGetAdminCategory";
import { useAdminUpdateCategory } from "../model/hooks/useAdminUpdateCategory";

type AdminUpdateCategoryFormProps = {
  categoryId: number;
};

const updateCategorySchema = z.object({
  name: z
    .string()
    .min(1, "Введіть назву категорії")
    .max(100, "Назва не може бути довшою за 100 символів"),

  productTypeId: z.number(),
});

type UpdateCategoryFormValues = z.infer<typeof updateCategorySchema>;

export default function AdminUpdateCategoryForm({
  categoryId,
}: AdminUpdateCategoryFormProps) {
  const {
    data: category,
    isLoading: isCategoryLoading,
    isError: isCategoryError,
  } = useGetAdminCategory(categoryId);

  const updateCategory = useAdminUpdateCategory();

  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm<UpdateCategoryFormValues>({
    resolver: zodResolver(updateCategorySchema),
  });

  useEffect(() => {
    if (!category) return;

    reset({
      name: category.name,
      productTypeId: category.productType.id,
    });
  }, [category, reset]);

  const onSubmit = (data: UpdateCategoryFormValues) => {
    updateCategory.mutate({
      id: categoryId,
      data,
    });
  };

  if (isCategoryLoading) {
    return <Text>Завантаження...</Text>;
  }

  if (isCategoryError || !category) {
    return <Text c="red">Не вдалося завантажити категорію.</Text>;
  }

  return (
    <Paper withBorder radius="md" p="xl" maw={600} mx="auto">
      <form onSubmit={handleSubmit(onSubmit)}>
        <Stack gap="lg">
          <div>
            <Title order={2}>Редагування категорії</Title>

            <Text c="dimmed" size="sm" mt={4}>
              Змініть дані категорії та збережіть зміни.
            </Text>
          </div>

          <TextInput
            label="Назва категорії"
            placeholder="Наприклад: Смартфони"
            {...register("name")}
            error={errors.name?.message}
          />

          <TextInput
            label="Тип товару"
            value={category.productType.name}
            disabled
          />

          <Group justify="flex-end">
            <Button type="submit" loading={updateCategory.isPending}>
              Зберегти зміни
            </Button>
          </Group>
        </Stack>
      </form>
    </Paper>
  );
}
