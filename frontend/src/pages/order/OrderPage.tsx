import { Badge, Text } from "@mantine/core";
import { CircleCheck } from "lucide-react";
import {
  Link,
  useLocation,
  useNavigate,
  useParams,
  useSearchParams,
} from "react-router-dom";
import Container from "@/shared/ui/layout/container";
import { useAuthStore } from "@/features/auth";
import { useGetBasket } from "@/features/basket/model/hooks/useGetBasket";
import { OrderActions } from "@/features/order/order-actions";
import { getOrderRole } from "@/entities/order/lib/actions";
import { useGetUser } from "@/entities/user/model/hooks/useGetUser";
import { getUserDisplayName } from "@/entities/user/lib/displayName";
import { OrderTimeline } from "@/widgets/order/order-timeline";
import Button from "@/shared/ui/buttons/button";
import { useOrder } from "@/entities/order/model/hooks";
import {
  DELIVERY_TYPE_LABELS,
  PAYMENT_METHOD_LABELS,
  formatMoney,
  formatOrderDate,
  getDeliveryAddress,
} from "@/entities/order/lib/labels";
import {
  formatDateTime,
  getOrderDisplayStatus,
  getOrderPaymentState,
} from "@/entities/order/lib/payment";
import {
  OrderPayment,
  PaymentStatusBadge,
} from "@/widgets/order/order-payment";
import styles from "./OrderPage.module.css";

export default function OrderPage() {
  const { id } = useParams();
  const location = useLocation();
  const navigate = useNavigate();
  const [searchParams] = useSearchParams();
  const justCreated = location.state?.justCreated === true;
  // Монобанк після оплати повертає на /orders/:id?payment=return
  const returnedFromPayment = searchParams.get("payment") === "return";

  const { data: order, isLoading, isError } = useOrder(Number(id));
  const me = useAuthStore((state) => state.user);
  const role = order ? getOrderRole(order, me?.id) : null;

  // Покупцю показуємо продавця; продавцю покупець видно в «Отримувач».
  const sellerId = order?.sellerId ?? 0;
  const { data: seller } = useGetUser(sellerId, role === "buyer");

  // Після оформлення в кошику могли лишитися товари інших продавців.
  const { data: basket } = useGetBasket({ enabled: justCreated });
  const remainingInBasket = basket?.basketItems.length ?? 0;

  if (isLoading) {
    return <div className={styles.message}>Завантаження замовлення...</div>;
  }

  if (isError || !order) {
    return (
      <div className={styles.message}>
        <Text>Замовлення не знайдено.</Text>
        <Button onClick={() => navigate("/profile/orders")}>
          Мої замовлення
        </Button>
        <Button variant="outline" onClick={() => navigate("/profile/sales")}>
          Мої продажі
        </Button>
      </div>
    );
  }

  const isSeller = role === "seller";
  const symbol = order.currency.symbol;
  const payment = getOrderPaymentState(order);
  const status = getOrderDisplayStatus(order);
  const awaitsPayment = payment.kind === "awaiting";
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
                  {awaitsPayment
                    ? "Замовлення оформлено — залишилося оплатити"
                    : "Дякуємо! Замовлення оформлено"}
                </Text>
                <Text c="dimmed" fz={14}>
                  {awaitsPayment
                    ? "Оплатіть замовлення, щоб продавець отримав його в роботу."
                    : "Продавець отримав замовлення й підтвердить його найближчим часом."}
                </Text>
                {remainingInBasket > 0 && (
                  <Text fz={14} mt={6}>
                    У кошику ще {remainingInBasket} товар(ів) від інших
                    продавців — <Link to="/basket">оформити їх</Link>.
                  </Text>
                )}
              </div>
            </div>
          )}

          {!isSeller && (
            <OrderPayment
              order={order}
              returnedFromPayment={returnedFromPayment}
            />
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
              <Badge color={status.color} variant="light" size="lg">
                {status.label}
              </Badge>
            </div>

            <div className={styles.details}>
              <div>
                <Text c="dimmed" fz={13}>
                  {isSeller ? "Покупець" : "Отримувач"}
                </Text>
                <Text fz={15}>
                  {order.firstName} {order.lastName}
                </Text>
                <Text fz={14} c="dimmed">
                  {order.phone}
                </Text>
                {isSeller && (
                  <Text fz={14} c="dimmed">
                    {order.email}
                  </Text>
                )}
                {!isSeller && seller && (
                  <Text fz={13} c="dimmed" mt={6}>
                    Продавець: {getUserDisplayName(seller)}
                  </Text>
                )}
              </div>
              <div>
                <Text c="dimmed" fz={13}>
                  Доставка
                </Text>
                <Text fz={15}>
                  {order.deliveryProvider?.label ??
                    DELIVERY_TYPE_LABELS[order.deliveryType]}
                </Text>
                <Text fz={14} c="dimmed">
                  {getDeliveryAddress(order)}
                </Text>
              </div>
              <div>
                <Text c="dimmed" fz={13}>
                  Оплата
                </Text>
                <Text fz={15}>
                  {PAYMENT_METHOD_LABELS[order.paymentMethod]}
                </Text>
                {payment.kind === "on-delivery" && (
                  <Text fz={14} c="dimmed">
                    Сплачується під час отримання
                  </Text>
                )}
                <PaymentStatusBadge state={payment} />
                {payment.kind === "paid" && payment.paidAt && (
                  <Text fz={13} c="dimmed" mt={4}>
                    {formatDateTime(payment.paidAt)}
                  </Text>
                )}
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
              Статус замовлення
            </Text>
            <OrderTimeline order={order} />
            <OrderActions order={order} role={role} />
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
                <Text c="green">{deliveryPrice}</Text>
              </div>
              <div className={styles.row}>
                <Text fw={700} fz={18}>
                  {payment.kind === "paid" ? "Сплачено" : "До сплати"}
                </Text>
                <Text fw={700} fz={18}>
                  {formatMoney(order.totalPrice, symbol)}
                </Text>
              </div>
            </div>
          </div>

          <div className={styles.actions}>
            <Button
              variant="outline"
              onClick={() =>
                navigate(isSeller ? "/profile/sales" : "/profile/orders")
              }
            >
              {isSeller ? "Мої продажі" : "Мої замовлення"}
            </Button>
            <Button onClick={() => navigate("/catalog")}>
              Продовжити покупки
            </Button>
          </div>
        </div>
      </Container>
    </div>
  );
}
