import { Table, Text } from "@mantine/core";
import styles from "./DeliveryTerms.module.css";
import Button from "@/shared/ui/button";
import { Phone } from "lucide-react";

import { termsBlock, deliveryBlock } from "@/widgets/delivery-terms/model/data";

export default function DeliveryTerms() {
  return (
    <section className={styles.deliveryTermsContent}>
      <div className={styles.deliveryTermsHeader}>
        <Text fz={40} fw={700}>
          Терміни доставки
        </Text>
        <Text fz={14} fw={400} className={styles.deliveryTermsDescription}>
          Детальна інформація щодо умов та термінів доставки ваших замовлень.
        </Text>
      </div>

      <div className={styles.deliveryTermsContainer}>
        <div className={styles.leftColumn}>
          <div className={styles.tableCard}>
            <Text fz={24} fw={600} className={styles.tableTitle}>
              Орієнтовні терміни
            </Text>

            <Table className={styles.termsTable} unstyled>
              <Table.Thead>
                <Table.Tr>
                  <Table.Th>РЕГІОН</Table.Th>
                  <Table.Th>СЛУЖБА ДОСТАВКИ</Table.Th>
                  <Table.Th>ТЕРМІН</Table.Th>
                </Table.Tr>
              </Table.Thead>
              <Table.Tbody>
                {termsBlock.map((item, index) => (
                  <Table.Tr key={index}>
                    <Table.Td>{item.region}</Table.Td>
                    <Table.Td>{item.deliveryService}</Table.Td>
                    <Table.Td>{item.terms}</Table.Td>
                  </Table.Tr>
                ))}
              </Table.Tbody>
            </Table>
          </div>

          <div className={styles.deliveryMethods}>
            {deliveryBlock.map((item, index) => (
              <div
                key={index}
                className={`${styles.deliveryMethodCard} ${
                  index === 2 ? styles.spanTwo : ""
                }`}
              >
                <div className={styles.deliveryMethodHeader}>
                  {item.icon}
                  <Text fz={20} fw={400}>
                    {item.label}
                  </Text>
                </div>

                <Text
                  fz={14}
                  fw={400}
                  className={styles.deliveryTermsDescription}
                >
                  {item.description}
                </Text>
              </div>
            ))}
          </div>
        </div>

        <div className={styles.rightColumn}>
          <div className={styles.freeShipping}>
            <Text fz={24} fw={600}>
              Безкоштовна доставка
            </Text>
            <Text fz={14} fw={400} className={styles.deliveryTermsDescription}>
              Ми пропонуємо безкоштовну доставку для замовлень, сума яких
              перевищує встановлений ліміт.
            </Text>
            <div className="divider" />
            <div className={styles.freeShippingInfo}>
              <Text
                fz={12}
                fw={400}
                className={styles.deliveryTermsDescription}
              >
                СУМА ЗАМОВЛЕННЯ ВІД:
              </Text>
              <Text
                fz={12}
                fw={400}
                className={styles.deliveryTermsDescription}
              >
                5000 ₴
              </Text>
            </div>
            <Button>Підтвердити</Button>
          </div>

          <div className={styles.questionsInfo}>
            <Text fz={20} fw={400}>
              Залишились питання?
            </Text>
            <Text fz={12} fw={400} className={styles.deliveryTermsDescription}>
              Зв'яжіться з нашою службою підтримки для уточнення деталей
            </Text>
            <div className={styles.phoneInfo}>
              <Phone size={24} />
              <Text fz={12} fw={600}>
                +38 (044) 123-45-67
              </Text>
            </div>
          </div>
        </div>
      </div>
    </section>
  );
}
