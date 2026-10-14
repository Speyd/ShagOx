import { Text } from "@mantine/core";
import styles from "./ReferenceReturn.module.css";
import { Archive, Banknote, Calendar } from "lucide-react";

const blocks = [
  {
    icon: <Calendar size={30} />,
    label: "14 днів на роздуми",
    description:
      "Ви маєте право повернути товар належної якості протягом 14 днів з моменту покупки.",
  },
  {
    icon: <Archive size={30} />,
    label: "Збереження вигляду",
    description:
      "Товар не повинен мати слідів використання,мають бути збережені пломби та оригінальна упаковка.",
  },
  {
    icon: <Banknote size={30} />,
    label: "Повернення коштів",
    description:
      "Гроші повертаються на вашу банківську картку протягом 3-5 робочих днів після отримання нами товару.",
  },
];

export default function ReferenceReturn() {
  return (
    <section className={styles.referenceQuestions}>
      <Text fz={24} fw={500}>
        Умови повернення
      </Text>
      <div className={styles.questionsContainer}>
        {blocks.map((item, index) => (
          <div key={index} className={styles.questionItem}>
            {item.icon}
            <Text fz={14} fw={400}>
              {item.label}
            </Text>
            <Text fz={12} fw={400} className={styles.questionDescription}>
              {item.description}
            </Text>
          </div>
        ))}
      </div>
    </section>
  );
}
