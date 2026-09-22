import { useLocation } from "react-router-dom";
import VerifyForm from "@/features/auth/ui/verify/VerifyForm";
import styles from "./VerifyPage.module.css";
import heroAuthBg from "@/shared/assets/images/hero-auth.png";

type LocationState = {
  userId?: number;
  phone?: string;
  email?: string;
  password?: string;
};

export default function VerifyPage() {
  const location = useLocation();
  const state = location.state as LocationState | null;

  const userId = state?.userId;

  return (
    <div
      className={styles.verifyPage}
      style={{ backgroundImage: `url(${heroAuthBg})` }}
    >
      <VerifyForm
        userId={userId}
        email={state?.email}
        phone={state?.phone}
        password={state?.password}
      />
    </div>
  );
}
