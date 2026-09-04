import { useEffect, useState } from "react";
import { Controller, useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import {
  Avatar,
  Button,
  Group,
  Paper,
  Select,
  SimpleGrid,
  Stack,
  Text,
  TextInput,
  Title,
  Divider,
  Box,
} from "@mantine/core";
import { useGetUser } from "@/entities/user/model/hooks/useGetUser";
import { useGetAdminCities } from "@/entities/city/model/hooks/useGetAdminCities";
import {
  adminUpdateUserSchema,
  type UpdateUserDto,
} from "../model/schemas/schema";
import useAdminUpdateUser from "../model/hooks/useAdminUpdateUser";
import AvatarCropModal from "./AvatarCropModal";

type AdminUpdateUserFormProps = {
  id: number;
};

export default function AdminUpdateUserForm({ id }: AdminUpdateUserFormProps) {
  const { data: user, isLoading } = useGetUser(id);

  const [cropModalOpened, setCropModalOpened] = useState(false);
  const [selectedImage, setSelectedImage] = useState<string | null>(null);
  const [avatarPreview, setAvatarPreview] = useState<string | null>(null);

  const { data: cities } = useGetAdminCities(1, 1000);

  const mutation = useAdminUpdateUser();

  const {
    register,
    handleSubmit,
    reset,
    control,
    formState: { errors, isSubmitting },
  } = useForm<UpdateUserDto>({
    resolver: zodResolver(adminUpdateUserSchema),

    defaultValues: {
      surname: "",
      name: "",
      phone: "",
      email: "",
      avatar: null,
      cityId: null,
    },
  });

  useEffect(() => {
    if (!user) return;

    setAvatarPreview(user.avatar?.url ?? null);

    reset({
      surname: user.surname ?? "",
      name: user.name ?? "",
      phone: user.phone ?? "",
      email: user.email ?? "",
      avatar: null,
      cityId: user.city?.id ?? null,
    });
  }, [user, reset]);

  const onSubmit = async (data: UpdateUserDto) => {
    await mutation.mutateAsync({
      id,
      data: data,
    });
  };

  if (isLoading) {
    return <Text>Loading...</Text>;
  }

  if (!user) {
    return <Text>User not found</Text>;
  }

  const cityOptions =
    cities?.items.map((city) => ({
      value: String(city.id),
      label: city.name,
    })) ?? [];

  return (
    <Paper withBorder radius="lg" p="xl">
      <form onSubmit={handleSubmit(onSubmit)}>
        <Stack gap="xl" maw={900}>
          <div>
            <Title order={3}>Редагування користувача</Title>

            <Text c="dimmed" size="sm" mt={4}>
              Змініть інформацію про користувача
            </Text>
          </div>

          <SimpleGrid
            cols={{
              base: 1,
              sm: 2,
            }}
            spacing="md"
          >
            <TextInput
              label="Ім'я"
              placeholder="Введіть ім'я"
              {...register("name")}
              error={errors.name?.message}
            />

            <TextInput
              label="Прізвище"
              placeholder="Введіть прізвище"
              {...register("surname")}
              error={errors.surname?.message}
            />

            <TextInput
              label="Email"
              placeholder="example@gmail.com"
              {...register("email")}
              error={errors.email?.message}
            />

            <TextInput
              label="Телефон"
              placeholder="+380..."
              {...register("phone")}
              error={errors.phone?.message}
            />
          </SimpleGrid>

          <Controller
            name="avatar"
            control={control}
            render={({ field, fieldState }) => (
              <Stack gap="sm">
                <Group align="center">
                  <Avatar src={avatarPreview} size={96} radius="xl" />

                  <Stack gap={4}>
                    <Text fw={500}>Аватар користувача</Text>

                    <Text size="xs" c="dimmed">
                      JPG, PNG або WEBP
                    </Text>

                    <Button
                      size="xs"
                      variant="light"
                      onClick={() => {
                        document.getElementById("avatar-input")?.click();
                      }}
                    >
                      Змінити аватар
                    </Button>
                  </Stack>
                </Group>

                <input
                  id="avatar-input"
                  type="file"
                  accept="image/png,image/jpeg,image/webp"
                  hidden
                  onChange={(event) => {
                    const file = event.target.files?.[0];

                    if (!file) return;

                    const imageUrl = URL.createObjectURL(file);

                    setSelectedImage(imageUrl);
                    setCropModalOpened(true);

                    event.target.value = "";
                  }}
                />

                {fieldState.error?.message && (
                  <Text size="xs" c="red">
                    {fieldState.error.message}
                  </Text>
                )}

                <AvatarCropModal
                  opened={cropModalOpened}
                  image={selectedImage}
                  onClose={() => {
                    setCropModalOpened(false);
                    setSelectedImage(null);
                  }}
                  onCropComplete={(croppedFile) => {
                    field.onChange(croppedFile);

                    const previewUrl = URL.createObjectURL(croppedFile);
                    setAvatarPreview(previewUrl);

                    setSelectedImage(null);
                  }}
                />
              </Stack>
            )}
          />

          <Box maw={450}>
            <Controller
              name="cityId"
              control={control}
              render={({ field, fieldState }) => (
                <Select
                  label="Місто"
                  placeholder="Оберіть місто"
                  searchable
                  clearable
                  nothingFoundMessage="Місто не знайдено"
                  data={cityOptions}
                  value={field.value !== null ? String(field.value) : null}
                  onChange={(value) => {
                    field.onChange(value ? Number(value) : null);
                  }}
                  error={fieldState.error?.message}
                />
              )}
            />
          </Box>

          <Divider />

          <Group justify="flex-end">
            <Button type="submit" loading={isSubmitting}>
              Зберегти зміни
            </Button>
          </Group>
        </Stack>
      </form>
    </Paper>
  );
}
