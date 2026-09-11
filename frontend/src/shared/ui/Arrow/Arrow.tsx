
import { ArrowRight } from "lucide-react";
import styles from "./Arrow.module.css";
import { useNavigate } from "react-router-dom";
import { Text } from "@mantine/core";

type ArrowProps = {
  text: string;
  className?: string;
  to?: string;
};

function Arrow({ text, className, to }: ArrowProps) {
  const navigate = useNavigate();
  return (
    <div
      className={`${styles.seeMore} ${className ?? ""}`}
      onClick={() => navigate(to ?? "")}
    >
      <Text fw={600} fz={16}>{text}</Text>

      <div className={styles.arrow}>
        <div className={styles.line}></div>
        <ArrowRight className={styles.icon} />
      </div>
    </div>
  );
}

export default Arrow;
