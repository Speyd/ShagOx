import Input from "@/shared/ui/Input";
import styles from "./LoginForm.module.css";
import Button from "@/shared/ui/Button";

export default function LoginForm() {
  return (
    <div className={styles.loginForm}>
      <Input placeholder="Email" />
      <Input placeholder="Password" />
      <Button>Login</Button>
    </div>
  );
}
