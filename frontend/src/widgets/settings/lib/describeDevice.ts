export type DeviceKind = "desktop" | "mobile";

export type DeviceInfo = {
  kind: DeviceKind;
  name: string;
};

const OS_RULES: [RegExp, string][] = [
  [/Windows/i, "Windows"],
  [/iPhone/i, "iPhone"],
  [/iPad/i, "iPad"],
  [/Android/i, "Android"],
  [/Mac OS X|Macintosh/i, "macOS"],
  [/Linux/i, "Linux"],
];

const BROWSER_RULES: [RegExp, string][] = [
  [/Edg\//i, "Edge"],
  [/OPR\/|Opera/i, "Opera"],
  [/Firefox\//i, "Firefox"],
  [/Chrome\//i, "Chrome"],
  [/Safari\//i, "Safari"],
];

const match = (ua: string, rules: [RegExp, string][], fallback: string) =>
  rules.find(([pattern]) => pattern.test(ua))?.[1] ?? fallback;

/** Rough "OS • Browser" label for the current device. */
export function describeDevice(ua = navigator.userAgent): DeviceInfo {
  const os = match(ua, OS_RULES, "Невідома ОС");
  const browser = match(ua, BROWSER_RULES, "Браузер");

  return {
    kind: /Mobi|iPhone|Android/i.test(ua) ? "mobile" : "desktop",
    name: `${os} • ${browser}`,
  };
}
