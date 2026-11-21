import type { City } from "@/shared/lib/types/city";

export function getCityLabel(city: City): string {
  return String(city.label ?? city.name ?? city.code ?? `Місто ${city.id}`);
}
