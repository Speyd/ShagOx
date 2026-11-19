import styles from "./AuthFields.module.css";

export default function ServerError({ message }: { message?: string }) {
  if (!message) return null;

  return (
    <p className={styles.serverError} role="alert">
      {message}
    </p>
  );
}
