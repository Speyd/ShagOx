import { api } from "@/shared/api/api";
import type { PaginatedResponse } from "@/shared/lib/types/paginatedResponse";
import type {
  CreateAdvertisementPayload,
  CreateAdvertisementResponse,
  CurrencyDto,
  LookupDto,
} from "../model/types";

export function buildAdvertisementFormData(payload: CreateAdvertisementPayload) {
  const formData = new FormData();

  formData.append("Title", payload.title);
  formData.append("Description", payload.description);
  formData.append("Popularity", "0");
  formData.append("CategoryId", String(payload.categoryId));
  formData.append("ConditionId", String(payload.conditionId));
  formData.append("CurrencyId", String(payload.currencyId));
  formData.append("StatusId", String(payload.statusId));
  payload.pictures.forEach((file) => formData.append("Pictures", file));
  formData.append("Attributes", JSON.stringify(payload.attributes));

  // Бекенд десеріалізує Variants через JsonSerializer без camelCase-політики,
  // тому назви полів мають бути в PascalCase.
  formData.append(
    "Variants",
    JSON.stringify(
      payload.variants.map((variant) => ({
        Price: variant.price,
        PreviousPrice: variant.previousPrice,
        Stock: variant.stock,
        Attributes: variant.attributes,
      })),
    ),
  );

  return formData;
}

export async function createAdvertisement(
  payload: CreateAdvertisementPayload,
) {
  const { data } = await api.post<CreateAdvertisementResponse>(
    "/advertisements",
    buildAdvertisementFormData(payload),
  );

  return data;
}

async function getLookup<T>(endpoint: string): Promise<T[]> {
  const { data } = await api.get<PaginatedResponse<T>>(endpoint, {
    params: { page: 1, pageSize: 100 },
  });
  return data.items;
}

export const getConditions = () => getLookup<LookupDto>("/conditions");
export const getStatuses = () => getLookup<LookupDto>("/statuses");
export const getCurrencies = () => getLookup<CurrencyDto>("/currencies");
