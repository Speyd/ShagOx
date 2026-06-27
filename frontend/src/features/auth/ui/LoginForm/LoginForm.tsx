import Input from "@/shared/ui/Input";
import styles from "./LoginForm.module.css";
import Button from "@/shared/ui/Button";
import { useState } from "react";
import { useLogin } from "../../hooks/useLogin";

export default function LoginForm() {
  const [emailOrPhone, setEmailOrPhone] = useState("");
  const [password, setPassword] = useState("");
  const loginMutation = useLogin();

  const handleSubmit = async () => {
    loginMutation.mutate({ emailOrPhone, password });
  };

  return (
    <form
      className={styles.loginForm}
      onSubmit={(e) => {
        e.preventDefault();
        handleSubmit();
      }}
    >
      <Input
        placeholder="Email"
        value={emailOrPhone}
        onChange={(e) => setEmailOrPhone(e.target.value)}
      />
      <Input
        placeholder="Password"
        value={password}
        onChange={(e) => setPassword(e.target.value)}
        type="password"
      />
      <Button type="submit">
        {loginMutation.isPending ? "Loading..." : "Login"}
      </Button>
    </form>
  );
}
