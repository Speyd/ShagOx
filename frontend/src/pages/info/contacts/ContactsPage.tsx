import { useNavigate } from "react-router-dom";
import {
  ArrowRight,
  Building2,
  Clock,
  Copy,
  Handshake,
  Headset,
  LifeBuoy,
  Mail,
  MapPin,
  MessageCircle,
  Newspaper,
  Phone,
  Send,
  Store,
  type LucideIcon,
} from "lucide-react";
import { toast } from "sonner";
import Container from "@/shared/ui/layout/container";
import Button from "@/shared/ui/buttons/button";
import styles from "./ContactsPage.module.css";

type Channel = {
  icon: LucideIcon;
  title: string;
  value: string;
  href: string;
  note: string;
};

// Contact details below are placeholders for the demo.
const CHANNELS: Channel[] = [
  {
    icon: Phone,
    title: "Гаряча лінія",
    value: "0 800 300 123",
    href: "tel:0800300123",
    note: "Безкоштовно по Україні",
  },
  {
    icon: Mail,
    title: "Email",
    value: "support@marketly.ua",
    href: "mailto:support@marketly.ua",
    note: "Відповідаємо протягом 24 годин",
  },
  {
    icon: Send,
    title: "Telegram",
    value: "@marketly_support",
    href: "https://t.me/marketly_support",
    note: "Швидкі відповіді в чаті",
  },
  {
    icon: MessageCircle,
    title: "Viber",
    value: "+380 67 300 12 34",
    href: "viber://chat?number=%2B380673001234",
    note: "Щодня з 8:00 до 22:00",
  },
];

const DEPARTMENTS: {
  icon: LucideIcon;
  title: string;
  description: string;
  email: string;
}[] = [
  {
    icon: Headset,
    title: "Покупцям",
    description: "Замовлення, оплата, доставка, повернення та гарантія.",
    email: "support@marketly.ua",
  },
  {
    icon: Store,
    title: "Продавцям",
    description: "Розміщення оголошень, верифікація магазину, виплати.",
    email: "sellers@marketly.ua",
  },
  {
    icon: Handshake,
    title: "Партнерство",
    description: "Логістика, платіжні сервіси та спільні проєкти.",
    email: "partners@marketly.ua",
  },
  {
    icon: Newspaper,
    title: "Для преси",
    description: "Інтерв'ю, коментарі, логотипи та медіаматеріали.",
    email: "press@marketly.ua",
  },
];

const SCHEDULE = [
  { days: "Пн – Пт", hours: "08:00 – 22:00" },
  { days: "Сб – Нд", hours: "10:00 – 20:00" },
  { days: "Онлайн-чат", hours: "Цілодобово" },
];

const copy = async (value: string) => {
  try {
    await navigator.clipboard.writeText(value);
    toast.success("Скопійовано в буфер обміну.");
  } catch {
    toast.error("Не вдалося скопіювати.");
  }
};

