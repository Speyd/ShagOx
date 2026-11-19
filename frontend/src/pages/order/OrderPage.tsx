import { Badge, Text } from "@mantine/core";
import { CircleCheck } from "lucide-react";
import { Link, useLocation, useNavigate, useParams } from "react-router-dom";
import Container from "@/shared/ui/layout/container";
import Button from "@/shared/ui/buttons/button";
import { useOrder } from "@/entities/order/model/hooks";
import {
  DELIVERY_TYPE_LABELS,
  ORDER_STATUS_COLORS,
  ORDER_STATUS_LABELS,
  PAYMENT_METHOD_LABELS,
  formatMoney,
  formatOrderDate,
  getDeliveryAddress,
} from "@/entities/order/lib/labels";
import styles from "./OrderPage.module.css";

export default function OrderPage() {
  const { id } = useParams();
  const location = useLocation();
  const navigate = useNavigate();
  const justCreated = location.state?.justCreated === true;

  const { data: order, isLoading, isError } = useOrder(Number(id));

  if (isLoading) {
    return <div className={styles.message}>Завантаження замовлення...</div>;
  }

  if (isError || !order) {
    return (
      <div className={styles.message}>
        <Text>Замовлення не знайдено.</Text>
        <Button onClick={() => navigate("/profile/orders")}>Мої замовлення</Button>
      </div>
    );
  }

  const symbol = order.currency.symbol;
  const deliveryPrice =
    order.deliveryType === "Pickup"
      ? "Безкоштовно"
      : order.deliveryPrice > 0
        ? formatMoney(order.deliveryPrice, symbol)
        : "За тарифами перевізника";

  return (
    <div className={styles.page}>
      <Container>
        <div className={styles.content}>
          {justCreated && (
            <div className={styles.success}>
              <CircleCheck size={40} className={styles.successIcon} />
              <div>
                <Text fw={700} fz={22}>
                  Дякуємо! Замовлення оформлено
                </Text>
                <Text c="dimmed" fz={14}>
                  Ми зв'яжемося з вами за номером {order.phone} для
                  підтвердження замовлення.
                </Text>
              </div>
            </div>
          )}

          <div className={styles.card}>
            <div className={styles.header}>
              <div>
                <Text fw={700} fz={20}>
                  Замовлення № {order.orderNumber}
                </Text>
                <Text c="dimmed" fz={14}>
                  від {formatOrderDate(order.createdAt)}
                </Text>
              </div>
              <Badge color={ORDER_STATUS_COLORS[order.orderStatus]} variant="light" size="lg">
                {ORDER_STATUS_LABELS[order.orderStatus]}
              </Badge>
            </div>

            <div className={styles.details}>
              <div>
                <Text c="dimmed" fz={13}>Отримувач</Text>
                <Text fz={15}>{order.firstName} {order.lastName}</Text>
                <Text fz={14} c="dimmed">{order.phone}</Text>
              </div>
              <div>
                <Text c="dimmed" fz={13}>Доставка</Text>
                <Text fz={15}>
                  {order.deliveryProvider?.label ?? DELIVERY_TYPE_LABELS[order.deliveryType]}
                </Text>
                <Text fz={14} c="dimmed">{getDeliveryAddress(order)}</Text>
              </div>
              <div>
                <Text c="dimmed" fz={13}>Оплата</Text>
                <Text fz={15}>{PAYMENT_METHOD_LABELS[order.paymentMethod]}</Text>
              </div>
            </div>

            {order.comment && (
              <Text fz={14} c="dimmed">
                Коментар: {order.comment}
              </Text>
            )}
          </div>

          <div className={styles.card}>
            <Text fw={600} fz={18}>
              Товари
            </Text>

            <ul className={styles.items}>
              {order.orderItems.map((item) => (
                <li key={item.id} className={styles.item}>
                  {item.advertisement.picture?.url ? (
                    <img
                      src={item.advertisement.picture.url}
                      alt={item.advertisement.title}
                      className={styles.image}
                    />
                  ) : (
                    <div className={styles.image} />
                  )}

                  <Link
                    to={`/advertisement/${item.advertisementId}`}
                    className={styles.itemTitle}
                  >
                    {item.advertisement.title}
                  </Link>

                  <Text fz={14} c="dimmed" className={styles.itemQuantity}>
                    {item.quantity} × {formatMoney(item.price, symbol)}
                  </Text>

                  <Text fw={600} fz={15} className={styles.itemTotal}>
                    {formatMoney(item.totalPrice, symbol)}
                  </Text>
                </li>
              ))}
            </ul>

            <div className={styles.totals}>
              <div className={styles.row}>
                <Text c="dimmed">Товари</Text>
                <Text>{formatMoney(order.subtotal, symbol)}</Text>
              </div>
              <div className={styles.row}>
                <Text c="dimmed">Доставка</Text>
                <Text>{deliveryPrice}</Text>
              </div>
              <div className={styles.row}>
                <Text fw={700} fz={18}>До сплати</Text>
                <Text fw={700} fz={18}>{formatMoney(order.totalPrice, symbol)}</Text>
              </div>
            </div>
          </div>

          <div className={styles.actions}>
            <Button variant="outline" onClick={() => navigate("/profile/orders")}>
              Мої замовлення
            </Button>
            <Button onClick={() => navigate("/catalog")}>Продовжити покупки</Button>
          </div>
        </div>
      </Container>
    </div>
  );
}
