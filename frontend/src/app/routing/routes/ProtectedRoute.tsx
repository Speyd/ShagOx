import { Navigate, Outlet } from "react-router-dom";
import { useAuthStore } from "@/features/auth";

export function ProtectedRoute() {
  const { user, isInitialized } = useAuthStore();

  if (!isInitialized) return null;

  if (!user) return <Navigate to="/authentication" replace />;

  return <Outlet />;
}
