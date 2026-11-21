import { useState } from "react";
import { Modal, Text, TextInput } from "@mantine/core";
import { CircleCheck, CircleX, PackageCheck, Truck } from "lucide-react";
import Button from "@/shared/ui/buttons/button";
import ConfirmModal from "@/shared/ui/feedback/confirm-modal";
import {
  getAvailableActions,
  isPaidOnline,
  type OrderAction,
  type OrderRole,
} from "@/entities/order/lib/actions";
import type { Order } from "@/entities/order/model/types";
import { useOrderAction } from "../model/useOrderAction";
import styles from "./OrderActions.module.css";

type OrderActionsProps = {
  order: Order;
  role: OrderRole | null;
};

/** Кнопки дій над замовленням для покупця або продавця. */
export function OrderActions({ order, role }: OrderActionsProps) {
  const actions = getAvailableActions(order, role);
  const [dialog, setDialog] = useState<OrderAction | null>(null);

  const cancel = useOrderAction(order.id, "cancel");
  const confirm = useOrderAction(order.id, "confirm");
  const reject = useOrderAction(order.id, "reject");
  const ship = useOrderAction(order.id, "ship");
  const complete = useOrderAction(order.id, "complete");

  const isPickup = order.deliveryType === "Pickup";
  const paidOnline = isPaidOnline(order);
  const close = () => setDialog(null);

  const runAndClose = (mutation: typeof cancel, vars = {}) =>
    mutation.mutate(vars, { onSuccess: close });

  const awaitsOnlinePayment =
    role === "seller" &&
    order.orderStatus === "Pending" &&
    order.paymentMethod === "Online" &&
    order.paymentStatus !== "Paid";

  if (actions.length === 0) {
    return awaitsOnlinePayment ? (
      <Text c="dimmed" fz={14}>
        Покупець ще не оплатив замовлення — підтвердити його можна після оплати.
      </Text>
    ) : null;
  }

  return (
    <>
      <div className={styles.actions}>
        {actions.includes("confirm") && (
          <Button
            leftSection={CircleCheck}
            loading={confirm.isPending}
            onClick={() => confirm.mutate({})}
          >
            Підтвердити замовлення
          </Button>
        )}

        {actions.includes("ship") && (
          <Button leftSection={Truck} onClick={() => setDialog("ship")}>
            {isPickup ? "Готове до видачі" : "Відправити"}
          </Button>
        )}

        {actions.includes("complete") && (
          <Button
            leftSection={PackageCheck}
            onClick={() => setDialog("complete")}
          >
            {role === "buyer" ? "Я отримав замовлення" : "Замовлення отримано"}
          </Button>
        )}

        {actions.includes("reject") && (
          <Button
            variant="outline"
            leftSection={CircleX}
            onClick={() => setDialog("reject")}
          >
            Відхилити
          </Button>
        )}

        {actions.includes("cancel") && (
          <Button
            variant="outline"
            leftSection={CircleX}
            onClick={() => setDialog("cancel")}
          >
            Скасувати замовлення
          </Button>
        )}
      </div>

      <ConfirmModal
        opened={dialog === "cancel"}
        onClose={close}
        onConfirm={() => runAndClose(cancel)}
        title="Скасувати замовлення?"
        message={
          paidOnline
            ? "Кошти буде повернено на картку, з якої ви платили. Зазвичай це займає від кількох хвилин до кількох днів."
            : "Товари повернуться у продаж. Цю дію не можна скасувати."
        }
        confirmLabel="Скасувати замовлення"
        confirmColor="red"
        isLoading={cancel.isPending}
      />

      <ConfirmModal
        opened={dialog === "reject"}
        onClose={close}
        onConfirm={() => runAndClose(reject)}
        title="Відхилити замовлення?"
        message={
          paidOnline
            ? "Покупець отримає повне повернення коштів, а товари повернуться у продаж."
            : "Покупець побачить, що замовлення відхилено, а товари повернуться у продаж."
        }
        confirmLabel="Відхилити"
        confirmColor="red"
        isLoading={reject.isPending}
      />

      <ConfirmModal
        opened={dialog === "complete"}
        onClose={close}
        onConfirm={() => runAndClose(complete)}
        title={
          role === "buyer" ? "Підтвердити отримання?" : "Завершити замовлення?"
        }
        message={
          role === "buyer"
            ? "Підтвердіть, що отримали замовлення і з ним усе гаразд."
            : "Підтвердіть, що покупець отримав замовлення."
        }
        confirmLabel="Підтвердити"
        isLoading={complete.isPending}
      />

      {isPickup ? (
        <ConfirmModal
          opened={dialog === "ship"}
          onClose={close}
          onConfirm={() => runAndClose(ship, { trackingNumber: null })}
          title="Замовлення готове до видачі?"
          message="Покупець побачить, що замовлення можна забирати."
          confirmLabel="Готове до видачі"
          isLoading={ship.isPending}
        />
      ) : (
        <ShipDialog
          opened={dialog === "ship"}
          onClose={close}
          isLoading={ship.isPending}
          onSubmit={(trackingNumber) => runAndClose(ship, { trackingNumber })}
        />
      )}
    </>
  );
}

type ShipDialogProps = {
  opened: boolean;
  onClose: () => void;
  isLoading: boolean;
  onSubmit: (trackingNumber: string) => void;
};

function ShipDialog({ opened, onClose, isLoading, onSubmit }: ShipDialogProps) {
  const [trackingNumber, setTrackingNumber] = useState("");
  const [touched, setTouched] = useState(false);

  const value = trackingNumber.trim();
  const error = touched && !value ? "Вкажіть номер ТТН" : undefined;

  const submit = () => {
    setTouched(true);
    if (value) onSubmit(value);
  };

  return (
    <Modal
      opened={opened}
      onClose={onClose}
      centered
      radius="lg"
      title="Відправлення замовлення"
      zIndex={1002}
    >
      <form
        className={styles.form}
        onSubmit={(event) => {
          event.preventDefault();
          submit();
        }}
      >
        <Text fz={14} c="dimmed">
          Вкажіть номер експрес-накладної — покупець зможе відстежити посилку.
        </Text>
        <TextInput
          label="Номер ТТН"
          placeholder="20450000000000"
          value={trackingNumber}
          onChange={(event) => setTrackingNumber(event.currentTarget.value)}
          onBlur={() => setTouched(true)}
          error={error}
          maxLength={50}
          data-autofocus
        />
        <div className={styles.formActions}>
          <Button type="button" variant="outline" onClick={onClose}>
            Скасувати
          </Button>
          <Button type="submit" loading={isLoading} leftSection={Truck}>
            Відправити
          </Button>
        </div>
      </form>
    </Modal>
  );
}
