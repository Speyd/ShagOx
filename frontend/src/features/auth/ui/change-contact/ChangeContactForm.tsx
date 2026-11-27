import { useMemo, useState } from "react";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { useQueryClient } from "@tanstack/react-query";
import { AtSign, Phone } from "lucide-react";
import { toast } from "sonner";
import clsx from "clsx";
import Button from "@/shared/ui/buttons/button";
import { getErrorMessage } from "@/shared/api/getErrorMessage";
import { useChangeContact } from "../../model/hooks/useChangeContact";
import { useVerify } from "../../model/hooks/useVerify";
import {
  createChangeContactSchema,
  type ChangeContactFormData,
  type ChangeContactFormInput,
  type ContactKind,
} from "../../model/schemas/changeContactSchema";
import AuthTextField from "../fields/AuthTextField";
import ServerError from "../fields/ServerError";
import CodeConfirmation from "../code-input/CodeConfirmation";
import styles from "./ChangeContactForm.module.css";

type Props = {
  kind: ContactKind;
  currentValue?: string | null;
  onCancel?: () => void;
  onSuccess?: () => void;
  className?: string;
};

type Step = "form" | "code";

const COPY = {
  email: {
    label: "Нова електронна пошта",
    placeholder: "example@gmail.com",
    sendError: "Не вдалося змінити пошту.",
    success: "Пошту успішно змінено!",
    sentTo: "Ми надіслали код підтвердження на",
  },
  phone: {
    label: "Новий номер телефону",
    placeholder: "+380XXXXXXXXX",
    sendError: "Не вдалося змінити номер телефону.",
    success: "Номер телефону успішно змінено!",
    sentTo: "Ми надіслали SMS з кодом підтвердження на",
  },
} as const;


export default function ChangeContactForm({
  kind,
  currentValue,
  onCancel,
  onSuccess,
  className,
}: Props) {
  const copy = COPY[kind];
  const [step, setStep] = useState<Step>("form");
  const [pendingValue, setPendingValue] = useState("");
  const [serverError, setServerError] = useState("");

  const queryClient = useQueryClient();
  const changeContact = useChangeContact(kind);
  const verify = useVerify(copy.success);

  const schema = useMemo(
    () => createChangeContactSchema(kind, currentValue),
    [kind, currentValue],
  );

  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm<ChangeContactFormInput, unknown, ChangeContactFormData>({
    resolver: zodResolver(schema),
    defaultValues: { value: "" },
  });

  const resetState = () => {
    setStep("form");
    setPendingValue("");
    setServerError("");
    reset();
  };

  const handleCancel = () => {
    resetState();
    onCancel?.();
  };

  const sendCode = async ({ value }: ChangeContactFormData) => {
    setServerError("");

    try {
      await changeContact.mutateAsync(value);
      setPendingValue(value);
      setStep("code");
      toast.success("Код підтвердження надіслано.");
    } catch (error) {
      setServerError(getErrorMessage(error, copy.sendError));
    }
  };

  const resendCode = async () => {
    try {
      await changeContact.mutateAsync(pendingValue);
      toast.success("Новий код надіслано.");
    } catch (error) {
      toast.error(getErrorMessage(error, "Не вдалося надіслати код."));
      throw error;
    }
  };

  const confirmCode = async (code: string) => {
    try {
      await verify.mutateAsync({ code });
      await queryClient.invalidateQueries({ queryKey: ["me"] });
      resetState();
      onSuccess?.();
      return true;
    } catch {
      return false;
    }
  };

  if (step === "code") {
    return (
      <div className={className}>
        <CodeConfirmation
          description={
            <>
              {copy.sentTo} <strong>{pendingValue}</strong>. Введіть його, щоб
              зберегти зміни.
            </>
          }
          submitLabel="Підтвердити"
          onSubmit={confirmCode}
          onResend={resendCode}
          isSubmitting={verify.isPending}
          isResending={changeContact.isPending}
          secondaryAction={
            <Button
              type="button"
              variant="ghost"
              onClick={() => setStep("form")}
              disabled={verify.isPending}
            >
              Назад
            </Button>
          }
        />
      </div>
    );
  }

  return (
    <form
      className={clsx(styles.form, className)}
      onSubmit={handleSubmit(sendCode)}
      noValidate
    >
      <AuthTextField
        label={copy.label}
        placeholder={copy.placeholder}
        icon={kind === "email" ? AtSign : Phone}
        type={kind === "email" ? "email" : "tel"}
        autoComplete={kind === "email" ? "email" : "tel"}
        inputMode={kind === "email" ? "email" : "tel"}
        autoFocus
        error={errors.value?.message}
        {...register("value")}
      />

      <ServerError message={serverError} />

      <div className={styles.actions}>
        {onCancel && (
          <Button type="button" variant="outline" onClick={handleCancel}>
            Скасувати
          </Button>
        )}
        <Button type="submit" loading={changeContact.isPending}>
          Надіслати код
        </Button>
      </div>
    </form>
  );
}
