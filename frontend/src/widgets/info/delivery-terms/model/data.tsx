import { Archive, Store, Truck } from "lucide-react";

export const termsBlock = [
  {
    region: "Київ",
    deliveryService: "Кур'єр Marketly",
    terms: "В день замовлення (до 14:00)",
  },
  {
    region: "Київ",
    deliveryService: "Самовивіз з магазину",
    terms: "Від 1 години",
  },
  {
    region: "Україна (Обласні центри)",
    deliveryService: "Нова Пошта",
    terms: "1-2 дні",
  },
  {
    region: "Україна (Інші міста)",
    deliveryService: "Нова Пошта",
    terms: "2-4 дні",
  },
];

export const deliveryBlock = [
  {
    icon: <Truck size={30} />,
    label: "Кур'єрська доставка",
    description:
      "Доставка безпосередньо до ваших дверей. Доступна в межах Києва та передмістя. Зручний час узгоджується з менеджером.",
  },
  {
    icon: <Store size={30} />,
    label: "У відділення",
    description:
      "Доставка на обране відділення транспортної компанії. Термін зберігання посилки - 5 днів з моменту прибуття.",
  },
  {
    icon: <Archive size={30} />,
    label: "Поштомати",
    description:
      "Швидке та зручне отримання в поштоматах. Максимальні габарити та вага відправлення обмежені правилами перевізника.",
  },
];
