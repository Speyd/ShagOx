import styles from "./PaymentMethodsPage.module.css";
import Container from "@/shared/ui/container";
import PaymentMethods from "@/widgets/payment-methods";

export default function PaymentMethodsPage() {
  return (
    <section className={styles.paymentMethodsPage}>
      <Container>
        <PaymentMethods />
      </Container>
    </section>
  );
}
