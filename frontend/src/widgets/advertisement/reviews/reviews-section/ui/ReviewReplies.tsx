import { useState } from "react";
import { Badge, Button, Group, Loader, Text } from "@mantine/core";
import {
  CommentAuthor,
  getOwnerName,
  useGetReviewReplies,
  type ReviewReply,
} from "@/entities/review";
import { ReplyForm, useReplyMutations } from "@/features/review/manage-reply";
import { confirmDelete } from "@/shared/lib/confirmDelete";
import styles from "./ReviewsSection.module.css";

export type ReplyTarget = { parentReplyId: number | null; name: string };

type ReviewRepliesProps = {
  reviewId: number;
  advertisementId: number;
  sellerId?: number;
  currentUserId?: number;
  /** Кому зараз відповідаємо (null — форма відповіді закрита) */
  replyTarget: ReplyTarget | null;
  onReply: (target: ReplyTarget) => void;
  onCloseReply: () => void;
};

/**
 * Відповіді на відгук — пласкою стрічкою за часом (як у більшості маркетплейсів).
 * Відповідь на відповідь позначається «у відповідь ...».
 */
export function ReviewReplies({
  reviewId,
  advertisementId,
  sellerId,
  currentUserId,
  replyTarget,
  onReply,
  onCloseReply,
}: ReviewRepliesProps) {
  const { data: replies = [], isLoading } = useGetReviewReplies(reviewId, true);
  const { create, update, remove } = useReplyMutations(reviewId, advertisementId);
  const [editingId, setEditingId] = useState<number | null>(null);

  const namesById = new Map(replies.map((r) => [r.id, getOwnerName(r.owner)]));

  const handleCreate = (text: string) =>
    create.mutate(
      { reviewId, parentReplyId: replyTarget?.parentReplyId ?? null, text },
      { onSuccess: onCloseReply },
    );

  const handleDelete = (reply: ReviewReply) =>
    confirmDelete({
      title: "Видалити відповідь",
      message: "Ви дійсно бажаєте видалити цю відповідь?",
      onConfirm: () => remove.mutate(reply.id),
    });

  return (
    <div className={styles.replies}>
      {isLoading && <Loader size="sm" />}

      {replies.map((reply) => {
        const isOwner = reply.owner.id === currentUserId;
        const parentName = reply.parentReplyId
          ? namesById.get(reply.parentReplyId)
          : undefined;

        return (
          <div key={reply.id} className={styles.reply}>
            <Group gap="xs">
              <CommentAuthor
                owner={reply.owner}
                createdAt={reply.createdAt}
                updatedAt={reply.updatedAt}
                size="sm"
              />
              {reply.owner.id === sellerId && (
                <Badge size="sm" variant="light">
                  Продавець
                </Badge>
              )}
            </Group>

            {editingId === reply.id ? (
              <ReplyForm
                initialText={reply.text}
                submitLabel="Зберегти"
                isPending={update.isPending}
                onSubmit={(text) =>
                  update.mutate(
                    { id: reply.id, text },
                    { onSuccess: () => setEditingId(null) },
                  )
                }
                onCancel={() => setEditingId(null)}
              />
            ) : (
              <Text fz={14} className={styles.text}>
                {parentName && (
                  <Text span c="var(--color-primary)" fw={500}>
                    @{parentName}{" "}
                  </Text>
                )}
                {reply.text}
              </Text>
            )}

            {currentUserId && editingId !== reply.id && (
              <Group gap={4}>
                <Button
                  size="compact-xs"
                  variant="subtle"
                  onClick={() =>
                    onReply({
                      parentReplyId: reply.id,
                      name: getOwnerName(reply.owner),
                    })
                  }
                >
                  Відповісти
                </Button>
                {isOwner && (
                  <>
                    <Button
                      size="compact-xs"
                      variant="subtle"
                      color="gray"
                      onClick={() => setEditingId(reply.id)}
                    >
                      Редагувати
                    </Button>
                    <Button
                      size="compact-xs"
                      variant="subtle"
                      color="red"
                      onClick={() => handleDelete(reply)}
                    >
                      Видалити
                    </Button>
                  </>
                )}
              </Group>
            )}
          </div>
        );
      })}

      {replyTarget && (
        <div className={styles.reply}>
          {replyTarget.parentReplyId && (
            <Text fz={12} c="dimmed" mb={4}>
              У відповідь {replyTarget.name}
            </Text>
          )}
          <ReplyForm
            key={replyTarget.parentReplyId ?? "review"}
            isPending={create.isPending}
            onSubmit={handleCreate}
            onCancel={onCloseReply}
          />
        </div>
      )}
    </div>
  );
}
