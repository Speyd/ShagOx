import { useEffect, useRef, useState } from "react";
import { Controller } from "react-hook-form";
import {
  Avatar,
  TextInput,
  Textarea,
  Text,
  ActionIcon,
  Autocomplete,
  Select,
  Loader,
} from "@mantine/core";
import styles from "./ProfileDrawer.module.css";
import { AtSign, Camera, X } from "lucide-react";
import type { UpdateProfileDto } from "@/features/profile/update-profile/model/schemas/schema";
import useUpdateProfile from "@/features/profile/update-profile/model/hooks/useUpdateProfile";
import useUpdateProfileForm from "@/features/profile/update-profile/model/hooks/useUpdateProfileForm";
import { useAuthStore } from "@/features/auth";
import Button from "@/shared/ui/buttons/button";
import { useGetCurrencies } from "@/entities/currency/model/hooks/useGetCurrencies";
import { getCityLabel } from "@/entities/city/lib/getCityLabel";
import { useSearchCities } from "@/entities/city/model/hooks/useSearchCities";

type ProfileFormProps = {
  close: () => void;
};

const BIO_MAX_LENGTH = 160;

export default function ProfileForm({ close }: ProfileFormProps) {
  const user = useAuthStore((state) => state.user);
  const mutation = useUpdateProfile();

  const { data: currencies, isLoading: currenciesLoading } = useGetCurrencies();

  const currencyOptions = currencies?.map((c) => ({
    value: String(c.id),
    label: `${c.symbol} ${c.code} — ${c.name}`,
  }));

  const [cityQuery, setCityQuery] = useState(() =>
    user?.city ? getCityLabel(user.city) : "",
  );
  const { data: cityOptions = [], isFetching: cityLoading } =
    useSearchCities(cityQuery);
  const cityAutocompleteData = cityOptions.map(getCityLabel);

  const [avatarPreview, setAvatarPreview] = useState<string | null>(null);
  const fileInputRef = useRef<HTMLInputElement>(null);

  const {
    register,
    handleSubmit,
    control,
    watch,
    formState: { errors, isSubmitting },
  } = useUpdateProfileForm(user);

  useEffect(() => {
    return () => {
      if (avatarPreview) URL.revokeObjectURL(avatarPreview);
    };
  }, [avatarPreview]);

  const onSubmit = async (data: UpdateProfileDto) => {
    if (!user) return;
    await mutation.mutateAsync({ id: user.id, data });
    close();
  };

  const openFilePicker = () => fileInputRef.current?.click();

  const isBusy = isSubmitting || mutation.isPending;
  const watchedBio = watch("bio") ?? "";
  const watchedUserName = watch("userName") ?? "";
  const displayName =
    user?.firstName && user?.lastName
      ? `${user.firstName} ${user.lastName}`
      : user?.firstName || user?.userName || "Користувач";

  return (
    <form className={styles.form} onSubmit={handleSubmit(onSubmit)}>
      <div className={styles.header}>
        <div className={styles.headerText}>
          <Text className={styles.title}>Редагувати профіль</Text>
          <Text className={styles.subtitle}>
            Оновіть свої особисті дані та контактну інформацію
          </Text>
        </div>

        <button type="button" className={styles.closeButton} onClick={close}>
          <X size={35} strokeWidth={2.5} className="icon" />
        </button>
      </div>

      <div className={styles.container}>
        <div className={styles.avatarSection}>
          <Controller
            name="avatar"
            control={control}
            render={({ field }) => (
              <>
                <div className={styles.avatarWrapper}>
                  <Avatar
                    src={avatarPreview ?? user?.avatar?.url ?? null}
                    name={displayName}
                    color="initials"
                    size={180}
                    radius="50%"
                    classNames={{
                      root: styles.avatar,
                      placeholder: styles.avatarPlaceholder,
                    }}
                  />
                  <ActionIcon
                    className={styles.cameraButton}
                    radius="xl"
                    variant="default"
                    type="button"
                    aria-label="Змінити фото"
                    onClick={openFilePicker}
                  >
                    <Camera size={16} />
                  </ActionIcon>
                </div>

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

          <div className={styles.avatarActions}>
            <Button variant="outline" type="button" onClick={openFilePicker}>
              Змінити фото
            </Button>
            <Text className={styles.fileHint}>
              JPG, PNG або WEBP. Макс. 5 МБ
            </Text>
            {errors.avatar?.message && (
              <Text className={styles.fileError}>
                {String(errors.avatar.message)}
              </Text>
            )}
          </div>
        </div>

        <div className={styles.formSection}>
          <div className={styles.nameRow}>
            <TextInput
              label="Ім'я"
              placeholder="Введіть ім'я"
              radius="md"
              autoComplete="given-name"
              classNames={{ label: styles.label, input: styles.input }}
              error={errors.firstName?.message}
              {...register("firstName")}
            />
            <TextInput
              label="Прізвище"
              placeholder="Введіть прізвище"
              radius="md"
              autoComplete="family-name"
              classNames={{ label: styles.label, input: styles.input }}
              error={errors.lastName?.message}
              {...register("lastName")}
            />
          </div>

          <div>
            <TextInput
              label="Ім'я користувача"
              placeholder="Введіть нікнейм"
              leftSection={<AtSign size={16} />}
              radius="md"
              autoComplete="username"
              autoCapitalize="none"
              spellCheck={false}
              classNames={{ label: styles.label, input: styles.input }}
              error={errors.userName?.message}
              {...register("userName")}
            />
            <Text className={styles.urlHint}>
              URL вашого профілю: marketly.ua/@{watchedUserName || "…"}
            </Text>
          </div>

          <div>
            <div className={styles.bioHeader}>
              <Text
                component="label"
                htmlFor="profile-bio"
                className={styles.label}
              >
                Про себе
              </Text>
              <Text
                className={`${styles.charCounter} ${
                  watchedBio.length >= BIO_MAX_LENGTH
                    ? styles.charCounterLimit
                    : ""
                }`}
              >
                {watchedBio.length}/{BIO_MAX_LENGTH}
              </Text>
            </div>
            <Textarea
              id="profile-bio"
              maxLength={BIO_MAX_LENGTH}
              autosize
              minRows={3}
              maxRows={6}
              radius="md"
              classNames={{ input: styles.input }}
              error={errors.bio?.message}
              {...register("bio")}
            />
          </div>

          <TextInput
            label="Email"
            placeholder="example@gmail.com"
            readOnly
            radius="md"
            classNames={{
              label: styles.label,
              input: `${styles.input} ${styles.inputReadonly}`,
            }}
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
                comboboxProps={{ zIndex: 1002 }}
                classNames={{ label: styles.label, input: styles.input }}
                error={fieldState.error?.message}
              />
            )}
          />

          <Controller
            name="currencyId"
            control={control}
            render={({ field, fieldState }) => (
              <Select
                label="Валюта"
                placeholder="Оберіть валюту"
                data={currencyOptions}
                value={field.value != null ? String(field.value) : null}
                onChange={(val) => field.onChange(val ? Number(val) : null)}
                onBlur={field.onBlur}
                disabled={currenciesLoading}
                rightSection={
                  currenciesLoading ? <Loader size={14} /> : undefined
                }
                allowDeselect={false}
                radius="md"
                comboboxProps={{ zIndex: 1002 }}
                classNames={{ label: styles.label, input: styles.input }}
                error={fieldState.error?.message}
              />
            )}
          />
        </div>
      </div>

      <div className={styles.actions}>
        <Button
          variant="outline"
          type="button"
          onClick={close}
          disabled={isBusy}
        >
          Скасувати
        </Button>
        <Button className={styles.saveButton} type="submit" loading={isBusy}>
          Зберегти зміни
        </Button>
      </div>
    </form>
  );
}
