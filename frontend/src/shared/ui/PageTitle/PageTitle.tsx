import styles from "./PageTitle.module.css";

type PageTitleProps = {
  children: React.ReactNode;
};

export default function PageTitle({ children }: PageTitleProps) {
  return <h1 className={styles.title}>{children}</h1>;
}
