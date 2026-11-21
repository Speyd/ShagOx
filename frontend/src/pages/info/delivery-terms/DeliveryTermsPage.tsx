import Container from "@/shared/ui/layout/container";
import styles from "./DeliveryTermsPage.module.css";
import DeliveryTerms from "@/widgets/info/delivery-terms";

export default function DeliveryTermsPage() {
  return (
    <section className={styles.deliveryTermsPage}>
      <Container>
        <DeliveryTerms />
      </Container>
    </section>
  );
}
