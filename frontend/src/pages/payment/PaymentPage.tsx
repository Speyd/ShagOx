import { useMemo, useState } from "react";
import { Button, Group, Radio, Text, TextInput } from "@mantine/core";
import { ArrowLeft, Check, Lock, WalletCards } from "lucide-react";
import { useNavigate } from "react-router-dom";
import { toast } from "sonner";

import Container from "@/shared/ui/container";
import OrderSummary from "@/widgets/checkout/checkout-order-summary";
import CheckoutStepper from "@/widgets/checkout/checkout-stepper";
import { useGetBasket } from "@/features/basket/model/hooks/useGetBasket";
import styles from "./PaymentPage.module.css";

type PaymentMethod = "cash" | "online" | "iban" | "installment";

export default function PaymentPage() {
  const navigate = useNavigate();
  const { data: basket, isLoading } = useGetBasket();
  const [paymentMethod, setPaymentMethod] = useState<PaymentMethod>("cash");
  const [cardNumber, setCardNumber] = useState("");
  const [cardDate, setCardDate] = useState("");
  const [cardCvv, setCardCvv] = useState("");

  const [selectedIds] = useState(() => {
    const savedIds = sessionStorage.getItem("checkoutItemIds");

    if (!savedIds) return new Set<number>();

    try {
      return new Set<number>(JSON.parse(savedIds) as number[]);
    } catch {
      return new Set<number>();
    }
  }, []);

  const items = useMemo(
    () =>
      (basket?.basketItems || []).filter((item) => selectedIds.has(item.id)),
    [basket, selectedIds],
  );

  const handlePayment = () => {
    sessionStorage.setItem("paymentMethod", paymentMethod);
    toast.success("Спосіб оплати обрано!");
  };

  if (isLoading) {
    return <div className={styles.loading}>Завантаження оплати...</div>;
  }

  if (items.length === 0) {
    return (
      <div className={styles.paymentPage}>
        <Container>
          <div className={styles.empty}>
            <Text fw={600} fz={22}>
              Немає товарів для оплати
            </Text>
            <Button
              onClick={() => navigate("/basket")}
              radius="md"
              className={styles.primaryButton}
            >
              Повернутися до кошика
            </Button>
          </div>
        </Container>
      </div>
    );
  }

  return (
    <div className={styles.paymentPage}>
      <Container>
        <CheckoutStepper currentStep="payment" />

        <div className={styles.contentWrapper}>
          <OrderSummary items={items} />

          <section className={styles.paymentSection}>
            <Text fw={600} fz={24} className={styles.title}>
              Метод оплати
            </Text>

            <Radio.Group
              value={paymentMethod}
              onChange={(value) => setPaymentMethod(value as PaymentMethod)}
            >
              <div className={styles.paymentOptions}>
                <label
                  className={`${styles.paymentOption} ${
                    paymentMethod === "cash" ? styles.selected : ""
                  }`}
                >
                  <Radio value="cash" />
                  <span className={styles.optionContent}>
                    <span className={styles.optionTitle}>
                      Оплата при отриманні
                    </span>
                    <span className={styles.optionDescription}>
                      Готівкою або карткою при отриманні
                    </span>
                  </span>
                </label>

                <label
                  className={`${styles.paymentOption} ${
                    paymentMethod === "online" ? styles.selected : ""
                  }`}
                >
                  <Radio value="online" />
                  <span className={styles.optionContent}>
                    <span className={styles.optionTitle}>Оплата онлайн</span>
                    <span className={styles.optionDescription}>
                      Visa, Mastercard, Google Pay, Apple Pay
                    </span>
                  </span>
                  <span className={styles.paymentIcons}>
                    <span className={styles.visa}>VISA</span>
                    <span className={styles.mastercard}>MC</span>
                  </span>
                </label>

                {paymentMethod === "online" && (
                  <div className={styles.cardDetails}>
                    <TextInput
                      label="Номер картки"
                      placeholder="0000 0000 0000 0000"
                      value={cardNumber}
                      onChange={(event) =>
                        setCardNumber(event.currentTarget.value)
                      }
                    />
                    <div className={styles.cardRow}>
                      <TextInput
                        label="MM / YY"
                        placeholder="MM / YY"
                        value={cardDate}
                        onChange={(event) =>
                          setCardDate(event.currentTarget.value)
                        }
                      />
                      <TextInput
                        label="CVV"
                        placeholder="000"
                        type="password"
                        value={cardCvv}
                        onChange={(event) =>
                          setCardCvv(event.currentTarget.value)
                        }
                      />
                    </div>
                  </div>
                )}

                <label
                  className={`${styles.paymentOption} ${
                    paymentMethod === "iban" ? styles.selected : ""
                  }`}
                >
                  <Radio value="iban" />
                  <span className={styles.optionContent}>
                    <span className={styles.optionTitle}>
                      Безготівковий розрахунок (IBAN)
                    </span>
                    <span className={styles.optionDescription}>
                      Оплата для юридичних осіб в ПП або ФОП
                    </span>
                  </span>
                </label>

                <label
                  className={`${styles.paymentOption} ${
                    paymentMethod === "installment" ? styles.selected : ""
                  }`}
                >
                  <Radio value="installment" />
                  <span className={styles.optionContent}>
                    <span className={styles.optionTitle}>
                      Оплата частинами
                    </span>
                    <span className={styles.optionDescription}>
                      Monobank, ПриватБанк, Розстрочка
                    </span>
                  </span>
                  <WalletCards size={24} className={styles.walletIcon} />
                </label>
              </div>
            </Radio.Group>

            <Group className={styles.actions} justify="space-between">
              <Button
                variant="outline"
                leftSection={<ArrowLeft size={18} />}
                onClick={() => navigate("/checkout")}
                className={styles.backButton}
              >
                Назад
              </Button>

              <Button
                onClick={handlePayment}
                rightSection={<Check size={18} />}
                className={styles.continueButton}
              >
                Продовжити
              </Button>
            </Group>

            <div className={styles.security}>
              <Lock size={17} />
              <Text size="sm">Ваші дані під захистом</Text>
            </div>
          </section>
        </div>
      </Container>
    </div>
  );
}
