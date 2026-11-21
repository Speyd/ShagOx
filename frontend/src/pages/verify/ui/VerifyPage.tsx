import { Navigate, useLocation } from "react-router-dom";
import VerifyForm from "@/features/auth/ui/verify/VerifyForm";
import styles from "./VerifyPage.module.css";
import heroAuthBg from "@/shared/assets/images/hero-auth.png";

type LocationState = {
  userId?: number;
  phone?: string;
  email?: string;
  userName?: string;
  password?: string;
};

export default function VerifyPage() {
  const location = useLocation();
  const state = location.state as LocationState | null;

  if (state?.userId == null) {
    return <Navigate to="/authentication" replace />;
  }

  return (
    <div
      className={styles.verifyPage}
      style={{ backgroundImage: `url(${heroAuthBg})` }}
    >
      <VerifyForm
        userId={state.userId}
        email={state.email}
        phone={state.phone}
        userName={state.userName}
        password={state.password}
      />
    </div>
  );
}
