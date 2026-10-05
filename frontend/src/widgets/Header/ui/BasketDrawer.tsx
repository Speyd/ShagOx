import {
  Drawer,
  Divider,
  Image,
  Stack,
  Text,
  Group,
  ScrollArea,
  Center,
  Loader,
} from "@mantine/core";
import { X } from "lucide-react";
import { Link, useNavigate } from "react-router-dom";
import { useMemo } from "react";

import emptyBasketImage from "@/shared/assets/images/empty-cart.svg";
import { useGetBasket } from "@/features/basket/model/hooks/useGetBasket";
import BasketItemsList from "@/widgets/basket/basket-items-list";
import { getAdvertisementPrice } from "@/shared/lib/types/advertisements";

import styles from "./BasketDrawer.module.css";
import Button from "@/shared/ui/button";
import Price from "@/shared/ui/price";

type BasketDrawerProps = {
  opened: boolean;
  onClose: () => void;
};

export default function BasketDrawer({ opened, onClose }: BasketDrawerProps) {
  const navigate = useNavigate();
  const { data: basket, isLoading } = useGetBasket();

  const items = useMemo(() => basket?.basketItems || [], [basket]);

  const totalPrice = useMemo(() => {
    return items.reduce((sum, item) => {
      const price = item.advertisement
        ? getAdvertisementPrice(item.advertisement)
        : 0;
      return sum + price * item.quantity;
    }, 0);
  }, [items]);

  const handleCheckout = () => {
    sessionStorage.setItem(
      "checkoutItemIds",
      JSON.stringify(items.map((item) => item.id)),
    );
    onClose();
    navigate("/checkout");
  };

  return (
    <Drawer
      opened={opened}
      onClose={onClose}
      position="right"
      size={650}
      withCloseButton={false}
      zIndex={1001}
      padding={0}
      classNames={{
        content: styles.drawerContent,
        body: styles.drawerBody,
      }}
    >
      <div className={styles.basketDrawer}>
        <div className={styles.header}>
          <Group gap="xs">
            <Text fw={600} fz={24}>
              Кошик
            </Text>
            {items.length > 0 && (
              <Text fz={18} c="dimmed" fw={500}>
                ({items.length})
              </Text>
            )}
          </Group>

          <button
            className={styles.closeButton}
            onClick={onClose}
            aria-label="Закрити кошик"
          >
            <X size={35} strokeWidth={2.5} className="icon" />
          </button>
        </div>

        <Divider />

        <div className={styles.content}>
          {isLoading ? (
            <Center h={300}>
              <Loader size="md" />
            </Center>
          ) : items.length === 0 ? (
            <div className={styles.emptyBasket}>
              <Image
                src={emptyBasketImage}
                alt="Порожній кошик"
                w={180}
                h="auto"
                className={styles.emptyBasketImage}
              />

              <Stack gap={6} align="center" mt="md">
                <Text fw={600} fz={20}>
                  Кошик порожній
                </Text>

                <Text
                  c="dimmed"
                  ta="center"
                  fz={14}
                >
                  Додайте товари до кошика,
                  <br />
                  щоб оформити замовлення
                </Text>
              </Stack>
            </div>
          ) : (
            <ScrollArea className={styles.scrollArea} offsetScrollbars>
              <BasketItemsList items={items} />
            </ScrollArea>
          )}
        </div>

        {!isLoading && items.length > 0 && (
          <div className={styles.footer}>
            <Divider mb="md" />

            <Group justify="space-between" mb="md">
              <Text fz={16} fw={500}>
                Разом до сплати:
              </Text>
              <Price price={totalPrice} />
            </Group>

            <div className={styles.actions}>
              <Button onClick={handleCheckout}>Оформити замовлення</Button>

              <Link to="/basket" className={styles.link} onClick={onClose}>
                Переглянути кошик
              </Link>
            </div>
          </div>
        )}
      </div>
    </Drawer>
  );
}
