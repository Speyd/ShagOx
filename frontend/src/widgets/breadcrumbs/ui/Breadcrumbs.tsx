import { Breadcrumbs as MantineBreadcrumbs, Anchor, Text } from "@mantine/core";
import { Link, useLocation } from "react-router-dom";

const ROUTE_LABELS: Record<string, string> = {
  advertisement: "Оголошення",
  catalog: "Каталог",
  profile: "Профіль",
  cart: "Кошик",
  favorites: "Обране",
};

type BreadcrumbsProps = {
  lastItemLabel?: string;
};

export default function Breadcrumbs({ lastItemLabel }: BreadcrumbsProps) {
  const location = useLocation();
  const pathnames = location.pathname.split("/").filter(Boolean);

  const items = [
    <Anchor component={Link} to="/" key="home" size="sm">
      Головна
    </Anchor>,
    ...pathnames.map((value, index) => {
      const to = `/${pathnames.slice(0, index + 1).join("/")}`;
      const isLast = index === pathnames.length - 1;

      let label = ROUTE_LABELS[value] || decodeURIComponent(value);

      if (isLast && lastItemLabel) {
        label = lastItemLabel;
      }

      return isLast ? (
        <Text key={to} c="dimmed" size="sm" fw={500}>
          {label}
        </Text>
      ) : (
        <Anchor component={Link} to={to} key={to} size="sm">
          {label}
        </Anchor>
      );
    }),
  ];

  return <MantineBreadcrumbs mb="md">{items}</MantineBreadcrumbs>;
}
