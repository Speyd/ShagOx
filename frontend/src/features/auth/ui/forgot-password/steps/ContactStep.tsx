import { ArrowRight, Mail, MoveLeft } from "lucide-react";
import { useState } from "react";
import { useForm } from "react-hook-form";
import { Link } from "react-router-dom";
import { zodResolver } from "@hookform/resolvers/zod";
import { Text } from "@mantine/core";
import Button from "@/shared/ui/buttons/button";
import AuthTextField from "../../fields/AuthTextField";
import ServerError from "../../fields/ServerError";
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
        <AuthTextField
          icon={Mail}
          placeholder="Електронна пошта чи телефон"
          autoComplete="username"
          error={errors.contact?.message}
          {...register("contact")}
        />

        <ServerError message={serverError} />

        <Button
          type="submit"
          loading={isSearching}
          rightSection={ArrowRight}
          iconProps={{ size: 20 }}
        >
          Далі
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
