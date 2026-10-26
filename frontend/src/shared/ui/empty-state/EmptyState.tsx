import { Image, Text } from "@mantine/core";
import { useNavigate } from "react-router-dom";
import Button from "@/shared/ui/button";
import defaultLogo from "@/shared/assets/images/empty-cart.svg";
import styles from "./EmptyState.module.css";

type EmptyStateProps = {
  label?: string;
  description?: string;
  image?: string;
  button?: boolean;
  buttonLabel?: string;
  onButtonClick?: () => void;
};

export default function EmptyState({
  label = "Поки що тут порожньо",
  description = "Перейдіть до каталогу або скористайтеся пошуком, щоб знайти потрібні товари.",
  image = defaultLogo,
  button = true,
  buttonLabel = "Перейти до головної",
  onButtonClick,
}: EmptyStateProps) {
  const navigate = useNavigate();

  const handleButtonClick = () => {
    if (onButtonClick) {
      onButtonClick();
    } else {
      navigate("/");
    }
  };

  return (
    <section className={styles.container}>
      <div className={styles.content}>
        <Text fz={24} fw={500}>
          {label}
        </Text>
        <Text fz={15} fw={500} c="dimmed" className={styles.description}>
          {description}
        </Text>
        {button && <Button onClick={handleButtonClick}>{buttonLabel}</Button>}
      </div>

      <Image src={image} alt={label} w={230} h={200} fit="contain" />
    </section>
  );
}
