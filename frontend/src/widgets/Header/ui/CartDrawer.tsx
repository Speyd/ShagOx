import { Drawer, Divider, Image, Stack, Text } from "@mantine/core";
import { X } from "lucide-react";

import emptyCartImage from "@/shared/assets/images/empty-cart.svg";

import styles from "./CartDrawer.module.css";

type CartDrawerProps = {
  opened: boolean;
  onClose: () => void;
};

export default function CartDrawer({ opened, onClose }: CartDrawerProps) {
  return (
    <Drawer
      opened={opened}
      onClose={onClose}
      position="right"
      size={380}
      withCloseButton={false}
      padding={0}
      classNames={{
        content: styles.drawerContent,
        body: styles.drawerBody,
      }}
    >
      <div className={styles.cartDrawer}>
        <div className={styles.header}>
          <Text fw={600} fz={30}>
            Кошик
          </Text>

          <button
            className={styles.closeButton}
            onClick={onClose}
            aria-label="Закрити кошик"
          >
            <X strokeWidth={2.5} className="icon" />
          </button>
        </div>

        <Divider />

        <div className={styles.emptyCart}>
          <Image
            src={emptyCartImage}
            alt="Порожній кошик"
            w={200}
            h="auto"
            className={styles.emptyCartImage}
          />

          <Stack gap={6} align="center">
            <Text className={styles.title}>Кошик порожній</Text>

            <Text className={styles.description}>
              Додайте товари до кошика,
              <br />
              щоб оформити замовлення
            </Text>
          </Stack>
        </div>
      </div>
    </Drawer>
  );
}
