import { Select, Text, TextInput } from "@mantine/core";
import { useForm } from "react-hook-form";
import { toast } from "sonner";
import { useNavigate } from "react-router-dom";
import { useAuthStore } from "@/features/auth";
import styles from "./CheckoutDetailsForm.module.css";
import Button from "@/shared/ui/button";

type CheckoutFormData = {
  firstName: string;
  lastName: string;
  email: string;
  phone: string;
  city: string;
  address: string;
  branch: string;
  delivery: string;
};

export default function CheckoutDetailsForm() {
  const navigate = useNavigate();
  const user = useAuthStore((state) => state.user);
  const {
    register,
    handleSubmit,
    setValue,
    watch,
    formState: { errors },
  } = useForm<CheckoutFormData>({
    defaultValues: {
      firstName: user?.firstName ?? "",
      lastName: user?.lastName ?? "",
      email: user?.email ?? "",
      phone: user?.phone ?? "",
      city: user?.city?.name ?? "",
      address: "",
      branch: "",
      delivery: "nova-poshta",
    },
  });

  const delivery = watch("delivery");
  const onSubmit = (data: CheckoutFormData) => {
    sessionStorage.setItem("checkoutDetails", JSON.stringify(data));
    toast.success("Дані доставки збережено!");
    navigate("/payment");
  };

  return (
    <form
      className={styles.checkoutDetailsForm}
      onSubmit={handleSubmit(onSubmit)}
    >
      <div className={styles.section}>
        <Text fw={600} fz={24} className={styles.title}>
          Контактні дані
        </Text>

        <div className={styles.fields}>
          <TextInput
            label="Ім'я"
            placeholder="Введіть ім'я"
            error={errors.firstName?.message}
            {...register("firstName", { required: "Введіть ім'я" })}
          />

          <TextInput
            label="Прізвище"
            placeholder="Введіть прізвище"
            error={errors.lastName?.message}
            {...register("lastName", { required: "Введіть прізвище" })}
          />

          <TextInput
            label="Електронна пошта"
            placeholder="example@email.com"
            type="email"
            error={errors.email?.message}
            {...register("email", {
              required: "Введіть електронну пошту",
              pattern: {
                value: /^\S+@\S+\.\S+$/,
                message: "Некоректний email",
              },
            })}
          />

          <TextInput
            label="Номер телефону"
            placeholder="+380"
            error={errors.phone?.message}
            {...register("phone", { required: "Введіть номер телефону" })}
          />
        </div>
      </div>

      <div className={styles.section}>
        <Text fw={600} fz={24} className={styles.title}>
          Деталі доставки
        </Text>

        <TextInput
          label="Місто"
          placeholder="Введіть місто"
          error={errors.city?.message}
          {...register("city", { required: "Введіть місто" })}
        />

        <Select
          label="Спосіб доставки"
          value={delivery}
          onChange={(value) => setValue("delivery", value ?? "")}
          data={[
            { value: "nova-poshta", label: "Нова пошта" },
            { value: "ukr-poshta", label: "Укрпошта" },
            { value: "pickup", label: "Самовивіз" },
          ]}
          allowDeselect={false}
        />

        <TextInput
          label="Адреса"
          placeholder="Введіть адресу доставки"
          error={errors.address?.message}
          {...register("address", { required: "Введіть адресу" })}
        />

        <TextInput
          label="Відділення або поштомат"
          placeholder="Номер відділення"
          error={errors.branch?.message}
          {...register("branch", {
            required: "Введіть відділення або поштомат",
          })}
        />
      </div>

      <Button type="submit" className={styles.submitButton}>
        Перейти до оплати
      </Button>
    </form>
  );
}
