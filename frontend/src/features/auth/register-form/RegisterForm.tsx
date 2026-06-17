import Input from "@/shared/ui/Input";
import styles from "./RegisterForm.module.css";
import Button from "@/shared/ui/Button";

export default function RegisterForm() {
  return (
    <div className={styles.registerForm}>
      <Input placeholder="Usename" />
      <Input placeholder="Email" />
      <Input placeholder="Password" />
      <Button>Register</Button>
    </div>
  );
}
