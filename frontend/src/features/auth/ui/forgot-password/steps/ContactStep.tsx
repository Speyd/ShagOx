import { ArrowRight, Mail, MoveLeft } from "lucide-react";
import { useState } from "react";
import { useForm } from "react-hook-form";
import { Link } from "react-router-dom";
import { zodResolver } from "@hookform/resolvers/zod";
import { Text } from "@mantine/core";
import Input from "@/shared/ui/data-display/inputs/input";
import Button from "@/shared/ui/buttons/button";
import { getByContanct } from "@/entities/user/api/api";
import styles from "../ForgotPasswordForm.module.css";
import {
  contactSchema,
  type ContactFormData,
} from "@/features/auth/model/schemas/resetPasswordSchema";

type Props = {
  onSuccess: (userId: number, contact: string) => void;
};

export default function ContactStep({ onSuccess }: Props) {
  const [serverError, setServerError] = useState("");
  const [isSearching, setIsSearching] = useState(false);

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<ContactFormData>({
    resolver: zodResolver(contactSchema),
    mode: "onChange",
  });

  const onSubmit = async ({ contact }: ContactFormData) => {
    setServerError("");
    setIsSearching(true);

    try {
      const user = await getByContanct(contact);
      onSuccess(user.id, contact.trim());
    } catch {
      setServerError("Користувача з такою поштою або телефоном не знайдено.");
    } finally {
      setIsSearching(false);
    }
  };

  return (
    <>
      <div className={styles.description}>
        <Text fz={15} c="var(--text-base)">
          Введіть електронну пошту чи телефон, які ви вказували під час
          реєстрації.
        </Text>
      </div>

      <form className={styles.form} onSubmit={handleSubmit(onSubmit)}>
        <div className={styles.inputWrapper}>
          <div className={styles.inputBlock}>
            <Mail className={styles.icon} />
            <Input
              placeholder="Електронна пошта чи телефон"
              {...register("contact")}
            />
          </div>

          {errors.contact && (
            <Text c="red" fz={12} mt={4}>
              {errors.contact.message}
            </Text>
          )}

          {serverError && (
            <Text c="red" fz={12} mt={4}>
              {serverError}
            </Text>
          )}
        </div>

        <Button type="submit" loading={isSearching}>
          <div className={styles.loginButton}>
            <Text fw={700} fz={15}>
              Далі
            </Text>
            <ArrowRight size={20} />
          </div>
        </Button>

        <div className={styles.guestWrapper}>
          <Link to="/authentication" className={styles.guestLink}>
            <MoveLeft size={20} />
            <Text fw={700} fz={12}>
              Повернутись до входу
            </Text>
          </Link>
        </div>
      </form>
    </>
  );
}
