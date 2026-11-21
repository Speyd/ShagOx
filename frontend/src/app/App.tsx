import { useEffect } from "react";
import Router from "./routing";
import "./styles/index.css";

import { Toaster } from "sonner";
import { useMe } from "@/features/auth/model/hooks/useMe";
import { useAuthStore } from "@/features/auth";
import ScrollToTop from "@/shared/lib/ScrollToTop";

export default function App() {
  const setUser = useAuthStore((state) => state.setUser);
  const { data: user, isLoading, isError } = useMe();

  useEffect(() => {
    if (isLoading) return;
    setUser(isError ? null : (user ?? null));
  }, [user, isLoading, isError, setUser]);

  return (
    <>
      <ScrollToTop />
      <Router />
      <Toaster />
    </>
  );
}
