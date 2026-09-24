import { useEffect, useRef, useState } from "react";
import { Controller, useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import {
  Drawer,
  Avatar,
  Button,
  TextInput,
  Textarea,
  Text,
  Group,
  Stack,
  Box,
  ActionIcon,
  Autocomplete,
  Loader,
} from "@mantine/core";
import styles from "./ProfileDrawer.module.css";
import { AtSign, Camera, Link, X } from "lucide-react";
import { searchCities } from "@/entities/city/api/api";
import type { City } from "@/shared/lib/types/city";
import {
  updateProfileSchema,
  type UpdateProfileDto,
} from "@/features/profile/update-profile/model/schemas/schema";
import useUpdateProfile from "@/features/profile/update-profile/model/hooks/useUpdateProfile";
import { useAuthStore } from "@/features/auth";

type ProfileDrawerProps = {
  opened: boolean;
  close: () => void;
};

export default function ProfileDrawer({ opened, close }: ProfileDrawerProps) {
  const user = useAuthStore((state) => state.user);
  const mutation = useUpdateProfile();

  const [cityQuery, setCityQuery] = useState("");
  const [cityOptions, setCityOptions] = useState<City[]>([]);
  const [cityLoading, setCityLoading] = useState(false);

  const [avatarPreview, setAvatarPreview] = useState<string | null>(null);
  const fileInputRef = useRef<HTMLInputElement>(null);

  const {
    register,
    handleSubmit,
    control,
    reset,
    watch,
    formState: { errors, isSubmitting },
  } = useForm<UpdateProfileDto>({
    resolver: zodResolver(updateProfileSchema),
    defaultValues: {
      firstName: user?.firstName ?? "",
      lastName: user?.lastName ?? "",
      userName: user?.userName ?? "",
      bio: user?.bio ?? "",
      email: user?.email ?? "",
      avatar: null,
      cityId: user?.city?.id ?? null,
    },
  });

  useEffect(() => {
    if (!opened) return;
    reset({
      firstName: user?.firstName ?? "",
      lastName: user?.lastName ?? "",
      userName: user?.userName ?? "",
      bio: user?.bio ?? "",
      email: user?.email ?? "",
      avatar: null,
      cityId: user?.city?.id ?? null,
    });
    setCityQuery(user?.city ? getCityLabel(user.city) : "");
    setCityOptions([]);
    setAvatarPreview(null);
  }, [opened, user, reset]);

  useEffect(() => {
    if (!cityQuery || cityQuery.length < 2) {
      setCityOptions([]);
      return;
    }
    const timeout = setTimeout(async () => {
      setCityLoading(true);
      try {
        const result = await searchCities(cityQuery);
        setCityOptions(result.items);
      } catch {
        // мовчки ігноруємо
      } finally {
        setCityLoading(false);
      }
    }, 350);
    return () => clearTimeout(timeout);
  }, [cityQuery]);

  const cityAutocompleteData = cityOptions.map(getCityLabel);

  const onSubmit = async (data: UpdateProfileDto) => {
    if (!user) return;
    await mutation.mutateAsync({ id: user.id, data });
    close();
  };

  const watchedBio = watch("bio") ?? "";
  const watchedUserName = watch("userName") ?? "";

  return (
    <Drawer
      opened={opened}
      onClose={close}
      position="right"
      size="660px"
      zIndex={1001}
      bdrs={20}
      withCloseButton={false}
      classNames={{
        content: styles.drawerContent,
        body: styles.drawerBody,
      }}
    >
      <form onSubmit={handleSubmit(onSubmit)}>
        <Box className={styles.header}>
          <div>
            <Text fz={24} fw={700} className={styles.title}>
              Редагувати профіль
            </Text>
            <Text fz={12} fw={400} className={styles.subtitle}>
              Оновіть свої особисті дані та контактну інформацію
            </Text>
          </div>

          <button className={styles.closeButton} onClick={close} type="button">
            <X size={35} strokeWidth={2.5} className="icon" />
          </button>
        </Box>

        <Box className={styles.container}>
          <Stack className={styles.avatarSection} align="center">
            <Controller
              name="avatar"
              control={control}
              render={({ field }) => (
                <>
                  <Box className={styles.avatarWrapper}>
                    <Avatar
                      src={avatarPreview ?? user?.avatar?.url ?? undefined}
                      size={180}
                      radius={180}
                      className={styles.avatar}
                    />
                    <ActionIcon
                      className={styles.cameraButton}
                      radius="xl"
                      variant="default"
                      type="button"
                      onClick={() => fileInputRef.current?.click()}
                    >
                      <Camera size={16} />
                    </ActionIcon>
                  </Box>

                  <input
                    ref={fileInputRef}
                    type="file"
                    accept="image/jpeg,image/png,image/webp"
                    hidden
                    onChange={(e) => {
                      const file = e.target.files?.[0];
                      if (!file) return;
                      field.onChange(file);
                      setAvatarPreview(URL.createObjectURL(file));
                      e.target.value = "";
                    }}
                  />
                </>
              )}
            />

            <Button
              variant="outline"
              color="gray"
              radius="md"
              fullWidth
              type="button"
              onClick={() => fileInputRef.current?.click()}
            >
              Змінити фото
            </Button>
            <Text className={styles.fileHint} fw={400} fz={12}>
              JPG, PNG або WEBP. Макс. 5 МБ
            </Text>
          </Stack>

          <Stack className={styles.formSection} gap="md">
            <Group grow>
              <TextInput
                label="Ім'я"
                placeholder="Введіть ім'я"
                radius="md"
                classNames={{ label: styles.label, input: styles.input }}
                error={errors.firstName?.message}
                {...register("firstName")}
              />
              <TextInput
                label="Прізвище"
                placeholder="Введіть прізвище"
                radius="md"
                classNames={{ label: styles.label, input: styles.input }}
                error={errors.lastName?.message}
                {...register("lastName")}
              />
            </Group>

            <Box>
              <TextInput
                label="Ім'я користувача"
                placeholder="Введіть нікнейм"
                leftSection={<AtSign size={16} />}
                radius="md"
                classNames={{ label: styles.label, input: styles.input }}
                error={errors.userName?.message}
                {...register("userName")}
              />
              <Text className={styles.urlHint} fw={400} fz={12}>
                URL вашого профілю: marketly.ua/@{watchedUserName || "…"}
              </Text>
            </Box>

            <Box>
              <Group justify="space-between" mb={3}>
                <Text className={styles.label} fw={700} fz={14}>
                  Про себе
                </Text>
                <Text className={styles.charCounter} fw={400} fz={12}>
                  {watchedBio.length}/160
                </Text>
              </Group>
              <Textarea
                maxLength={160}
                rows={3}
                radius="md"
                classNames={{ input: styles.input }}
                error={errors.bio?.message}
                {...register("bio")}
              />
            </Box>

            <TextInput
              label="Email"
              placeholder="example@gmail.com"
              radius="md"
              classNames={{ label: styles.label, input: styles.input }}
              error={errors.email?.message}
              {...register("email")}
            />

            <Controller
              name="cityId"
              control={control}
              render={({ field, fieldState }) => (
                <Autocomplete
                  label="Місто"
                  value={cityQuery}
                  onChange={(val) => {
                    setCityQuery(val);
                    if (!val) field.onChange(null);
                  }}
                  onOptionSubmit={(val) => {
                    const found = cityOptions.find(
                      (c) => getCityLabel(c) === val,
                    );
                    if (found) {
                      field.onChange(found.id);
                      setCityQuery(getCityLabel(found));
                    }
                  }}
                  data={cityAutocompleteData}
                  radius="md"
                  placeholder="Введіть код міста…"
                  rightSection={cityLoading ? <Loader size={14} /> : null}
                  classNames={{ label: styles.label, input: styles.input }}
                  error={fieldState.error?.message}
                />
              )}
            />

            <TextInput
              label="Посилання"
              disabled
              placeholder="Буде доступно пізніше"
              rightSection={<Link size={18} color="#909399" />}
              radius="md"
              classNames={{ label: styles.label, input: styles.input }}
            />
          </Stack>
        </Box>

        <Group justify="flex-end" className={styles.actions} mt="xl">
          <Button
            variant="outline"
            color="gray"
            radius="md"
            type="button"
            onClick={close}
            disabled={isSubmitting}
          >
            Скасувати
          </Button>
          <Button
            className={styles.saveButton}
            radius="md"
            type="submit"
            loading={isSubmitting}
          >
            Зберегти зміни
          </Button>
        </Group>
      </form>
    </Drawer>
  );
}

function getCityLabel(city: City): string {
  const cityWithCode = city as City & { code?: string };
  return String(city.name ?? cityWithCode.code ?? `Місто ${city.id}`);
}
