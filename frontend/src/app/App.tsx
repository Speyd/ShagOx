import { useEffect } from "react";
import Router from "./routing";
import "./styles/index.css";
import { useAuthStore } from "@/features/auth/store/useAuthStore";
import { Toaster } from "sonner";
import { useMe } from "@/features/auth/model/hooks/useMe";

export default function App() {
  const setUser = useAuthStore((state) => state.setUser);

  const { data: user, isLoading, isError } = useMe();

  useEffect(() => {
    if (isLoading) return;

    setUser(isError ? null : (user ?? null));
  }, [user, isLoading, isError, setUser]);

  if (isLoading) {
    return (
      <div
        style={{
          display: "flex",
          justifyContent: "center",
          alignItems: "center",
          height: "100vh",
        }}
      >
        <h3>Ініціалізація додатка...</h3>
      </div>
    );
  }

  return (
    <>
      <Router /> <Toaster />
    </>
  );
}
