import { useMemo, useRef, useState } from "react";
import { Controller, useForm, useWatch } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { Avatar, Select, Text } from "@mantine/core";
import { Camera } from "lucide-react";
import type { User } from "@/shared/lib/types/user";
import type { AdminCity } from "@/shared/lib/types/admin";
import { useAdminOptions } from "@/shared/api/crudHooks";
import Button from "@/shared/ui/buttons/button";
import { FormField, TextAreaField, TextField } from "@/shared/ui/form";
import { adminUpdateUserSchema, type UpdateUserDto } from "../model/schemas/schema";
import useAdminUpdateUser from "../model/hooks/useAdminUpdateUser";
import AvatarCropModal from "./AvatarCropModal";
import styles from "./AdminUpdateUserForm.module.css";

type AdminUpdateUserFormProps = {
  user: User;
};

export default function AdminUpdateUserForm({ user }: AdminUpdateUserFormProps) {
  const fileInputRef = useRef<HTMLInputElement>(null);
  const [cropImage, setCropImage] = useState<string | null>(null);
  const [avatarPreview, setAvatarPreview] = useState<string | null>(
    user.avatar?.url ?? null,
  );

  const { data: cities = [] } = useAdminOptions<AdminCity>("/admin/cities");
  const mutation = useAdminUpdateUser();

  const cityOptions = useMemo(
    () =>
      cities.map((city) => ({
        value: String(city.id),
        label: city.label || city.code,
      })),
    [cities],
  );

  const {
    register,
    handleSubmit,
    control,
    reset,
    formState: { errors, isDirty },
  } = useForm<UpdateUserDto>({
    resolver: zodResolver(adminUpdateUserSchema),
    defaultValues: {
      firstName: user.firstName ?? "",
      lastName: user.lastName ?? "",
      userName: user.userName ?? "",
      bio: user.bio ?? "",
      avatar: null,
      cityId: user.city?.id ? String(user.city.id) : null,
    },
  });

  const bio = useWatch({ control, name: "bio" });

  const onSubmit = handleSubmit(async (data) => {
    await mutation.mutateAsync({
      id: user.id,
      data: { ...data, cityId: data.cityId ? Number(data.cityId) : null },
    });
    reset({ ...data, avatar: null });
  });

  return (
    <form onSubmit={onSubmit} className={styles.form} noValidate>
      <Controller
        name="avatar"
        control={control}
        render={({ field }) => (
          <section className={styles.avatarRow}>
            <button
              type="button"
              className={styles.avatarButton}
              onClick={() => fileInputRef.current?.click()}
              aria-label="Змінити аватар"
            >
              <Avatar src={avatarPreview} size={96} radius={48} color="blue">
                {user.userName?.[0]?.toUpperCase()}
              </Avatar>
              <span className={styles.avatarOverlay}>
                <Camera size={20} />
              </span>
            </button>

            <div>
              <Text fw={700} c="var(--color-base)">
                Аватар
              </Text>
              <Text size="sm" c="dimmed">
                JPG, PNG або WEBP. Зображення буде обрізано до кола.
              </Text>
              <Button
                size="sm"
                variant="light"
                className={styles.avatarAction}
                onClick={() => fileInputRef.current?.click()}
              >
                Завантажити нове фото
              </Button>
            </div>

            <input
              ref={fileInputRef}
              type="file"
              accept="image/png,image/jpeg,image/webp"
              hidden
              onChange={(event) => {
                const file = event.target.files?.[0];
                if (file) setCropImage(URL.createObjectURL(file));
                event.target.value = "";
              }}
            />

            <AvatarCropModal
              opened={Boolean(cropImage)}
              image={cropImage}
              onClose={() => setCropImage(null)}
              onCropComplete={(file) => {
                field.onChange(file);
                setAvatarPreview(URL.createObjectURL(file));
              }}
            />
          </section>
        )}
      />

      <div className={styles.grid}>
        <FormField label="Ім'я" htmlFor="firstName" error={errors.firstName?.message}>
          <TextField id="firstName" {...register("firstName")} invalid={Boolean(errors.firstName)} />
        </FormField>

        <FormField label="Прізвище" htmlFor="lastName" error={errors.lastName?.message}>
          <TextField id="lastName" {...register("lastName")} invalid={Boolean(errors.lastName)} />
        </FormField>

        <FormField
          label="Нікнейм"
          htmlFor="userName"
          required
          error={errors.userName?.message}
        >
          <TextField id="userName" {...register("userName")} invalid={Boolean(errors.userName)} />
        </FormField>

        <Controller
          name="cityId"
          control={control}
          render={({ field }) => (
            <FormField label="Місто">
              <Select
                data={cityOptions}
                value={field.value}
                onChange={field.onChange}
                placeholder="Оберіть місто"
                searchable
                clearable
                nothingFoundMessage="Місто не знайдено"
                radius="md"
              />
            </FormField>
          )}
        />

        <FormField label="Email" hint="Змінюється лише користувачем через підтвердження">
          <TextField value={user.email ?? "—"} disabled readOnly />
        </FormField>

        <FormField label="Телефон" hint="Змінюється лише користувачем через підтвердження">
          <TextField value={user.phone ?? "—"} disabled readOnly />
        </FormField>

        <FormField
          label="Про себе"
          htmlFor="bio"
          className={styles.full}
          hint={`${bio?.length ?? 0}/500`}
          error={errors.bio?.message}
        >
          <TextAreaField id="bio" rows={4} maxLength={500} {...register("bio")} />
        </FormField>
      </div>

      <footer className={styles.footer}>
        <Button
          variant="ghost"
          disabled={!isDirty || mutation.isPending}
          onClick={() => {
            reset();
            setAvatarPreview(user.avatar?.url ?? null);
          }}
        >
          Скинути
        </Button>
        <Button type="submit" loading={mutation.isPending} disabled={!isDirty}>
          Зберегти зміни
        </Button>
      </footer>
    </form>
  );
}
