import { Title, Text, Accordion } from "@mantine/core";
import { Lock } from "lucide-react";
import Button from "@/shared/ui/buttons/button";
import { useNavigate } from "react-router-dom";
import styles from "./PrivacyPolicyPage.module.css";

const POLICY_SECTIONS = [
  {
    value: "collection",
    title: "1. Які дані ми збираємо",
    content:
      "Ми збираємо персональну інформацію, яку ви надаєте під час реєстрації або оформлення замовлення: ім'я, номер телефону, електронну пошту, адресу доставки та історію замовлень.",
  },
  {
    value: "usage",
    title: "2. Як ми використовуємо ваші дані",
    content:
      "Ваші дані використовуються виключно для обробки та доставки замовлень, зв'язку з вами щодо стану купівлі, надання підтримки та покращення роботи маркетплейсу.",
  },
  {
    value: "sharing",
    title: "3. Передача даних третім особам",
    content:
      "Ми передаємо ваші дані лише тим партнерам, які безпосередньо беруть участь у виконанні замовлення (наприклад, службам доставки «Нова Пошта» та платіжним системам). Ми ніколи не продаємо ваші дані третім особам.",
  },
  {
    value: "protection",
    title: "4. Захист інформації",
    content:
      "Усі особисті дані та платежі захищені сучасними протоколами шифрування (SSL). Доступ до вашої інформації мають лише авторизовані співробітники.",
  },
  {
    value: "cookies",
    title: "5. Файли Cookie",
    content:
      "Ми використовуємо файли cookie для того, щоб зберігати ваш кошик, запам'ятовувати обрані товари та робити взаємодію з сайтом більш зручною.",
  },
  {
    value: "rights",
    title: "6. Ваші права",
    content:
      "Ви маєте право в будь-який момент переглянути, змінити або попросити повністю видалити свої персональні дані з нашої системи через кабінет або звернення до підтримки.",
  },
];

export default function PrivacyPolicyPage() {
  const navigate = useNavigate();

  return (
    <main className={styles.privacyPage}>
      <div className={styles.container}>
        <section className={styles.headerSection}>
          <div className={styles.badge}>
            <Lock size={16} />
            <span>Конфіденційність</span>
          </div>
          <Title className={styles.title}>Політика конфіденційності</Title>
          <Text className={styles.subtitle}>
            Останнє оновлення: жовтень 2026. Ми поважаємо ваші персональні дані
            та дбаємо про їхній повний захист.
          </Text>
        </section>

        <section className={styles.contentSection}>
          <Accordion
            variant="separated"
            radius="md"
            defaultValue="collection"
            className={styles.accordion}
          >
            {POLICY_SECTIONS.map((section) => (
              <Accordion.Item
                key={section.value}
                value={section.value}
                className={styles.accordionItem}
              >
                <Accordion.Control className={styles.accordionControl}>
                  {section.title}
                </Accordion.Control>
                <Accordion.Panel className={styles.accordionPanel}>
                  {section.content}
                </Accordion.Panel>
              </Accordion.Item>
            ))}
          </Accordion>
        </section>

        <section className={styles.contactCard}>
          <div className={styles.contactContent}>
            <Title order={3} className={styles.contactTitle}>
              Залишилися запитання щодо персональних даних?
            </Title>
            <Text className={styles.contactText}>
              Якщо ви хочете видалити свій акаунт або уточнити умов обробки
              даних, напишіть нашій службі підтримки.
            </Text>
          </div>
          <Button
            onClick={() => navigate("/support")}
            className={styles.contactButton}
          >
            Написати в підтримку
          </Button>
        </section>
      </div>
    </main>
  );
}
