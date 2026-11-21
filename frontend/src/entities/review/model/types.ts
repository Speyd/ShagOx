export type CommentOwner = {
  id: number;
  firstName: string | null;
  lastName: string | null;
  userName: string | null;
  avatar: { url: string } | null;
};

export type CommentImage = {
  id: number;
  url: string;
  order: number;
};

type BaseComment = {
  id: number;
  owner: CommentOwner;
  commentableEntityId: number;
  text: string;
  createdAt: string;
  updatedAt: string | null;
};

export type Review = BaseComment & {
  grade: number;
  repliesCount: number;
  pictures: CommentImage[];
};

export type ReviewReply = BaseComment & {
  parentReplyId: number | null;
  pictures: CommentImage[];
};

export type ReviewRating = {
  average: number;
  count: number;
};

export type ReviewStats = {
  total: number;
  average: number;
  distribution: { grade: number; count: number }[];
};
