import { Navigate, Outlet } from "react-router-dom";
import { hasRole } from "@/shared/lib/auth";
import { useAuthStore } from "@/features/auth";

export function AdminRoute() {
  const { user, isInitialized } = useAuthStore();

  if (!isInitialized) return null;

  if (!user) {
    return <Navigate to="/authentication" replace />;
  }

  const isAdmin = hasRole(user, "Admin");

  if (!isAdmin) {
    return <Navigate to="/" replace />;
  }

  return <Outlet />;
}
