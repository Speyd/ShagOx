type NamedUser = {
  firstName?: string | null;
  lastName?: string | null;
  userName?: string | null;
};

/** «Ім'я Прізвище», а якщо їх немає — нікнейм. */
export function getUserDisplayName(user: NamedUser | null | undefined) {
  if (!user) return null;

  const fullName = [user.firstName, user.lastName]
    .filter((part) => part && part.trim())
    .join(" ");

  return fullName || user.userName || null;
}
