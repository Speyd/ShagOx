import { useEffect } from "react";
import { FormProvider, useForm, useWatch } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { Text } from "@mantine/core";
import { useNavigate } from "react-router-dom";
import { toast } from "sonner";
import { useAuthStore } from "@/features/auth";
import Button from "@/shared/ui/buttons/button";
import { FormField, TextAreaField, TextField } from "@/shared/ui/form";
import { getErrorMessage } from "@/shared/api/getErrorMessage";
import { useGetCurrencies } from "@/entities/currency/model/hooks/useGetCurrencies";
import { useDeliveryProviders } from "@/entities/delivery/model/hooks";
import { NOVA_POSHTA_CODE } from "@/entities/delivery/model/types";
import type { BasketItem } from "@/features/basket/model/types";
import {
  checkoutSchema,
  type CheckoutFormData,
} from "@/features/checkout/model/checkoutSchema";
import { toOrderRequest } from "@/features/checkout/lib/toOrderRequest";
import { useCreateOrder } from "@/features/checkout/model/useCreateOrder";
import DeliverySection from "./DeliverySection";
import styles from "./CheckoutDetailsForm.module.css";

const PAYMENT_OPTIONS = [
  {
    value: "OnDelivery",
    title: "При отриманні",
    description: "Оплата при отриманні",
  },
  {
    value: "Online",
    title: "Карткою",
    description: "Повна оплата карткою",
  },
] as const;

type Props = {
  items: BasketItem[];
};

export default function CheckoutDetailsForm({ items }: Props) {
  const navigate = useNavigate();
  const user = useAuthStore((state) => state.user);

  const { data: providers, isLoading: providersLoading } =
    useDeliveryProviders();
  const { data: currencies = [] } = useGetCurrencies();
  const createOrder = useCreateOrder();

  const novaPoshta = providers?.find((p) => p.code === NOVA_POSHTA_CODE);
  const currencyId =
    user?.currency?.id ?? currencies.find((c) => c.code === "UAH")?.id;

  const form = useForm<CheckoutFormData>({
    resolver: zodResolver(checkoutSchema),
    defaultValues: {
      firstName: user?.firstName ?? "",
      lastName: user?.lastName ?? "",
      phone: user?.phone ?? "",
      email: user?.email ?? "",
      deliveryMethod: "branch",
      city: null,
      point: null,
      street: null,
      building: "",
      flat: "",
      paymentMethod: "OnDelivery",
      comment: "",
    },
  });

  const {
    register,
    handleSubmit,
    control,
    setValue,
    formState: { errors },
  } = form;

  const paymentMethod = useWatch({ control, name: "paymentMethod" });

  useEffect(() => {
    if (!providersLoading && !novaPoshta) {
      setValue("deliveryMethod", "pickup");
    }
  }, [providersLoading, novaPoshta, setValue]);

  const onSubmit = async (data: CheckoutFormData) => {
    if (!currencyId) {
      toast.error("Не вдалося визначити валюту замовлення.");
      return;
    }

    try {
      const order = await createOrder.mutateAsync({
        request: toOrderRequest(
          data,
          items,
          novaPoshta?.id ?? null,
          currencyId,
        ),
        basketItemIds: items.map((item) => item.id),
      });

      if (order.paymentUrl) {
        window.location.replace(order.paymentUrl);
        return;
      }

      navigate(`/orders/${order.id}`, {
        replace: true,
        state: { justCreated: true },
      });
    } catch (error) {
      toast.error(getErrorMessage(error, "Не вдалося оформити замовлення."));
    }
  };

  return (
    <FormProvider {...form}>
      <form
        className={styles.checkoutDetailsForm}
        onSubmit={handleSubmit(onSubmit)}
      >
        <div className={styles.section}>
          <Text fw={600} fz={24} className={styles.title}>
            Отримувач
          </Text>

          <div className={styles.fields}>
            <FormField
              label="Ім'я"
              htmlFor="firstName"
              required
              error={errors.firstName?.message}
            >
              <TextField
                id="firstName"
                invalid={!!errors.firstName}
                {...register("firstName")}
              />
            </FormField>

            <FormField
              label="Прізвище"
              htmlFor="lastName"
              required
              error={errors.lastName?.message}
            >
              <TextField
                id="lastName"
                invalid={!!errors.lastName}
                {...register("lastName")}
              />
            </FormField>

            <FormField
              label="Телефон"
              htmlFor="phone"
              required
              error={errors.phone?.message}
            >
              <TextField
                id="phone"
                type="tel"
                placeholder="+380"
                invalid={!!errors.phone}
                {...register("phone")}
              />
            </FormField>

            <FormField
              label="Електронна пошта"
              htmlFor="email"
              required
              error={errors.email?.message}
            >
              <TextField
                id="email"
                type="email"
                placeholder="example@email.com"
                invalid={!!errors.email}
                {...register("email")}
              />
            </FormField>
          </div>
        </div>

        <DeliverySection hasNovaPoshta={!!novaPoshta} />

        <div className={styles.section}>
          <Text fw={600} fz={24} className={styles.title}>
            Оплата
          </Text>

          <div className={styles.options}>
            {PAYMENT_OPTIONS.map((option) => (
              <label
                key={option.value}
                className={`${styles.option} ${
                  paymentMethod === option.value ? styles.selected : ""
                }`}
              >
                <input
                  type="radio"
                  value={option.value}
                  {...register("paymentMethod")}
                />
                <span className={styles.optionContent}>
                  <span className={styles.optionTitle}>{option.title}</span>
                  <span className={styles.optionDescription}>
                    {option.description}
                  </span>
                </span>
              </label>
            ))}
          </div>
        </div>

        <FormField
          label="Коментар до замовлення"
          htmlFor="comment"
          error={errors.comment?.message}
        >
          <TextAreaField id="comment" rows={3} {...register("comment")} />
        </FormField>

        <div className={styles.submitBlock}>
          <Button
            type="submit"
            className={styles.submitButton}
            loading={createOrder.isPending}
          >
            Підтвердити замовлення
          </Button>

          <Text fz={12} c="dimmed">
            Натискаючи кнопку, ви погоджуєтесь з умовами доставки та оплати.
            Вартість доставки розраховується за тарифами перевізника.
          </Text>
        </div>
      </form>
    </FormProvider>
  );
}
