import { useState } from "react";
import { Modal, Text } from "@mantine/core";
import { useForm } from "react-hook-form";
import { useMutation, useQuery } from "@tanstack/react-query";
import { toast } from "sonner";
import { createEntity, updateEntity, type BaseEntity } from "@/shared/api/crud";
import { useInvalidateAll } from "@/shared/api/crudHooks";
import { getErrorMessage } from "@/shared/api/getErrorMessage";
import {
  emptyTranslations,
  loadTranslations,
  saveTranslations,
  type TranslationValues,
} from "@/shared/api/translations";
import Button from "@/shared/ui/buttons/button";
import { TranslationsEditor } from "@/widgets/admin/translations-editor";
import type { CrudPageProps, FormValues } from "../model/types";
import FieldRenderer from "./FieldRenderer";
import styles from "./CrudPage.module.css";

type EntityFormModalProps<T extends BaseEntity> = Pick<
  CrudPageProps<T>,
  | "entityName"
  | "endpoint"
  | "commandEndpoint"
  | "fields"
  | "getDefaults"
  | "toPayload"
  | "validate"
  | "translations"
  | "modalSize"
> & {
  opened: boolean;
  item?: T;
  onClose: () => void;
};

export default function EntityFormModal<T extends BaseEntity>(
  props: EntityFormModalProps<T>,
) {
  const { opened, item, onClose, entityName, modalSize = "lg" } = props;

  return (
    <Modal
      opened={opened}
      onClose={onClose}
      size={modalSize}
      radius="lg"
      centered
      title={
        <Text fw={700} size="lg" c="var(--color-base)">
          {item ? `Редагування: ${entityName}` : `Новий запис: ${entityName}`}
        </Text>
      }
    >
      {opened && (
        <EntityForm key={item?.id ?? "new"} {...props} />
      )}
    </Modal>
  );
}

function EntityForm<T extends BaseEntity>({
  item,
  onClose,
  endpoint,
  commandEndpoint = endpoint,
  fields,
  getDefaults,
  toPayload,
  validate,
  translations,
}: EntityFormModalProps<T>) {
  const isEdit = Boolean(item);
  const invalidate = useInvalidateAll();
  const [names, setNames] = useState<TranslationValues | null>(
    isEdit ? null : emptyTranslations(),
  );
  const [namesError, setNamesError] = useState<string>();

  const { control, handleSubmit, setError } = useForm<FormValues>({
    defaultValues: getDefaults(item),
  });

  const translationsQuery = useQuery({
    queryKey: ["admin-translations", translations?.path, item?.id],
    queryFn: () =>
      loadTranslations(translations!, translations!.codeOf(getDefaults(item), item)),
    enabled: Boolean(translations && item),
  });

  const currentNames = names ?? translationsQuery.data ?? emptyTranslations();

  const save = useMutation({
    mutationFn: async (values: FormValues) => {
      const payload = toPayload(values, item);
      let id = item?.id;

      if (item) {
        await updateEntity(commandEndpoint, item.id, payload);
      } else {
        const created = await createEntity(commandEndpoint, payload);
        id = created.id;
      }

      if (translations && id) {
        await saveTranslations(
          translations,
          id,
          translations.codeOf(values, item),
          currentNames,
        );
      }
    },
    onSuccess: () => {
      invalidate();
      toast.success(isEdit ? "Зміни збережено" : "Запис створено");
      onClose();
    },
    onError: (error) =>
      toast.error(getErrorMessage(error, "Не вдалося зберегти зміни")),
  });

  const onSubmit = handleSubmit((values) => {
    const errors = validate?.(values);
    if (errors && Object.keys(errors).length > 0) {
      Object.entries(errors).forEach(([name, message]) =>
        setError(name, { message }),
      );
      return;
    }
    if (translations && !currentNames.uk?.name.trim()) {
      setNamesError("Вкажіть назву українською");
      return;
    }
    setNamesError(undefined);
    save.mutate(values);
  });

  return (
    <form onSubmit={onSubmit} className={styles.form} noValidate>
      <div className={styles.grid}>
        {fields.map((field) => (
          <FieldRenderer
            key={field.name}
            field={field}
            control={control}
            isEdit={isEdit}
          />
        ))}
      </div>

      {translations && (
        <TranslationsEditor
          value={currentNames}
          onChange={setNames}
          withDescription={translations.withDescription}
          loading={translationsQuery.isFetching}
          error={namesError}
        />
      )}

      <footer className={styles.footer}>
        <Button variant="ghost" onClick={onClose} type="button">
          Скасувати
        </Button>
        <Button type="submit" loading={save.isPending}>
          {isEdit ? "Зберегти зміни" : "Створити"}
        </Button>
      </footer>
    </form>
  );
}
