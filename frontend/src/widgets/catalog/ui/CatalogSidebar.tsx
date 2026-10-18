import { NavLink } from "react-router-dom";
import clsx from "clsx";
import { Text } from "@mantine/core";
import { useGetProductTypes } from "@/entities/advertisement/model/hooks/useGetProductTypes";
import styles from "./CatalogSidebar.module.css";

export default function CatalogSidebar() {
  const { data: productTypes, isLoading: isLoadingTypes } =
    useGetProductTypes();

  if (isLoadingTypes) return null;
  if (!productTypes) return null;

  return (
    <section className={styles.catalogSidebar}>
      <nav className={styles.navigation}>
        {productTypes.map(({ id, lable: label }) => (
          <NavLink
            key={id}
            to={`/catalog/${id}`}
            className={({ isActive }) =>
              clsx(styles.menuItem, {
                [styles.menuItemActive]: isActive,
              })
            }
          >
            <Text fz={14}>{label}</Text>
          </NavLink>
        ))}
      </nav>
    </section>
  );
}
