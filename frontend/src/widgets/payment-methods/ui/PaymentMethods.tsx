import { Text } from "@mantine/core";
import styles from "./PaymentMethods.module.css";
import { paymentMethods } from "@/widgets/payment-methods/model/data";

export default function PaymentMethods() {
  return (
    <section className={styles.paymentMethodsContent}>
      <div className={styles.paymentMethodsHeader}>
        <Text fz={40} fw={700}>
          Методи оплати
        </Text>
        <Text fz={14} fw={400} className={styles.paymentMethodsDescription}>
          Ми пропонуємо різноманітні безпечні та зручні способи оплати для
          вашого комфорту. Оберіть той, <br /> що підходить вам найкраще.
        </Text>
      </div>
      <div className={styles.paymentMethodsContainer}>
        {paymentMethods.map((method, index) => (
          <div
            key={index}
            className={`${styles.paymentMethodCard} ${
              index === 4 ? styles.spanTwo : ""
            }`}
          >
            <div className={styles.paymentMethodHeader}>
              {method.icon}
              <Text fz={20} fw={400} className={styles.paymentMethodLabel}>
                {method.label}
              </Text>
            </div>
            <div className="divider" />
            <Text fz={12} fw={400} className={styles.paymentMethodDescription}>
              {method.description}
            </Text>
            <div className={styles.paymentMethodButtons}>
              {method.buttons.map((button, index) => (
                <div key={index} className={styles.paymentMethodButton}>
                  <Text fz={10} fw={400}>
                    {button.label}
                  </Text>
                </div>
              ))}
            </div>
          </div>
        ))}
      </div>
    </section>
  );
}
