import { Navigate } from "react-router-dom";
import { useMe } from "@/features/auth/hooks/useMe";
import type { ReactNode } from "react";

interface PublicRouteProps {
  children: ReactNode;
}

export function PublicRoute({ children }: PublicRouteProps) {
  const { data: user, isLoading } = useMe();

  if (isLoading) return <div>Loading...</div>;

  if (user) return <Navigate to="/" replace />;

  return children;
}
