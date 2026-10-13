import { useState } from "react";
import { Checkbox, Text } from "@mantine/core";
import {
  Mail,
  MessageSquare,
  MessageSquareText,
  Smartphone,
  Tv,
} from "lucide-react";
import Button from "@/shared/ui/button";
import styles from "./CommunicationChannels.module.css";

type ChannelItem = {
  id: string;
  title: string;
  subtitle: string;
  icon: React.ReactNode;
  defaultChecked: boolean;
};

const communicationChannels: ChannelItem[] = [
  {
    id: "email",
    title: "Email",
    subtitle: "user@example.com",
    icon: <Mail size={22} color="#001736" />,
    defaultChecked: true,
  },
  {
    id: "sms",
    title: "SMS",
    subtitle: "+380 ** *** ** 12",
    icon: <MessageSquare size={22} color="#001736" />,
    defaultChecked: true,
  },
  {
    id: "push",
    title: "Push-сповіщення",
    subtitle: "У браузері та додатку",
    icon: <Smartphone size={22} color="#001736" />,
    defaultChecked: false,
  },
  {
    id: "messengers",
    title: "Viber / Telegram",
    subtitle: "Через офіційного бота",
    icon: <MessageSquareText size={22} color="#001736" />,
    defaultChecked: true,
  },
];

export default function CommunicationChannels() {
  const [channels, setChannels] = useState<Record<string, boolean>>(() =>
    communicationChannels.reduce(
      (acc, item) => ({ ...acc, [item.id]: item.defaultChecked }),
      {},
    ),
  );

  const handleToggle = (id: string) => {
    setChannels((prev) => ({ ...prev, [id]: !prev[id] }));
  };

  return (
    <div className={styles.card}>
      <div className={styles.header}>
        <Tv size={22} className={styles.headerIcon} />
        <Text fz={20} fw={600} className={styles.title}>
          Канали зв'язку
        </Text>
      </div>

      <div className={styles.grid}>
        {communicationChannels.map((item) => (
          <div
            key={item.id}
            className={`${styles.channelCard} ${
              channels[item.id] ? styles.activeCard : ""
            }`}
            onClick={() => handleToggle(item.id)}
          >
            <div className={styles.channelHeader}>
              {item.icon}
              <Checkbox
                checked={channels[item.id]}
                onChange={() => {}}
                classNames={{
                  input: styles.checkboxInput,
                }}
              />
            </div>

            <div className={styles.textContainer}>
              <Text fz={15} fw={600} className={styles.channelTitle}>
                {item.title}
              </Text>
              <Text fz={13} fw={400} c="dimmed">
                {item.subtitle}
              </Text>
            </div>
          </div>
        ))}
      </div>

      <div className={styles.footer}>
        <Button className={styles.saveButton}>Зберегти зміни</Button>
      </div>
    </div>
  );
}
