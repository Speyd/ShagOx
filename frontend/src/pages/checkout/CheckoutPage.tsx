import Container from "@/shared/ui/container";
import CheckoutDetailsForm from "@/widgets/checkout/checkout-details-form";
import OrderSummary from "@/widgets/checkout/checkout-order-summary";
import CheckoutStepper from "@/widgets/checkout/checkout-stepper";
import styles from "./CheckoutPage.module.css";

export default function CheckoutPage() {
  return (
    <div className={styles.checkoutPage}>
      <Container>
        <CheckoutStepper />
        <div className={styles.contentWrapper}>
          <OrderSummary />
          <CheckoutDetailsForm />
        </div>
      </Container>
    </div>
  );
}
