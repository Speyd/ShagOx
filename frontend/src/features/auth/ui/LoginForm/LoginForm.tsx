import Input from "@/shared/ui/Input";
import styles from "./LoginForm.module.css";
import Button from "@/shared/ui/Button";
import { login } from "../../api/login";
import { useState } from "react";

export default function LoginForm() {
  const [emailOrPhone, setEmailOrPhone] = useState("");
  const [password, setPassword] = useState("");

  const handleSubmit = async () => {
    const result = await login({
      emailOrPhone,
      password,
    });

    console.log(result);
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
      <Button>Login</Button>
    </form>
  );
}
