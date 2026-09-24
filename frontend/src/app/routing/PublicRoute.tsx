import { Navigate } from "react-router-dom";
import type { ReactNode } from "react";
import { useAuthStore } from "@/features/auth";


interface PublicRouteProps {
  children: ReactNode;
}

export function PublicRoute({ children }: PublicRouteProps) {
  const { user, isInitialized } = useAuthStore();

  if (!isInitialized) return null;
  
  if (user) return <Navigate to="/" replace />;

  return children;
}
