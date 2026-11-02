import { Title, Text, Accordion } from "@mantine/core";
import { ShieldCheck, RotateCcw, Wrench, Truck } from "lucide-react";
import Button from "@/shared/ui/buttons/button";
import { useNavigate } from "react-router-dom";
import styles from "./WarrantyPage.module.css";

const CONDITIONS = [
  {
    icon: RotateCcw,
    title: "14 днів на повернення",
    description:
      "Ви можете повернути або обміняти товар неналежної або належної якості протягом двох тижнів.",
  },
  {
    icon: ShieldCheck,
    title: "Офіційна гарантія",
    description:
      "На всю техніку надається гарантія від виробника або продавця терміном від 6 до 36 місяців.",
  },
  {
    icon: Wrench,
    title: "Авторизовані СЦ",
    description:
      "Ремонт здійснюється у сертифікованих сервісних центрах із використанням оригінальних запчастин.",
  },
  {
    icon: Truck,
    title: "Легка відправка",
    description:
      "Повернення та гарантійні випадки оформлюються через найближче відділення Нової Пошти.",
  },
];

const RETURN_STEPS = [
  {
    step: "01",
    title: "Перевірте умови",
    text: "Товар не має слідів використання, збережено товарний вигляд, комплектацію та упаковку.",
  },
  {
    step: "02",
    title: "Заповніть заявку",
    text: "Перейдіть у кабінет в розділ «Мої замовлення» або напишіть у службу підтримки.",
  },
  {
    step: "03",
    title: "Надішліть товар",
    text: "Упакуйте пристрій разом із гарантійним талоном і чек/накладною та відправте службою доставки.",
  },
  {
    step: "04",
    title: "Отримайте кошти",
    text: "Після перевірки стану товару ми повернемо гроші на карту протягом 1–3 робочих днів.",
  },
];

const FAQ_ITEMS = [
  {
    value: "documents",
    question: "Які документи потрібні для гарантійного обслуговування?",
    answer:
      "Вам знадобляться: чек або видаткова накладна (доступні в електронному вигляді в особистому кабінеті), гарантійний талон та документ, що посвідчує особу.",
  },
  {
    value: "non-returnable",
    question: "Які товари не підлягають поверненню?",
    answer:
      "Згідно із Законом України «Про захист прав споживачів», поверненню не підлягають засоби особистої гігієни (наприклад, електробритви, зубні щітки), навушники-вкладиші, а також товари у блістерній упаковці, якщо вона була розкрита.",
  },
  {
    value: "duration",
    question: "Скільки триває гарантійний ремонт?",
    answer:
      "Стандартний термін діагностики та ремонту становить від 7 до 14 робочих днів з моменту отримання пристрою сервісним центром.",
  },
  {
    value: "shipping-cost",
    question: "Хто оплачує доставку при гарантійному випадку?",
    answer:
      "Якщо у товарі виявлено заводський брак, усі витрати на доставку покриває маркетплейс або продавець.",
  },
];

export default function WarrantyPage() {
  const navigate = useNavigate();

  return (
    <main className={styles.warrantyPage}>
      <div className={styles.container}>
        <section className={styles.headerSection}>
          <div className={styles.badge}>
            <ShieldCheck size={16} />
            <span>Захист покупця</span>
          </div>
          <Title className={styles.title}>Гарантія та повернення</Title>
          <Text className={styles.subtitle}>
            Ми дбаємо про те, щоб кожна покупка в Marketly була безпечною та
            комфортною.
          </Text>
        </section>

        <section className={styles.conditionsGrid}>
          {CONDITIONS.map((item) => {
            const Icon = item.icon;
            return (
              <div key={item.title} className={styles.conditionCard}>
                <div className={styles.iconWrapper}>
                  <Icon size={24} />
                </div>
                <Text className={styles.cardTitle}>{item.title}</Text>
                <Text className={styles.cardDescription}>
                  {item.description}
                </Text>
              </div>
            );
          })}
        </section>

        <section className={styles.stepsSection}>
          <div className={styles.sectionHeader}>
            <Title order={2} className={styles.sectionTitle}>
              Як повернути або обміняти товар
            </Title>
            <Text className={styles.sectionSubtitle}>
              Простий процес у 4 кроки
            </Text>
          </div>

          <div className={styles.stepsGrid}>
            {RETURN_STEPS.map((item) => (
              <div key={item.step} className={styles.stepCard}>
                <span className={styles.stepNumber}>{item.step}</span>
                <Text className={styles.stepTitle}>{item.title}</Text>
                <Text className={styles.stepText}>{item.text}</Text>
              </div>
            ))}
          </div>
        </section>

        <section className={styles.faqSection}>
          <div className={styles.sectionHeader}>
            <Title order={2} className={styles.sectionTitle}>
              Часті запитання
            </Title>
          </div>

          <Accordion
            variant="separated"
            radius="md"
            className={styles.accordion}
          >
            {FAQ_ITEMS.map((faq) => (
              <Accordion.Item
                key={faq.value}
                value={faq.value}
                className={styles.accordionItem}
              >
                <Accordion.Control className={styles.accordionControl}>
                  {faq.question}
                </Accordion.Control>
                <Accordion.Panel className={styles.accordionPanel}>
                  {faq.answer}
                </Accordion.Panel>
              </Accordion.Item>
            ))}
          </Accordion>
        </section>

        <section className={styles.supportBanner}>
          <div className={styles.supportContent}>
            <Title order={3} className={styles.supportTitle}>
              Залишилися запитання щодо гарантії?
            </Title>
            <Text className={styles.supportText}>
              Наша служба турботи допоможе швидко вирішити будь-яку проблему з
              товаром.
            </Text>
          </div>
          <Button
            onClick={() => navigate("/support")}
            className={styles.supportButton}
          >
            Зв'язатися з підтримкою
          </Button>
        </section>
      </div>
    </main>
  );
}
