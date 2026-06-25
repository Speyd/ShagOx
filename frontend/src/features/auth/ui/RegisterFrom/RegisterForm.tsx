import Input from "@/shared/ui/Input";
import styles from "./RegisterForm.module.css";
import Button from "@/shared/ui/Button";
import { register } from "../../api/register";
import { useState } from "react";

export default function RegisterForm() {
  const [emailOrPhone, setEmailOrPhone] = useState("");
  const [password, setPassword] = useState("");

  const handleSubmit = async () => {
    const result = await register({ emailOrPhone, password });

    console.log(result);
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

      <Button>Register</Button>
    </form>
  );
}
