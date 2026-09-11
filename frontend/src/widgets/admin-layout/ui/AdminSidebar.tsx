import { NavLink } from "react-router-dom";
import styles from "./AdminSidebar.module.css";

const links = [
  { to: "/admin", label: "Dashboard" },
  { to: "/admin/users", label: "Users" },
  { to: "/admin/advertisements", label: "Advertisements" },
  { to: "/admin/categories", label: "Categories" },
];

export default function AdminSidebar() {
  return (
    <div className={styles.sidebar}>
      <h2 className={styles.title}>Sidebar</h2>

      <ul className={styles.list}>
        {links.map((link) => (
          <li key={link.to}>
            <NavLink
              to={link.to}
              end
              className={({ isActive }) =>
                `${styles.link} ${isActive ? styles.active : ""}`
              }
            >
              {link.label}
            </NavLink>
          </li>
        ))}
      </ul>
    </div>
  );
}
