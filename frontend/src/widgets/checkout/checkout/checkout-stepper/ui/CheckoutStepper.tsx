import { Check, CreditCard, Truck } from "lucide-react";
import styles from "./CheckoutStepper.module.css";

type CheckoutStepperProps = {
  currentStep?: "shipping" | "delivery" | "payment";
};

export default function CheckoutStepper({
  currentStep = "shipping",
}: CheckoutStepperProps) {
  const isDeliveryStep =
    currentStep === "delivery" || currentStep === "payment";
  const isPaymentStep = currentStep === "payment";

  return (
    <div className={styles.checkoutStepper}>
      <div className={`${styles.step} ${styles.active}`}>
        <span className={styles.icon}>
          <Check size={16} />
        </span>
        <span>Відправлення</span>
      </div>

      <div className={styles.line} />

      <div className={`${styles.step} ${isDeliveryStep ? styles.active : ""}`}>
        <span className={styles.icon}>
          <Truck size={16} />
        </span>
        <span>Доставка</span>
      </div>

      <div className={styles.line} />

      <div
        className={`${styles.step} ${isPaymentStep ? styles.active : ""}`}
      >
        <span className={styles.icon}>
          <CreditCard size={16} />
        </span>
        <span>Оплата</span>
      </div>
    </div>
  );
}
