import { CreditCard, Landmark, PieChart, Truck, Wallet } from "lucide-react";

export const paymentMethods = [
  {
    icon: <CreditCard size={30} />,
    label: "Банківські картки",
    description:
      "Оплачуйте замовлення онлайн за допомогою карток Visa або Mastercard. Безпека транзакцій гарантована.",
    buttons: [
      {
        label: "VISA",
      },
      {
        label: "MASTERCARD",
      },
    ],
  },
  {
    icon: <Wallet size={30} />,
    label: "Електронні гаманці",
    description:
      "Швидка та зручна оплата в один клік з використанням Apple Pay або Google Pay прямо з вашого пристрою.",
    buttons: [
      {
        label: "APPLE PAY",
      },
      {
        label: "GOOGLE PAY",
      },
    ],
  },
  {
    icon: <PieChart size={30} />,
    label: "Оплата частинами",
    description:
      "Розбийте суму покупки на кілька платежів без переплат завдяки програмам від банків-партнерів.",
    buttons: [
      {
        label: "PRIVATBANK",
      },
      {
        label: "MONO",
      },
    ],
  },
  {
    icon: <Truck size={30} />,
    label: "Післяплата",
    description:
      "Оплачуйте товар готівкою або карткою безпосередньо при отриманні у відділенні пошти або кур'єру.",
    buttons: [
      {
        label: "ГОТІВКА",
      },
      {
        label: "ТЕРМІНАЛ",
      },
    ],
  },
  {
    icon: <Landmark size={30} />,
    label: "Безготівковий розрахунок для B2B",
    description:
      "Ми надаємо повний пакет документів для юридичних осіб. Оплата здійснюється наrozрахунковий рахунок компанії згідно з виставленим рахунком-фактурою.",
    buttons: [
      {
        label: "ПДВ",
      },
      {
        label: "ДОГОВІР",
      },
    ],
  },
];
