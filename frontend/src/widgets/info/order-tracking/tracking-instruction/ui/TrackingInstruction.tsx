import { Text } from "@mantine/core";
import styles from "./TrackingInstruction.module.css";
import { Mailbox, Truck } from "lucide-react";

const steps = [
  {
    label: "Авторизуйтесь в особистому кабінеті",
    description:
      "Перейдіть до розділу 'Мої замовлення' у вашому профілі. Там зберігається повна історія всіх ваших покупок.",
  },
  {
    label: "Знайдіть потрібне замовлення",
    description:
      "Виберіть активне замовлення. У його деталях буде вказано поточний статус та орієнтовну дату доставки.",
  },
  {
    label: "Відстеження на сайті перевізника",
    description:
      "Якщо замовлення вже передано кур'єрській службі, ви отримаєте ТТН. Використовуйте сайти наших партнерів для точного трекінгу:",
    buttons: [
      {
        label: "НОВА ПОШТА",
        icon: <Truck size={24} />,
        link: "https://novaposhta.ua/tracking",
      },
      {
        label: "УКРПОШТА",
        icon: <Mailbox size={24} />,
        link: "https://ukrposhta.ua/vidslidkuvati",
      },
    ],
  },
];

export default function TrackingInstruction() {
  return (
    <section className={styles.trackingInstruction}>
      <Text fz={24} fw={600}>
        Детальна інструкція
      </Text>
      <div className={styles.stepsContainer}>
        {steps.map((step, index) => (
          <div className={styles.step}>
            <div className={styles.stepNumber}>
              <Text fz={18} fw={600}>
                {index + 1}
              </Text>
            </div>
            <div className={styles.stepTextContainer}>
              <Text fz={17} fw={400}>
                {step.label}
              </Text>
              <Text fz={14} fw={400}>
                {step.description}
              </Text>
              {step.buttons && (
                <div className={styles.buttonsContainer}>
                  {step.buttons.map((button) => (
                    <a
                      key={button.label}
                      href={button.link}
                      target="_blank"
                      rel="noopener noreferrer"
                      className={styles.button}
                    >
                      {button.icon}
                      <Text fz={12} fw={400}>
                        {button.label}
                      </Text>
                    </a>
                  ))}
                </div>
              )}
            </div>
          </div>
        ))}
      </div>
    </section>
  );
}
