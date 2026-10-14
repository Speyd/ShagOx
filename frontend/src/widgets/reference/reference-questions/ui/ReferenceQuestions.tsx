import { Text } from "@mantine/core";
import styles from "./ReferenceQuestions.module.css";
import Button from "@/shared/ui/button";
import { useNavigate } from "react-router-dom";
import { ArrowRight } from "lucide-react";

const questions = [
  {
    question: "Як відстежити замовлення?",
    buttonLabel: "Відстежити",
    to: "/order-tracking",
  },
  {
    question: "Методи оплати",
    buttonLabel: "Методи оплати",
    to: "/payment-methods",
  },
  {
    question: "Терміни доставки",
    buttonLabel: "Терміни доставки",
    to: "/delivery-terms",
  },
];

export default function ReferenceQuestions() {
  const navigate = useNavigate();
  return (
    <section className={styles.referenceQuestions}>
      <Text fz={24} fw={500}>
        Поширені запитання
      </Text>
      <div className={styles.questionsContainer}>
        {questions.map((item, index) => (
          <div key={index} className={styles.questionItem}>
            <Text fz={15} fw={400}>
              {item.question}
            </Text>
            <Button onClick={() => navigate(item.to)} className={styles.button}>
              {item.buttonLabel}
              <ArrowRight size={20} />
            </Button>
          </div>
        ))}
      </div>
    </section>
  );
}
