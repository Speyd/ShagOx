import { NavLink } from 'react-router-dom';
import styles from './ProfileTabs.module.css';

const TABS = [
  { label: 'Профіль та канал', to: '/profile' },
  { label: 'Мої відео', to: '/profile/videos' },
  { label: 'Обране', to: '/profile/favorites' },
  { label: 'Замовлення', to: '/profile/orders' },
  { label: 'Інформаційна панель', to: '/profile/statistics' },
];

export default function ProfileTabs() {
  return (
    <nav className={styles.tabsNav}>
      {TABS.map((tab) => (
        <NavLink
          key={tab.to}
          to={tab.to}
          end={tab.to === '/profile'}
          className={({ isActive }) =>
            `${styles.tabBtn} ${isActive ? styles.active : ''}`
          }
        >
          {tab.label}
        </NavLink>
      ))}
    </nav>
  );
}