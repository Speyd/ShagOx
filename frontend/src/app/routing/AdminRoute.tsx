import { Navigate } from "react-router-dom";
import type { ReactNode } from "react";
import { useAuthStore } from "@/features/auth/store/useAuthStore";
import { hasRole } from "@/shared/lib/auth";

interface AdminRouteProps {
  children: ReactNode;
}

export function AdminRoute({ children }: AdminRouteProps) {
  const { user, isInitialized } = useAuthStore();

  if (!isInitialized) return null;

  if (!user) {
    return <Navigate to="/login" replace />;
  }

  const isAdmin = hasRole(user, "Admin");

  if (!isAdmin) {
    return <Navigate to="/" replace />;
  }

  return children;
}