export default function ContactsPage() {
  const navigate = useNavigate();

  return (
    <main className={styles.page}>
      <Container className={styles.container}>
        <section className={styles.hero}>
          <div className={styles.heroContent}>
            <span className={styles.badge}>
              <LifeBuoy size={16} />
              Ми завжди на зв'язку
            </span>
            <h1 className={styles.title}>
              Зв'яжіться з <span className={styles.highlight}>Marketly</span>
            </h1>
            <p className={styles.description}>
              Маєте питання щодо замовлення, хочете стати продавцем чи
              запропонувати співпрацю? Оберіть зручний спосіб зв'язку — наша
              команда відповість якнайшвидше.
            </p>
            <div className={styles.heroActions}>
              <Button
                rightSection={ArrowRight}
                iconProps={{ size: 18 }}
                onClick={() => navigate("/support")}
              >
                Написати в підтримку
              </Button>
              <Button variant="outline" onClick={() => navigate("/reference")}>
                Довідковий центр
              </Button>
            </div>
          </div>

          <div className={styles.heroStats}>
            <div className={styles.heroStat}>
              <span className={styles.heroStatValue}>≈ 5 хв</span>
              <span className={styles.heroStatLabel}>
                середній час відповіді в чаті
              </span>
            </div>
            <div className={styles.heroStat}>
              <span className={styles.heroStatValue}>97%</span>
              <span className={styles.heroStatLabel}>
                звернень вирішуємо з першого разу
              </span>
            </div>
          </div>
        </section>

        <section className={styles.channels}>
          {CHANNELS.map((channel) => {
            const Icon = channel.icon;
            return (
              <div key={channel.title} className={styles.channelCard}>
                <div className={styles.channelTop}>
                  <span className={styles.iconWrapper}>
                    <Icon size={22} />
                  </span>
                  <button
                    type="button"
                    className={styles.copyButton}
                    onClick={() => copy(channel.value)}
                    aria-label={`Скопіювати: ${channel.value}`}
                    title="Скопіювати"
                  >
                    <Copy size={16} />
                  </button>
                </div>
                <span className={styles.channelTitle}>{channel.title}</span>
                <a className={styles.channelValue} href={channel.href}>
                  {channel.value}
                </a>
                <span className={styles.channelNote}>{channel.note}</span>
              </div>
            );
          })}
        </section>

        <section className={styles.officeGrid}>
          <div className={styles.card}>
            <div className={styles.cardHeader}>
              <Clock size={22} />
              <h2 className={styles.cardTitle}>Графік роботи підтримки</h2>
            </div>
            <ul className={styles.schedule}>
              {SCHEDULE.map((row) => (
                <li key={row.days} className={styles.scheduleRow}>
                  <span>{row.days}</span>
                  <span className={styles.scheduleHours}>{row.hours}</span>
                </li>
              ))}
            </ul>
            <p className={styles.cardNote}>
              У святкові дні гаряча лінія працює за скороченим графіком, а
              онлайн-чат — як завжди.
            </p>
          </div>

          <div className={`${styles.card} ${styles.officeCard}`}>
            <div className={styles.officeInfo}>
              <div className={styles.cardHeader}>
                <Building2 size={22} />
                <h2 className={styles.cardTitle}>Головний офіс</h2>
              </div>
              <p className={styles.address}>
                м. Київ, вул. Інноваційна, 12,
                <br />
                БЦ «Техноплаза», 5 поверх
              </p>
              <p className={styles.cardNote}>
                Прийом відвідувачів — за попереднім записом, Пн – Пт з 10:00 до
                18:00.
              </p>
            </div>

            <div className={styles.map} aria-hidden>
              <span className={styles.mapRoadH} />
              <span className={styles.mapRoadV} />
              <span className={styles.mapPin}>
                <MapPin size={28} />
              </span>
              <span className={styles.mapLabel}>Marketly HQ</span>
            </div>
          </div>
        </section>

        <section className={styles.departments}>
          <div className={styles.sectionHeader}>
            <h2 className={styles.sectionTitle}>Напишіть у потрібний відділ</h2>
            <p className={styles.sectionSubtitle}>
              Так ваше звернення одразу потрапить до потрібних фахівців
            </p>
          </div>

          <div className={styles.departmentGrid}>
            {DEPARTMENTS.map((department) => {
              const Icon = department.icon;
              return (
                <a
                  key={department.title}
                  href={`mailto:${department.email}`}
                  className={styles.departmentCard}
                >
                  <span className={styles.iconWrapper}>
                    <Icon size={22} />
                  </span>
                  <span className={styles.departmentTitle}>
                    {department.title}
                  </span>
                  <span className={styles.departmentDescription}>
                    {department.description}
                  </span>
                  <span className={styles.departmentEmail}>
                    {department.email}
                    <ArrowRight size={16} />
                  </span>
                </a>
              );
            })}
          </div>
        </section>

        <section className={styles.cta}>
          <div>
            <h2 className={styles.ctaTitle}>Не знайшли відповідь?</h2>
            <p className={styles.ctaText}>
              Залиште звернення — ми відповімо на вашу пошту протягом доби.
            </p>
          </div>
          <Button
            className={styles.ctaButton}
            onClick={() => navigate("/support")}
          >
            Створити звернення
          </Button>
        </section>
      </Container>
    </main>
  );
}
