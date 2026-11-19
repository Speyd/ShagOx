import { useState } from "react";
import { Controller, useFormContext, useWatch } from "react-hook-form";
import { Text } from "@mantine/core";
import { useDebouncedValue } from "@mantine/hooks";
import { FormField, TextField } from "@/shared/ui/form";
import {
  useDeliveryCities,
  useDeliveryPoints,
  useDeliveryStreets,
} from "@/entities/delivery/model/hooks";
import {
  NOVA_POSHTA_CODE,
  type DeliveryCity,
  type DeliveryPoint,
  type DeliveryStreet,
} from "@/entities/delivery/model/types";
import type {
  CheckoutFormData,
  DeliveryMethod,
} from "@/features/checkout/model/checkoutSchema";
import SearchSelect from "./SearchSelect";
import styles from "./CheckoutDetailsForm.module.css";

type DeliveryOption = {
  value: DeliveryMethod;
  title: string;
  description: string;
};

const NOVA_POSHTA_OPTIONS: DeliveryOption[] = [
  {
    value: "branch",
    title: "У відділення Нової пошти",
    description: "Вартість — за тарифами перевізника",
  },
  {
    value: "locker",
    title: "У поштомат Нової пошти",
    description: "Вартість — за тарифами перевізника",
  },
  {
    value: "courier",
    title: "Кур'єр Нової пошти",
    description: "Доставка до дверей за вашою адресою",
  },
];

const PICKUP_OPTION: DeliveryOption = {
  value: "pickup",
  title: "Самовивіз з магазину",
  description: "Безкоштовно",
};

const getCityLabel = (city: DeliveryCity) =>
  city.area ? `${city.name} (${city.area})` : city.name;

type Props = {
  hasNovaPoshta: boolean;
};

export default function DeliverySection({ hasNovaPoshta }: Props) {
  const {
    control,
    register,
    setValue,
    formState: { errors },
  } = useFormContext<CheckoutFormData>();

  const deliveryMethod = useWatch({ control, name: "deliveryMethod" });
  const city = useWatch({ control, name: "city" });

  const [citySearch, setCitySearch] = useState("");
  const [pointSearch, setPointSearch] = useState("");
  const [streetSearch, setStreetSearch] = useState("");

  const [debouncedCity] = useDebouncedValue(citySearch, 400);
  const [debouncedPoint] = useDebouncedValue(pointSearch, 400);
  const [debouncedStreet] = useDebouncedValue(streetSearch, 400);

  const cities = useDeliveryCities(NOVA_POSHTA_CODE, debouncedCity);
  const points = useDeliveryPoints(
    NOVA_POSHTA_CODE,
    city?.externalId,
    deliveryMethod === "locker" ? "ParcelLocker" : "Branch",
    debouncedPoint,
  );
  const streets = useDeliveryStreets(
    NOVA_POSHTA_CODE,
    city?.externalId,
    debouncedStreet,
  );

  const options = hasNovaPoshta
    ? [...NOVA_POSHTA_OPTIONS, PICKUP_OPTION]
    : [PICKUP_OPTION];

  const changeMethod = (method: DeliveryMethod) => {
    setValue("deliveryMethod", method);
    setValue("point", null);
  };

  const changeCity = (selected: DeliveryCity | null) => {
    setValue("city", selected, { shouldValidate: true });
    setValue("point", null);
    setValue("street", null);
  };

  return (
    <div className={styles.section}>
      <Text fw={600} fz={24} className={styles.title}>
        Доставка
      </Text>

      <div className={styles.options}>
        {options.map((option) => (
          <label
            key={option.value}
            className={`${styles.option} ${
              deliveryMethod === option.value ? styles.selected : ""
            }`}
          >
            <input
              type="radio"
              name="deliveryMethod"
              checked={deliveryMethod === option.value}
              onChange={() => changeMethod(option.value)}
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

      {!hasNovaPoshta && (
        <Text fz={13} c="dimmed">
          Доставка Новою поштою зараз недоступна.
        </Text>
      )}

      {deliveryMethod !== "pickup" && (
        <SearchSelect<DeliveryCity>
          label="Місто"
          placeholder="Почніть вводити назву міста"
          items={cities.data ?? []}
          value={city}
          getKey={(item) => item.externalId}
          getLabel={getCityLabel}
          onChange={changeCity}
          onSearchChange={setCitySearch}
          loading={cities.isFetching}
          error={errors.city?.message}
          nothingFoundMessage={
            citySearch.length < 2 ? "Введіть щонайменше 2 літери" : undefined
          }
        />
      )}

      {(deliveryMethod === "branch" || deliveryMethod === "locker") && (
        <Controller
          name="point"
          control={control}
          render={({ field, fieldState }) => (
            <SearchSelect<DeliveryPoint>
              label={deliveryMethod === "locker" ? "Поштомат" : "Відділення"}
              placeholder={
                city ? "Оберіть зі списку або почніть вводити" : "Спочатку оберіть місто"
              }
              items={points.data ?? []}
              value={field.value}
              getKey={(item) => item.externalId}
              getLabel={(item) => item.name}
              onChange={field.onChange}
              onSearchChange={setPointSearch}
              loading={points.isFetching}
              disabled={!city}
              error={fieldState.error?.message}
            />
          )}
        />
      )}

      {deliveryMethod === "courier" && (
        <>
          <Controller
            name="street"
            control={control}
            render={({ field, fieldState }) => (
              <SearchSelect<DeliveryStreet>
                label="Вулиця"
                placeholder={city ? "Почніть вводити назву вулиці" : "Спочатку оберіть місто"}
                items={streets.data ?? []}
                value={field.value}
                getKey={(item) => item.externalId}
                getLabel={(item) => item.name}
                onChange={field.onChange}
                onSearchChange={setStreetSearch}
                loading={streets.isFetching}
                disabled={!city}
                error={fieldState.error?.message}
              />
            )}
          />

          <div className={styles.fields}>
            <FormField
              label="Будинок"
              htmlFor="building"
              error={errors.building?.message}
            >
              <TextField
                id="building"
                invalid={!!errors.building}
                {...register("building")}
              />
            </FormField>

            <FormField label="Квартира" htmlFor="flat" hint="Необов'язково">
              <TextField id="flat" {...register("flat")} />
            </FormField>
          </div>
        </>
      )}
    </div>
  );
}
