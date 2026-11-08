const COLOR_HEX: Record<string, string> = {
  black: "#1f2023",
  white: "#ffffff",
  silver: "#c7cbd1",
  gray: "#8e9299",
  graphite: "#45474d",
  titanium: "#8b867c",
  blue: "#2f5fd0",
  "light-blue": "#9cc5f0",
  green: "#3c8d5a",
  red: "#d23b3b",
  pink: "#f3b6c8",
  violet: "#8a63d2",
  yellow: "#f2cf45",
  orange: "#f08a35",
  gold: "#d8b46a",
  beige: "#e6d8bf",
  brown: "#7f5539",
};

const FALLBACK_COLOR = "#e5e7eb";

export function getColorHex(code: string): string {
  if (/^#([0-9a-f]{3}){1,2}$/i.test(code)) return code;
  return COLOR_HEX[code] ?? FALLBACK_COLOR;
}
