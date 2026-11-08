import { api } from "./api";
import type { PaginatedResponse } from "@/shared/lib/types/paginatedResponse";

export const LANGUAGES = [
  { code: "uk", label: "Українська" },
  { code: "en", label: "English" },
  { code: "ru", label: "Русский" },
  { code: "de", label: "Deutsch" },
] as const;

export type LanguageCode = (typeof LANGUAGES)[number]["code"];

export type TranslationConfig = {
  path: string;
  codeParam: string;
  withDescription?: boolean;
};

export type TranslationValue = {
  name: string;
  description?: string;
};

export type TranslationValues = Record<string, TranslationValue>;

type TranslationDto = {
  id: number;
  name: string;
  description?: string;
};

async function findTranslations(
  config: TranslationConfig,
  code: string,
  language: string,
) {
  const { data } = await api.get<PaginatedResponse<TranslationDto>>(
    `${config.path}/search`,
    {
      params: {
        [config.codeParam]: code,
        Language: language,
        page: 1,
        pageSize: 5,
        status: "ActiveAndHidden",
      },
    },
  );
  return data.items;
}

export function emptyTranslations(): TranslationValues {
  return Object.fromEntries(
    LANGUAGES.map(({ code }) => [code, { name: "", description: "" }]),
  );
}

export async function loadTranslations(
  config: TranslationConfig,
  code: string,
): Promise<TranslationValues> {
  const entries = await Promise.all(
    LANGUAGES.map(async ({ code: language }) => {
      const items = await findTranslations(config, code, language).catch(
        () => [],
      );
      const item = items.length === 1 ? items[0] : undefined;
      return [
        language,
        { name: item?.name ?? "", description: item?.description ?? "" },
      ] as const;
    }),
  );

  return Object.fromEntries(entries);
}

export async function saveTranslations(
  config: TranslationConfig,
  translatableId: number,
  code: string,
  values: TranslationValues,
) {
  for (const { code: language } of LANGUAGES) {
    const value = values[language];
    const name = value?.name.trim();
    if (!name) continue;

    const body = {
      translatableId,
      language,
      name,
      ...(config.withDescription && {
        description: value.description?.trim() ?? "",
      }),
    };

    const existing = await findTranslations(config, code, language).catch(
      () => [],
    );

    if (existing.length === 1) {
      await api.put(`${config.path}/${existing[0].id}`, body);
    } else if (existing.length === 0) {
      await api.post(config.path, body);
    }
  }
}
