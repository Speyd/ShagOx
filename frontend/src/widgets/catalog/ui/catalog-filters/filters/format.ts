export const formatRangeValue = (value: number, unit: string) =>
  `${String(value).replace(".", ",")}${unit ? ` ${unit}` : ""}`;
