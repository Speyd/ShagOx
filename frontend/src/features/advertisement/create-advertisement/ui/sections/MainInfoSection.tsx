import { useState, useRef } from "react";
import {
  TextInput,
  Select,
  NumberInput,
  Textarea,
  Group,
  Badge,
  ActionIcon,
  Button,
} from "@mantine/core";
import {
  Bold,
  Italic,
  Underline,
  Strikethrough,
  List,
  ListOrdered,
  Link2,
  Plus,
  X,
} from "lucide-react";
import type { Control } from "react-hook-form";
import { Controller } from "react-hook-form";
import type { CreateAdvertisementFormData } from "../../model/types";
import styles from "../CreateAdvertisementForm.module.css";
import type { CreateAdvertisementSchema } from "../../model/schemas/createAdvertisementSchema";

type MainInfoSectionProps = {
  control: Control<CreateAdvertisementSchema>;
  titleValue?: string;
  descriptionValue?: string;
  hashtagsValue?: string[];
  categories: { value: string; label: string }[];
  isLoadingCategories?: boolean;
  errors: Partial<
    Record<keyof CreateAdvertisementFormData, { message?: string }>
  >;
  setValue: (field: keyof CreateAdvertisementFormData, value: unknown) => void;
};

export default function MainInfoSection({
  control,
  titleValue = "",
  descriptionValue = "",
  hashtagsValue = [],
  categories,
  isLoadingCategories,
  errors,
  setValue,
}: MainInfoSectionProps) {
  const [hashtagInputOpen, setHashtagInputOpen] = useState(false);
  const [newHashtag, setNewHashtag] = useState("");
  const textareaRef = useRef<HTMLTextAreaElement | null>(null);

  const handleApplyFormatting = (prefix: string, suffix = "") => {
    const textarea = textareaRef.current;
    if (!textarea) return;

    const start = textarea.selectionStart;
    const end = textarea.selectionEnd;
    const selected = descriptionValue.substring(start, end);
    const replacement = `${prefix}${selected || "текст"}${suffix}`;
    const nextText =
      descriptionValue.substring(0, start) +
      replacement +
      descriptionValue.substring(end);

    setValue("description", nextText);

    setTimeout(() => {
      textarea.focus();
      textarea.setSelectionRange(
        start + prefix.length,
        start + prefix.length + (selected.length || 5),
      );
    }, 0);
  };

  const handleAddHashtag = () => {
    const clean = newHashtag.trim().replace(/^#+/, "");
    if (!clean) return;
    if (!hashtagsValue.includes(clean)) {
      setValue("hashtags", [...hashtagsValue, clean]);
    }
    setNewHashtag("");
    setHashtagInputOpen(false);
  };

  const handleRemoveHashtag = (tagToRemove: string) => {
    setValue(
      "hashtags",
      hashtagsValue.filter((t) => t !== tagToRemove),
    );
  };

  return (
    <div className={styles.card}>
      <h3 className={styles.cardTitle}>Основна інформація</h3>

      <div className={styles.fieldsContainer}>
        {/* Title */}
        <div className={styles.field}>
          <div className={styles.fieldLabelRow}>
            <label className={styles.fieldLabel}>
              Назва товару <span className={styles.requiredStar}>*</span>
            </label>
            <span
              className={`${styles.charCounter} ${
                (titleValue?.length || 0) > 120 ? styles.charCounterLimit : ""
              }`}
            >
              {(titleValue || "").length}/120
            </span>
          </div>
          <Controller
            name="title"
            control={control}
            render={({ field }) => (
              <TextInput
                {...field}
                placeholder="Введіть назву товару"
                size="md"
                maxLength={120}
                error={errors.title?.message}
              />
            )}
          />
        </div>

        {/* Category */}
        <div className={styles.field}>
          <label className={styles.fieldLabel}>
            Категорія <span className={styles.requiredStar}>*</span>
          </label>
          <Controller
            name="categoryId"
            control={control}
            render={({ field }) => (
              <Select
                placeholder="Оберіть категорію"
                data={categories}
                size="md"
                searchable
                clearable
                nothingFoundMessage="Категорій не знайдено"
                disabled={isLoadingCategories}
                value={field.value ?? null}
                onChange={(val) => field.onChange(val ?? "")}
                error={errors.categoryId?.message}
              />
            )}
          />
        </div>

        {/* Price and Stock */}
        <Group grow align="flex-start" gap="md">
          <div className={styles.field}>
            <label className={styles.fieldLabel}>
              Ціна <span className={styles.requiredStar}>*</span>
            </label>
            <Controller
              name="price"
              control={control}
              render={({ field }) => (
                <NumberInput
                  placeholder="Введіть ціну"
                  size="md"
                  min={1}
                  clampBehavior="strict"
                  allowNegative={false}
                  rightSection={<span className={styles.unitText}>₴</span>}
                  value={field.value === "" ? "" : field.value}
                  onChange={(val) => field.onChange(val)}
                  error={errors.price?.message}
                />
              )}
            />
          </div>

          <div className={styles.field}>
            <label className={styles.fieldLabel}>
              Кількість <span className={styles.requiredStar}>*</span>
            </label>
            <Controller
              name="stock"
              control={control}
              render={({ field }) => (
                <NumberInput
                  placeholder="Введіть кількість"
                  size="md"
                  min={1}
                  clampBehavior="strict"
                  allowNegative={false}
                  allowDecimal={false}
                  rightSection={<span className={styles.unitText}>шт.</span>}
                  value={field.value === "" ? "" : field.value}
                  onChange={(val) => field.onChange(val)}
                  error={errors.stock?.message}
                />
              )}
            />
          </div>
        </Group>

        {/* Description */}
        <div className={styles.field}>
          <div className={styles.fieldLabelRow}>
            <label className={styles.fieldLabel}>
              Опис товару <span className={styles.requiredStar}>*</span>
            </label>
            <span className={styles.charCounter}>
              {(descriptionValue || "").length}/2000
            </span>
          </div>

          <div className={styles.editorWrapper}>
            <div className={styles.editorToolbar}>
              <button
                type="button"
                className={styles.toolbarBtn}
                onClick={() => handleApplyFormatting("**", "**")}
                title="Жирний"
              >
                <Bold size={15} />
              </button>
              <button
                type="button"
                className={styles.toolbarBtn}
                onClick={() => handleApplyFormatting("*", "*")}
                title="Курсив"
              >
                <Italic size={15} />
              </button>
              <button
                type="button"
                className={styles.toolbarBtn}
                onClick={() => handleApplyFormatting("<u>", "</u>")}
                title="Підкреслення"
              >
                <Underline size={15} />
              </button>
              <button
                type="button"
                className={styles.toolbarBtn}
                onClick={() => handleApplyFormatting("~~", "~~")}
                title="Закреслення"
              >
                <Strikethrough size={15} />
              </button>
              <div className={styles.toolbarDivider} />
              <button
                type="button"
                className={styles.toolbarBtn}
                onClick={() => handleApplyFormatting("\n- ")}
                title="Маркований список"
              >
                <List size={15} />
              </button>
              <button
                type="button"
                className={styles.toolbarBtn}
                onClick={() => handleApplyFormatting("\n1. ")}
                title="Нумерований список"
              >
                <ListOrdered size={15} />
              </button>
              <button
                type="button"
                className={styles.toolbarBtn}
                onClick={() => handleApplyFormatting("[", "](https://)")}
                title="Посилання"
              >
                <Link2 size={15} />
              </button>
            </div>

            <Controller
              name="description"
              control={control}
              render={({ field }) => (
                <Textarea
                  placeholder="Опишіть ваш товар, його особливості, переваги..."
                  minRows={5}
                  maxRows={12}
                  autosize
                  maxLength={2000}
                  className={styles.editorTextarea}
                  {...field}
                  ref={(elem) => {
                    field.ref(elem);
                    textareaRef.current = elem;
                  }}
                  error={errors.description?.message}
                />
              )}
            />
          </div>
        </div>

        {/* Hashtags */}
        <div className={styles.field}>
          <label className={styles.fieldLabel}>Хештег</label>

          <div className={styles.hashtagsWrapper}>
            {(hashtagsValue || []).map((tag) => (
              <Badge
                key={tag}
                variant="light"
                size="lg"
                color="blue"
                rightSection={
                  <ActionIcon
                    size="xs"
                    color="blue"
                    radius="xl"
                    variant="transparent"
                    onClick={() => handleRemoveHashtag(tag)}
                  >
                    <X size={12} />
                  </ActionIcon>
                }
              >
                #{tag}
              </Badge>
            ))}

            {hashtagInputOpen ? (
              <Group gap="xs">
                <TextInput
                  placeholder="Введіть хештег"
                  size="sm"
                  value={newHashtag}
                  onChange={(e) => setNewHashtag(e.currentTarget.value)}
                  onKeyDown={(e) => {
                    if (e.key === "Enter") {
                      e.preventDefault();
                      handleAddHashtag();
                    }
                  }}
                  autoFocus
                />
                <Button size="sm" variant="filled" onClick={handleAddHashtag}>
                  Ок
                </Button>
                <Button
                  size="sm"
                  variant="subtle"
                  onClick={() => setHashtagInputOpen(false)}
                >
                  Скасувати
                </Button>
              </Group>
            ) : (
              <Button
                variant="light"
                size="sm"
                leftSection={<Plus size={14} />}
                onClick={() => setHashtagInputOpen(true)}
                className={styles.addHashtagButton}
              >
                Додати хештег
              </Button>
            )}
          </div>
        </div>
      </div>
    </div>
  );
}
