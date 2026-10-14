import { useState } from "react";
import { Switch, Text } from "@mantine/core";
import { Bell } from "lucide-react";
import styles from "./NotificationTypes.module.css";

type NotificationItem = {
  id: string;
  title: string;
  description: string;
  defaultChecked: boolean;
};

const notificationTypes: NotificationItem[] = [
  {
    id: "orders",
    title: "Замовлення та доставка",
    description: "Статуси оплати, відправки та отримання",
    defaultChecked: true,
  },
  {
    id: "promotions",
    title: "Акції та знижки",
    description: "Персональні пропозиції та розпродажі",
    defaultChecked: false,
  },
  {
    id: "new_products",
    title: "Новинки",
    description: "Надходження нових моделей техніки",
    defaultChecked: true,
  },
  {
    id: "system",
    title: "Системні сповіщення",
    description: "Зміни в акаунті, безпека та налаштування",
    defaultChecked: true,
  },
];

export default function NotificationTypes() {
  const [settings, setSettings] = useState<Record<string, boolean>>(() =>
    notificationTypes.reduce(
      (acc, item) => ({ ...acc, [item.id]: item.defaultChecked }),
      {},
    ),
  );

  const handleToggle = (id: string) => {
    setSettings((prev) => ({ ...prev, [id]: !prev[id] }));
  };

  return (
    <div className={styles.card}>
      <div className={styles.header}>
        <Bell size={22} className={styles.headerIcon} />
        <Text fz={20} fw={600} className={styles.title}>
          Типи сповіщень
        </Text>
      </div>

      <div className={styles.list}>
        {notificationTypes.map((item) => (
          <div key={item.id} className={styles.item}>
            <div className={styles.textContainer}>
              <Text fz={15} fw={600} className={styles.itemTitle}>
                {item.title}
              </Text>
              <Text fz={13} fw={400} c="dimmed">
                {item.description}
              </Text>
            </div>

            <Switch
              size="md"
              checked={settings[item.id]}
              onChange={() => handleToggle(item.id)}
              color={"#1b2e4b"}
              classNames={{
                root: styles.switchRoot,
                track: styles.switchTrack,
                thumb: styles.switchThumb,
              }}
            />
          </div>
        ))}
      </div>
    </div>
  );
}
