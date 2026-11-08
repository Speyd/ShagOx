import { useState } from "react";
import { ActionIcon, Badge, Button, Group, Menu, Rating, Text } from "@mantine/core";
import { MessageCircle, MoreVertical, Pencil, Trash2 } from "lucide-react";
import {
  CommentAuthor,
  CommentPhotos,
  getOwnerName,
  type Review,
} from "@/entities/review";
import { ReviewReplies, type ReplyTarget } from "./ReviewReplies";
import styles from "./ReviewsSection.module.css";

type ReviewCardProps = {
  review: Review;
  advertisementId: number;
  sellerId?: number;
  currentUserId?: number;
  onEdit: (review: Review) => void;
  onDelete: (review: Review) => void;
};

export function ReviewCard({
  review,
  advertisementId,
  sellerId,
  currentUserId,
  onEdit,
  onDelete,
}: ReviewCardProps) {
  const [expanded, setExpanded] = useState(false);
  const [replyTarget, setReplyTarget] = useState<ReplyTarget | null>(null);

  const isOwner = review.owner.id === currentUserId;
  const showReplies = expanded || replyTarget !== null;

  const startReply = (target: ReplyTarget) => {
    setExpanded(true);
    setReplyTarget(target);
  };

  return (
    <article className={styles.reviewCard}>
      <div className={styles.cardHeader}>
        <Group gap="xs">
          <CommentAuthor
            owner={review.owner}
            createdAt={review.createdAt}
            updatedAt={review.updatedAt}
          />
          {isOwner && (
            <Badge size="sm" variant="light" color="gray">
              Ваш відгук
            </Badge>
          )}
        </Group>

        {isOwner && (
          <Menu position="bottom-end" withinPortal>
            <Menu.Target>
              <ActionIcon variant="subtle" color="gray" aria-label="Дії з відгуком">
                <MoreVertical size={18} />
              </ActionIcon>
            </Menu.Target>
            <Menu.Dropdown>
              <Menu.Item leftSection={<Pencil size={14} />} onClick={() => onEdit(review)}>
                Редагувати
              </Menu.Item>
              <Menu.Item
                color="red"
                leftSection={<Trash2 size={14} />}
                onClick={() => onDelete(review)}
              >
                Видалити
              </Menu.Item>
            </Menu.Dropdown>
          </Menu>
        )}
      </div>

      <Rating value={review.grade} readOnly color="#fea945" size="sm" />

      <Text fz={14} c="#0f172a" className={styles.text}>
        {review.text}
      </Text>

      <CommentPhotos pictures={review.pictures} />

      <Group gap="xs">
        {currentUserId && (
          <Button
            size="compact-sm"
            variant="subtle"
            leftSection={<MessageCircle size={14} />}
            onClick={() =>
              startReply({ parentReplyId: null, name: getOwnerName(review.owner) })
            }
          >
            Відповісти
          </Button>
        )}
        {review.repliesCount > 0 && (
          <Button
            size="compact-sm"
            variant="subtle"
            color="gray"
            onClick={() => {
              setExpanded(!showReplies);
              if (showReplies) setReplyTarget(null);
            }}
          >
            {showReplies ? "Сховати відповіді" : `Відповіді (${review.repliesCount})`}
          </Button>
        )}
      </Group>

      {showReplies && (
        <ReviewReplies
          reviewId={review.id}
          advertisementId={advertisementId}
          sellerId={sellerId}
          currentUserId={currentUserId}
          replyTarget={replyTarget}
          onReply={startReply}
          onCloseReply={() => setReplyTarget(null)}
        />
      )}
    </article>
  );
}
