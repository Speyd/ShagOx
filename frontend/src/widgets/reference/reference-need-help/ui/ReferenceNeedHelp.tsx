import { Text } from "@mantine/core";
import { useNavigate } from "react-router-dom";
import styles from "./ReferenceNeedHelp.module.css";
import Button from "@/shared/ui/button";
import { ArrowRight, Clock, Mail, Phone } from "lucide-react";
import React from "react";

const blocks = [
  {
    label: "ТЕЛЕФОН",
    info: "0 96 292 08 78",
    icon: <Phone size={20} />,
  },
  {
    label: "EMAIL",
    info: "support@company.com",
    icon: <Mail size={20} />,
  },
  {
    label: "ГРАФІК РОБОТИ",
    info: "Пн-Нд: 09:00 - 21:00",
    icon: <Clock size={20} />,
  },
];

export default function ReferenceNeedHelp() {
  const navigate = useNavigate();
  return (
    <section className={styles.referenceNeedHelp}>
      <Text fz={24} fw={500}>
        Потрібна допомога?
      </Text>
      <Text fz={15} fw={400} className={styles.contactDescription}>
        Наші спеціалісти готові допомогти вам з будь-якими питаннями щодо
        техніки та послуг.
      </Text>
      <div className={styles.contactBlocks}>
        {blocks.map((block, index) => (
          <React.Fragment key={index}>
            <div className={styles.contactBlock}>
              {block.icon}
              <div className={styles.contactText}>
                <Text fz={12} fw={400} className={styles.contactLabel}>
                  {block.label}
                </Text>
                <Text fz={15} fw={400}>
                  {block.info}
                </Text>
              </div>
            </div>
            {index < blocks.length - 1 && <div className="divider" />}
          </React.Fragment>
        ))}
      </div>
      <Button onClick={() => navigate("/support")} className={styles.button}>
        <Text fz={15} fw={400}>
          Зв'язок з підтримкою
        </Text>
        <ArrowRight size={20} />
      </Button>
    </section>
  );
}
