import { api } from "@/shared/api/api";
import type { PaginatedResponse } from "@/shared/lib/types/paginatedResponse";
import type { Review, ReviewReply } from "../model/types";

export const REVIEWS_PAGE_SIZE = 10;

type ReviewsParams = {
  advertisementId: number;
  page?: number;
  pageSize?: number;
  grade?: number;
  ownerId?: number;
};

export async function getReviews({
  advertisementId,
  page = 1,
  pageSize = REVIEWS_PAGE_SIZE,
  grade,
  ownerId,
}: ReviewsParams) {
  const { data } = await api.get<PaginatedResponse<Review>>(
    "/review-comment/search",
    {
      params: {
        CommentableEntityId: advertisementId,
        Grade: grade,
        OwnerId: ownerId,
        Page: page,
        PageSize: pageSize,
      },
    },
  );
  return data;
}

export async function getReviewsCount(advertisementId: number, grade: number) {
  const { totalCount } = await getReviews({ advertisementId, grade, pageSize: 1 });
  return totalCount;
}

export async function getReviewReplies(reviewId: number) {
  const { data } = await api.get<PaginatedResponse<ReviewReply>>(
    "/review-repli-comment/search",
    { params: { CommentableEntityId: reviewId, Page: 1, PageSize: 100 } },
  );
  return data.items;
}
