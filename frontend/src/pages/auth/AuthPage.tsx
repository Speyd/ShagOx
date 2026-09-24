import LoginForm from "@/features/auth/ui/login";
import styles from "./AuthPage.module.css";
import heroAuthBg from "@/shared/assets/images/hero-auth.png";
import { Group, Image, Tabs, Text } from "@mantine/core";
import { useState } from "react";
import logo from "@/shared/assets/images/logo.png";
import RegisterForm from "@/features/auth/ui/register";
import { Link } from "react-router-dom";
import { MoveRight } from "lucide-react";
import { GoogleLoginButton } from "@/shared/ui/google-button/GoogleLoginButton";

export default function AuthPage() {
  const [activeTab, setActiveTab] = useState<string | null>("login");

  return (
    <div
      className={styles.authPage}
      style={{ backgroundImage: `url(${heroAuthBg})` }}
    >
      <div className={styles.wrapper}>
        <div className={styles.header}>
          <Group gap={5} align="center" wrap="nowrap">
            <Image
              src={logo}
              alt="Marketly"
              w={40}
              h={40}
              fit="contain"
              style={{ flexShrink: 0 }}
            />
            <Text fz={24} fw={600}>
              Marketly
            </Text>
          </Group>

          <Text fz={24} fw={400} c="var(--text-base)">
            Ласкаво просимо
          </Text>
        </div>
        <Tabs
          value={activeTab}
          onChange={setActiveTab}
          className={styles.tabsContainer}
        >
          <Tabs.List className={styles.list}>
            <Tabs.Tab value="login" className={styles.tab}>
              Вхід
            </Tabs.Tab>
            <Tabs.Tab value="register" className={styles.tab}>
              Реєстрація
            </Tabs.Tab>
          </Tabs.List>

          <Tabs.Panel value="login" pt="xs" className={styles.panel}>
            <LoginForm />
          </Tabs.Panel>

          <Tabs.Panel value="register" pt="xs" className={styles.panel}>
            <RegisterForm />
          </Tabs.Panel>
        </Tabs>

        <div className={styles.divider}>
          <span>або</span>
        </div>

        <div className={styles.formContainer}>
          <GoogleLoginButton />
        </div>

        <div className={styles.guestWrapper}>
          <Link to="/" className={styles.guestLink}>
            <Text fw={700} fz={12}>
              Продовжити як гість
            </Text>

            <MoveRight size={20} />
          </Link>
        </div>
      </div>
    </div>
  );
}
