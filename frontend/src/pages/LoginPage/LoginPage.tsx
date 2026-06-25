import LoginForm from "@/features/auth/ui/LoginForm";
import styles from "./LoginPage.module.css";
import { Link } from "react-router-dom";

export default function LoginPage() {
  return (
    <div className={styles.loginPage}>
      <h2>LoginPage</h2>
      <LoginForm />
      <Link to="/register">Don't have an account</Link>
    </div>
  );
}
