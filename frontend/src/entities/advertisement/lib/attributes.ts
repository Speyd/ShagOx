import type { VariantAttribute } from "@/shared/lib/types/advertisements";

export function formatAttribute(attr: VariantAttribute): string {
  return attr.values
    .map((value) => {
      if (attr.attribute.type === "Boolean") return value.value === "True" ? "Так" : "Ні";
      return value.label || value.value || value.code;
    })
    .filter(Boolean)
    .join(", ");
}
