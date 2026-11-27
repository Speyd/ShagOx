import {
  AtSign,
  KeyRound,
  ShieldCheck,
  Trash2,
  type LucideIcon,
} from "lucide-react";

export type SettingsSectionId =
  | "password"
  | "contacts"
  | "two-factor"
  | "delete-account";

export type SettingsNavItem = {
  id: SettingsSectionId;
  label: string;
  icon: LucideIcon;
  danger?: boolean;
};

export const SETTINGS_NAV: SettingsNavItem[] = [
  { id: "password", label: "Зміна пароля", icon: KeyRound },
  { id: "contacts", label: "Пошта та телефон", icon: AtSign },
  { id: "two-factor", label: "Двофакторна автентифікація", icon: ShieldCheck },
  {
    id: "delete-account",
    label: "Видалення акаунта",
    icon: Trash2,
    danger: true,
  },
];
