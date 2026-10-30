import { NavLink } from "react-router-dom";
import clsx from "clsx";
import { Text } from "@mantine/core";
import { useGetProductTypes } from "@/entities/advertisement/model/hooks/useGetProductTypes";
import styles from "./CatalogSidebar.module.css";

export default function CatalogSidebar() {
  const { data: productTypes, isLoading: isLoadingTypes } =
    useGetProductTypes();

  if (isLoadingTypes || !productTypes) return null;

  return (
    <aside className={styles.catalogSidebar}>
      <nav className={styles.navigation}>
        {productTypes.map(({ id, name, label }) => (
          <NavLink
            key={id}
            to={`/catalog/${name}`}
            className={({ isActive }) =>
              clsx(styles.menuItem, isActive && styles.menuItemActive)
            }
          >
            <Text fz={14}>{label}</Text>
          </NavLink>
        ))}
      </nav>
    </aside>
  );
}
