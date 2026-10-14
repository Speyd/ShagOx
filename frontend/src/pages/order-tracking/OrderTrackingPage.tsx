import { Text } from "@mantine/core";
import styles from "./OrderTrackingPage.module.css";
import Container from "@/shared/ui/container";
import { ReferenceNeedHelp } from "@/widgets/reference/reference-need-help";
import FastTracking from "@/widgets/order-tracking/fast-track";
import TrackingInstruction from "@/widgets/order-tracking/tracking-instruction/ui/TrackingInstruction";

export default function OrderTrackingPage() {
  return (
    <section className={styles.orderTrackingPage}>
      <Container>
        <div className={styles.orderTrackingContent}>
          <div className={styles.orderTrackingHeader}>
            <Text fz={40} fw={700}>
              Як відстежити замовлення?
            </Text>
            <div className="divider" />
          </div>

          <div className={styles.orderTrackingBody}>
            <div className={styles.orderTrackingMain}>
              <FastTracking />
              <TrackingInstruction />
            </div>
            <ReferenceNeedHelp />
          </div>
        </div>
      </Container>
    </section>
  );
}
