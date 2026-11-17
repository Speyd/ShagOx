import { useState } from "react";
import { Badge, Skeleton, Text } from "@mantine/core";
import {
  CornerDownRight,
  MessageCircle,
  Pencil,
  Trash2,
  Trash2Icon,
} from "lucide-react";
import {
  CommentAuthor,
  getOwnerName,
  useGetReviewReplies,
  type ReviewReply,
} from "@/entities/review";
import { ReplyForm, useReplyMutations } from "@/features/review/manage-reply";
import Button from "@/shared/ui/buttons/button";
import styles from "./ReviewsSection.module.css";
import ConfirmModal from "@/shared/ui/feedback/confirm-modal";

export type ReplyTarget = { parentReplyId: number | null; name: string };

type ReviewRepliesProps = {
  reviewId: number;
  advertisementId: number;
  sellerId?: number;
  currentUserId?: number;
  replyTarget: ReplyTarget | null;
  onReply: (target: ReplyTarget) => void;
  onCloseReply: () => void;
};

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
  const { create, update, remove } = useReplyMutations(
    reviewId,
    advertisementId,
  );
  const [editingId, setEditingId] = useState<number | null>(null);

  const namesById = new Map(replies.map((r) => [r.id, getOwnerName(r.owner)]));

  const [replyToDelete, setReplyToDelete] = useState<ReviewReply | null>(null);

  const handleCreate = (text: string) =>
    create.mutate(
      { reviewId, parentReplyId: replyTarget?.parentReplyId ?? null, text },
      { onSuccess: onCloseReply },
    );

  const handleUpdate = (id: number, text: string) =>
    update.mutate({ id, text }, { onSuccess: () => setEditingId(null) });

  const handleConfirmDelete = () => {
    if (!replyToDelete) return;

    remove.mutate(replyToDelete.id, {
      onSuccess: () => {
        if (editingId === replyToDelete.id) setEditingId(null);
        setReplyToDelete(null);
      },
    });
  };

  const startEdit = (id: number) => {
    onCloseReply();
    setEditingId(id);
  };

  const startReply = (target: ReplyTarget) => {
    setEditingId(null);
    onReply(target);
  };

  if (isLoading) {
    return (
      <div className={styles.replies}>
        <Skeleton height={100} radius="md" />
      </div>
    );
  }

  if (replies.length === 0 && !replyTarget) return null;

  return (
    <div className={styles.replies}>
      {replies.map((reply) => {
        const isOwner = reply.owner.id === currentUserId;
        const isEditing = editingId === reply.id;
        const isDeleting = remove.isPending && remove.variables === reply.id;
        const parentName = reply.parentReplyId
          ? namesById.get(reply.parentReplyId)
          : undefined;

        return (
          <div key={reply.id} className={styles.reply}>
            <div className={styles.authorRow}>
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
            </div>

            {isEditing ? (
              <ReplyForm
                initialText={reply.text}
                submitLabel="Зберегти"
                isPending={update.isPending}
                onSubmit={(text) => handleUpdate(reply.id, text)}
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

            {currentUserId && !isEditing && (
              <div className={styles.actions}>
                <Button
                  variant="ghost"
                  size="sm"
                  leftSection={MessageCircle}
                  iconProps={{ size: 14 }}
                  onClick={() =>
                    startReply({
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
                      variant="ghost"
                      size="sm"
                      leftSection={Pencil}
                      iconProps={{ size: 14 }}
                      onClick={() => startEdit(reply.id)}
                      disabled={isDeleting}
                    >
                      Редагувати
                    </Button>
                    <Button
                      variant="ghost"
                      size="sm"
                      className={styles.dangerAction}
                      leftSection={Trash2}
                      iconProps={{ size: 14 }}
                      onClick={() => setReplyToDelete(reply)}
                      loading={isDeleting}
                    >
                      Видалити
                    </Button>
                  </>
                )}
              </div>
            )}
          </div>
        );
      })}

      {replyTarget && (
        <div className={styles.reply}>
          {replyTarget.parentReplyId && (
            <Text fz={12} c="dimmed" className={styles.replyTo}>
              <CornerDownRight size={12} />У відповідь {replyTarget.name}
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

      <ConfirmModal
        opened={Boolean(replyToDelete)}
        onClose={() => setReplyToDelete(null)}
        icon={Trash2Icon}
        iconProps={{ size: 32, color: "var(--mantine-color-red-6)" }}
        title="Видалити відповідь"
        message="Ви дійсно бажаєте видалити цю відповідь? Цю дію неможливо скасувати."
        confirmLabel="Видалити"
        confirmColor="red"
        onConfirm={handleConfirmDelete}
        isLoading={remove.isPending}
      />
    </div>
  );
}
