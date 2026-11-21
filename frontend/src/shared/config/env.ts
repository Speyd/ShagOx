const googleClientId = import.meta.env.VITE_GOOGLE_CLIENT_ID;

if (!googleClientId) {
  console.warn("VITE_GOOGLE_CLIENT_ID не задано у змінних оточення.");
}

export const env = {
  GOOGLE_CLIENT_ID: googleClientId ?? "",
} as const;
