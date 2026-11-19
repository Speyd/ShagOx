import React, { useEffect, useRef, useState } from "react";
import { Button, Image, Text } from "@mantine/core";
import googleLogo from "@/shared/assets/icons/google.png";
import { env } from "@/shared/config/env";
import { useGoogleLogin } from "@/features/auth/model/hooks/useGoogleLogin";
import styles from "./GoogleLoginButton.module.css";

declare global {
  interface Window {
    google?: {
      accounts: {
        oauth2: {
          initCodeClient: (config: {
            client_id: string;
            scope: string;
            ux_mode?: "popup" | "redirect";
            callback: (response: {
              code?: string;
              error?: string;
              error_description?: string;
            }) => void;
          }) => GoogleCodeClient;
        };
      };
    };
  }
}

type GoogleCodeClient = {
  requestCode: () => void;
};

export default function GoogleLoginButton(): React.ReactNode {
  const codeClientRef = useRef<GoogleCodeClient | null>(null);
  const [isGoogleReady, setIsGoogleReady] = useState(false);
  const { mutate: loginWithGoogle, isPending } = useGoogleLogin();

  useEffect(() => {
    const initializeGoogleClient = () => {
      if (
        codeClientRef.current ||
        !window.google?.accounts?.oauth2 ||
        !env.GOOGLE_CLIENT_ID
      ) {
        return;
      }

      codeClientRef.current = window.google.accounts.oauth2.initCodeClient({
        client_id: env.GOOGLE_CLIENT_ID,
        scope: "openid profile email",
        ux_mode: "popup",
        callback: (response) => {
          if (response.code) {
            loginWithGoogle({ code: response.code });
          } else if (response.error) {
            console.error(
              "Помилка Google OAuth:",
              response.error_description ?? response.error,
            );
          }
        },
      });

      setIsGoogleReady(true);
    };

    initializeGoogleClient();

    const script = document.getElementById("google-gsi-script");
    script?.addEventListener("load", initializeGoogleClient);

    return () => {
      script?.removeEventListener("load", initializeGoogleClient);
      codeClientRef.current = null;
    };
  }, [loginWithGoogle]);

  const handleGoogleLogin = () => {
    if (codeClientRef.current) {
      codeClientRef.current.requestCode();
    } else {
      console.error("Google SDK не завантажився");
    }
  };

  return (
    <div className={styles.container}>
      <Button
        leftSection={
          <Image src={googleLogo} alt="Google" w={20} h={20} fit="contain" />
        }
        onClick={handleGoogleLogin}
        disabled={!isGoogleReady || isPending}
        variant="default"
        size="lg"
        radius="md"
        fullWidth
      >
        <Text fw={700} fz={15}>
          Продовжити з Google
        </Text>
      </Button>
    </div>
  );
}
