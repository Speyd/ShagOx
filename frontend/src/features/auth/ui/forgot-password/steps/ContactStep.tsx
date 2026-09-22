import { ArrowRight, Mail, MoveLeft } from "lucide-react";
import { useForm } from "react-hook-form";
import { Link } from "react-router-dom";
import { zodResolver } from "@hookform/resolvers/zod";
import { Text } from "@mantine/core";
import { toast } from "sonner";
import Input from "@/shared/ui/input";
import Button from "@/shared/ui/button";
import { useGetByContact } from "@/entities/user/model/hooks/useGetByContact";
import styles from "../ForgotPasswordForm.module.css";
import {
  resetPasswordSchema,
  type ResetPasswordFormData,
} from "@/features/auth/model/schemas/resetPasswordSchema";

type Props = {
  onSuccess: (userId: number) => void;
};

export default function ContactStep({ onSuccess }: Props) {
  const {
    register,
    trigger,
    getValues,
    formState: { errors },
  } = useForm<ResetPasswordFormData>({
    resolver: zodResolver(resetPasswordSchema),
    mode: "onChange",
  });

  const getByContact = useGetByContact(getValues("contact") || "");

  const handleSubmit = async () => {
    const isValid = await trigger("contact");

    if (!isValid) return;

    try {
      const { data } = await getByContact.refetch();

      if (!data) {
        toast.error("Користувача не знайдено.");
        return;
      }

      toast.success("Користувача знайдено.");
      onSuccess(data.id);
    } catch {
      toast.error("Помилка під час пошуку користувача.");
    }
  };

  return (
    <>
      <div className={styles.description}>
        <Text fz={15} c="var(--text-base)">
          Введіть електронну пошту чи телефон. Ми надішлемо вам код
          підтвердження.
        </Text>
      </div>

      <form
        className={styles.form}
        onSubmit={(event) => {
          event.preventDefault();
          handleSubmit();
        }}
      >
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
        </div>

        <Button type="submit" className={styles.button}>
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
