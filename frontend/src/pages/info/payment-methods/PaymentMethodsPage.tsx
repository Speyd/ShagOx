import styles from "./PaymentMethodsPage.module.css";
import Container from "@/shared/ui/layout/container";
import PaymentMethods from "@/widgets/info/payment-methods";

export default function PaymentMethodsPage() {
  return (
    <section className={styles.paymentMethodsPage}>
      <Container>
        <PaymentMethods />
      </Container>
    </section>
  );
}
