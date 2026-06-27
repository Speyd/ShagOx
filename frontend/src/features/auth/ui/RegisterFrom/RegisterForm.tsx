import Input from "@/shared/ui/Input";
import styles from "./RegisterForm.module.css";
import Button from "@/shared/ui/Button";
import { useState } from "react";
import { useRegister } from "../../hooks/useRegister";

export default function RegisterForm() {
  const [emailOrPhone, setEmailOrPhone] = useState("");
  const [password, setPassword] = useState("");

  const registerMutation = useRegister();

  const handleSubmit = async () => {
    registerMutation.mutate({ emailOrPhone, password });
  };

  return (
    <form
      className={styles.registerForm}
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
        type="password"
        value={password}
        onChange={(e) => setPassword(e.target.value)}
      />

      <Button type="submit">
        {registerMutation.isPending ? "Loading..." : "Register"}
      </Button>
    </form>
  );
}
