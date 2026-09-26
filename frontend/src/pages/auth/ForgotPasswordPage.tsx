import styles from "./ForgotPasswordPage.module.css";
import heroAuthBg from "@/shared/assets/images/hero-auth.png";
import ForgotPasswordForm from "@/features/auth/ui/forgot-password/ForgotPasswordForm";

export default function ForgotPasswordPage() {
  return (
    <div
      className={styles.forgotPasswordPage}
      style={{ backgroundImage: `url(${heroAuthBg})` }}
    >
      <ForgotPasswordForm />
    </div>
  );
}
