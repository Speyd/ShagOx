import type {
  AttributeInfo,
  AttributeType,
} from "@/entities/advertisement/model/types";
import type {
  ApiAttributes,
  ApiAttributeValue,
  AttributeFormValue,
  SelectOption,
} from "../model/types";

export const BOOLEAN_OPTIONS: SelectOption[] = [
  { value: "true", label: "Так" },
  { value: "false", label: "Ні" },
];

export const getAttributeLabel = (definition: AttributeInfo) =>
  definition.label || definition.key;

export const isNumericAttribute = ({ type }: AttributeInfo) =>
  type === "Integer" || type === "Decimal";

/** Select і Boolean обираються зі списку, решта вводиться вручну. */
export const hasPredefinedOptions = ({ type }: AttributeInfo) =>
  type === "Select" || type === "Boolean";

export function hasValue(
  value: AttributeFormValue,
): value is NonNullable<AttributeFormValue> {
  if (value === null || value === undefined || value === "") return false;
  return !Array.isArray(value) || value.length > 0;
}

const toList = (value: NonNullable<AttributeFormValue>) =>
  Array.isArray(value) ? value : [value];

function validateNumber(
  definition: AttributeInfo,
  raw: string | number,
): string | null {
  const value = Number(raw);

  if (raw === "" || Number.isNaN(value)) return `«${raw}» не є числом`;
  if (definition.type === "Integer" && !Number.isInteger(value))
    return "Потрібне ціле число";
  if (definition.min != null && value < definition.min)
    return `Мінімальне значення: ${definition.min}`;
  if (definition.max != null && value > definition.max)
    return `Максимальне значення: ${definition.max}`;

  return null;
}

/** Повертає текст помилки або null, якщо значення коректне. */
export function validateAttributeValue(
  definition: AttributeInfo,
  value: AttributeFormValue,
): string | null {
  if (!hasValue(value)) {
    return definition.required ? "Обов'язкове поле" : null;
  }

  if (!isNumericAttribute(definition)) return null;

  for (const item of toList(value)) {
    const error = validateNumber(definition, item);
    if (error) return error;
  }

  return null;
}

function toApiScalar(type: AttributeType, raw: string | number) {
  switch (type) {
    case "Select": // бекенд очікує id значення словника
    case "Integer":
    case "Decimal":
      return Number(raw);
    case "Boolean":
      return raw === "true";
    default:
      return String(raw).trim();
  }
}

export function toApiValue(
  definition: AttributeInfo,
  value: NonNullable<AttributeFormValue>,
): ApiAttributeValue {
  const values = toList(value).map((item) =>
    toApiScalar(definition.type, item),
  );

  return definition.multiple ? values : values[0];
}

/** Перетворює значення форми у JSON атрибутів для бекенда, пропускаючи порожні. */
export function toApiAttributes(
  values: Record<string, AttributeFormValue>,
  definitionsByKey: Map<string, AttributeInfo>,
): ApiAttributes {
  const result: ApiAttributes = {};

  for (const [key, value] of Object.entries(values)) {
    const definition = definitionsByKey.get(key);
    if (definition && hasValue(value)) {
      result[key] = toApiValue(definition, value);
    }
  }

  return result;
}

export function getOptionLabel(
  options: SelectOption[] | undefined,
  value: string,
): string {
  return options?.find((option) => option.value === value)?.label ?? value;
}
