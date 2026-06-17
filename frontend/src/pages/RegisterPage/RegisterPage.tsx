import RegisterForm from "@/features/auth/register-form";
import styles from "./RegisterPage.module.css";
import { Link } from "react-router-dom";

export default function RegisterPage() {
  return (
    <div className={styles.registerPage}>
      <h2>RegisterPage</h2>
      <RegisterForm />
      <Link to="/login">Allready have an account</Link>
    </div>
  );
}
