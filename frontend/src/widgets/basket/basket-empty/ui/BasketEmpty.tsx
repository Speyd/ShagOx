import { Image, Text } from "@mantine/core";
import styles from "./BasketEmpty.module.css";
import logo from "@/shared/assets/images/empty-cart.svg";
import Button from "@/shared/ui/button";
import { useNavigate } from "react-router-dom";

export default function BasketEmpty() {
  const navigate = useNavigate();
  return (
    <section className={styles.basketEmpty}>
      <div className={styles.content}>
        <Text fz={24} fw={500}>
          Ваш кошик порожній
        </Text>
        <Text fz={15} fw={500} c="dimmed" className={styles.description}>
          Перейдіть до головної сторінки та скористайтеся пошуком, або <br />{" "}
          каталогом, щоб знайти все, що потрібно.
        </Text>
        <Button onClick={() => navigate("/")}>Перейти до головної</Button>
      </div>

      <Image src={logo} alt="Empty cart" w={230} h={200} />
    </section>
  );
}
