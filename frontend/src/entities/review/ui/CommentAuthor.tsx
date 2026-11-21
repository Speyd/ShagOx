import { Avatar, Text } from "@mantine/core";
import { formatCommentDate, getOwnerName } from "../lib/format";
import type { CommentOwner } from "../model/types";
import styles from "./Review.module.css";

type CommentAuthorProps = {
  owner: CommentOwner;
  createdAt: string;
  updatedAt?: string | null;
  size?: "sm" | "md";
};

export function CommentAuthor({
  owner,
  createdAt,
  updatedAt,
  size = "md",
}: CommentAuthorProps) {
  const name = getOwnerName(owner);

  return (
    <div className={styles.author}>
      <Avatar
        src={owner.avatar?.url}
        alt={name}
        name={name}
        color="initials"
        radius="xl"
        size={size === "md" ? 40 : 30}
      />
      <div>
        <Text fw={600} fz={size === "md" ? 14 : 13} c="#0f172a">
          {name}
        </Text>
        <Text fz={12} c="dimmed">
          {formatCommentDate(createdAt)}
          {updatedAt && " · змінено"}
        </Text>
      </div>
    </div>
  );
}
