import { useEffect, useMemo, useRef, useState } from "react";
import { Controller } from "react-hook-form";
import {
  Avatar,
  TextInput,
  Textarea,
  Text,
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
import type { City } from "@/shared/lib/types/city";

type ProfileFormProps = {
  close: () => void;
};

const BIO_MAX_LENGTH = 160;
const CITY_SEARCH_MIN_LENGTH = 2;

export default function ProfileForm({ close }: ProfileFormProps) {
  const user = useAuthStore((state) => state.user);
  const mutation = useUpdateProfile();

  const { data: currencies, isLoading: currenciesLoading } = useGetCurrencies();

  const currencyOptions = currencies?.map((c) => ({
    value: String(c.id),
    label: `${c.symbol} ${c.code} — ${c.name}`,
  }));

  const [selectedCity, setSelectedCity] = useState<City | null>(
    user?.city ?? null,
  );
  const [citySearch, setCitySearch] = useState(() =>
    user?.city ? getCityLabel(user.city) : "",
  );
  
  const isShowingSelected =
    selectedCity != null && citySearch === getCityLabel(selectedCity);
  const citySearchTerm = isShowingSelected ? "" : citySearch;

  const { data: cityResults = [], isFetching: cityLoading } =
    useSearchCities(citySearchTerm);

  const cityById = useMemo(() => {
    const map = new Map<string, City>();
    if (selectedCity) map.set(String(selectedCity.id), selectedCity);
    for (const city of cityResults) map.set(String(city.id), city);
    return map;
  }, [selectedCity, cityResults]);

  const cityOptions = useMemo(
    () =>
      Array.from(cityById.values()).map((city) => ({
        value: String(city.id),
        label: getCityLabel(city),
      })),
    [cityById],
  );

  const cityNothingFound =
    citySearchTerm.trim().length < CITY_SEARCH_MIN_LENGTH
      ? `Введіть щонайменше ${CITY_SEARCH_MIN_LENGTH} символи`
      : cityLoading
        ? "Пошук…"
        : "Місто не знайдено";

  const [avatarPreview, setAvatarPreview] = useState<string | null>(null);
  const fileInputRef = useRef<HTMLInputElement>(null);

  const {
    register,
    handleSubmit,
    control,
    watch,
    setValue,
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

  const resetAvatar = () => {
    setValue("avatar", null);
    setAvatarPreview(null);
  };

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
                <button
                  type="button"
                  className={styles.avatarButton}
                  onClick={openFilePicker}
                  disabled={isBusy}
                  aria-label="Змінити фото профілю"
                  title="Змінити фото"
                >
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
                  <span className={styles.avatarOverlay} aria-hidden="true">
                    <Camera className={styles.avatarOverlayIcon} />
                    <span className={styles.avatarOverlayText}>
                      Змінити фото
                    </span>
                  </span>
                </button>

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
            <Text className={styles.fileHint}>
              Натисніть на фото, щоб змінити.
              <br />
              JPG, PNG або WEBP. Макс. 5 МБ
            </Text>
            {avatarPreview && (
              <button
                type="button"
                className={styles.resetAvatarButton}
                onClick={resetAvatar}
                disabled={isBusy}
              >
                Скасувати вибір
              </button>
            )}
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
              placeholder="Необов'язково"
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
              <Select
                label="Місто"
                placeholder="Почніть вводити назву міста"
                searchable
                searchValue={citySearch}
                onSearchChange={setCitySearch}
                filter={({ options }) => options}
                data={cityOptions}
                value={field.value != null ? String(field.value) : null}
                onChange={(val) => {
                  const city = val ? cityById.get(val) : undefined;
                  if (!city) return;
                  setSelectedCity(city);
                  setCitySearch(getCityLabel(city));
                  field.onChange(city.id);
                }}
                onBlur={field.onBlur}
                renderOption={({ option }) => {
                  const region = cityById.get(option.value)?.region;
                  const regionLabel = region?.label ?? region?.name;
                  return (
                    <div>
                      <Text size="sm">{option.label}</Text>
                      {regionLabel && (
                        <Text size="xs" c="dimmed">
                          {regionLabel}
                        </Text>
                      )}
                    </div>
                  );
                }}
                nothingFoundMessage={cityNothingFound}
                allowDeselect={false}
                rightSection={cityLoading ? <Loader size={14} /> : undefined}
                radius="md"
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
