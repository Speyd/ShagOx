import type { ReactNode } from "react";
import type { ColumnDef } from "@tanstack/react-table";
import type { BaseEntity } from "@/shared/api/crud";
import type { TranslationConfig } from "@/shared/api/translations";

export type FormValues = Record<string, unknown>;

export type SelectOption = { value: string; label: string };

type BaseField = {
  name: string;
  label: string;
  required?: boolean;
  hint?: string;
  placeholder?: string;
  fullWidth?: boolean;
  disabledOnEdit?: boolean;
};

export type FieldConfig = BaseField &
  (
    | { kind: "text"; maxLength?: number; pattern?: RegExp; patternMessage?: string }
    | { kind: "textarea"; maxLength?: number }
    | { kind: "number"; min?: number; max?: number; allowDecimal?: boolean }
    | {
        kind: "select";
        options: SelectOption[];
        searchable?: boolean;
        clearable?: boolean;
      }
    | { kind: "switch" }
  );

export type CrudTranslations<T> = TranslationConfig & {
  codeOf: (values: FormValues, item?: T) => string;
};

export type CrudPageProps<T extends BaseEntity> = {
  title: string;
  description?: string;
  entityName: string;
  endpoint: string;
  commandEndpoint?: string;
  columns: ColumnDef<T>[];
  fields: FieldConfig[];
  getDefaults: (item?: T) => FormValues;
  toPayload: (values: FormValues, item?: T) => unknown;
  validate?: (values: FormValues) => Record<string, string> | null;
  translations?: CrudTranslations<T>;
  searchParam?: string;
  searchPlaceholder?: string;
  filters?: Record<string, string | number | undefined>;
  toolbarExtra?: ReactNode;
  headerActions?: ReactNode;
  backTo?: string;
  rowExtra?: (item: T) => ReactNode;
  onCreate?: () => void;
  onEdit?: (item: T) => void;
  createLabel?: string;
  canCreate?: boolean;
  canEdit?: boolean;
  canDelete?: boolean;
  canHide?: boolean;
  pageSize?: number;
  minWidth?: number;
  modalSize?: string;
  embedded?: boolean;
};
