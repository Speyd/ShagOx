import RegisterForm from "@/features/auth/ui/RegisterFrom";
import styles from "./RegisterPage.module.css";
import { Link } from "react-router-dom";

export default function RegisterPage() {
  return (
    <div className={styles.registerPage}>
      <h2>Реєстрація</h2>
      <RegisterForm />
      <Link to="/login" className={styles.link}>
        Вже маєте аккаунт? Увійти
      </Link>
    </div>
  );
}
