import type { CommentOwner } from "../model/types";

export function getOwnerName(owner: CommentOwner): string {
  const fullName = [owner.firstName, owner.lastName].filter(Boolean).join(" ");
  return fullName || owner.userName || "Користувач";
}

export function formatCommentDate(date: string): string {
  return new Date(date).toLocaleDateString("uk-UA", {
    day: "numeric",
    month: "long",
    year: "numeric",
  });
}

export function pluralizeReviews(count: number): string {
  const mod10 = count % 10;
  const mod100 = count % 100;
  if (mod10 === 1 && mod100 !== 11) return `${count} відгук`;
  if (mod10 >= 2 && mod10 <= 4 && (mod100 < 12 || mod100 > 14))
    return `${count} відгуки`;
  return `${count} відгуків`;
}
