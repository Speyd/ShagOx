import { Navigate, Outlet } from "react-router-dom";
import { useAuthStore } from "@/features/auth";

export function PublicRoute() {
  const { user, isInitialized } = useAuthStore();

  if (!isInitialized) return null;

  if (user) return <Navigate to="/" replace />;

  return <Outlet />;
}
