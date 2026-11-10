import { useInfiniteQuery, useQueries, useQuery } from "@tanstack/react-query";
import {
  getReviewReplies,
  getReviews,
  getReviewsCount,
} from "../api/api";
import { getAdvertisementRating } from "../api/ratings";
import { reviewKeys } from "./queryKeys";
import type { ReviewStats } from "./types";

const GRADES = [5, 4, 3, 2, 1];

export function useGetReviews(advertisementId: number, grade?: number) {
  return useInfiniteQuery({
    queryKey: reviewKeys.list(advertisementId, grade),
    queryFn: ({ pageParam }) =>
      getReviews({ advertisementId, grade, page: pageParam }),
    initialPageParam: 1,
    getNextPageParam: (last) =>
      last.page < last.totalPages ? last.page + 1 : undefined,
    enabled: advertisementId > 0,
  });
}

export function useGetReviewStats(advertisementId: number) {
  return useQueries({
    queries: GRADES.map((grade) => ({
      queryKey: [...reviewKeys.stats(advertisementId), grade],
      queryFn: () => getReviewsCount(advertisementId, grade),
      enabled: advertisementId > 0,
      staleTime: 60_000,
    })),
    combine: (results): { data: ReviewStats; isLoading: boolean } => {
      const distribution = GRADES.map((grade, i) => ({
        grade,
        count: results[i].data ?? 0,
      }));
      const total = distribution.reduce((sum, d) => sum + d.count, 0);
      const sum = distribution.reduce((acc, d) => acc + d.grade * d.count, 0);

      return {
        data: { total, average: total ? sum / total : 0, distribution },
        isLoading: results.some((r) => r.isLoading),
      };
    },
  });
}

export function useGetMyReview(advertisementId: number, userId?: number) {
  return useQuery({
    queryKey: reviewKeys.mine(advertisementId, userId),
    queryFn: async () => {
      const page = await getReviews({ advertisementId, ownerId: userId, pageSize: 1 });
      return page.items[0] ?? null;
    },
    enabled: advertisementId > 0 && Boolean(userId),
  });
}

export function useAdvertisementRating(advertisementId: number, enabled = true) {
  return useQuery({
    queryKey: reviewKeys.rating(advertisementId),
    queryFn: () => getAdvertisementRating(advertisementId),
    enabled: enabled && advertisementId > 0,
    staleTime: 60_000,
  });
}

export function useGetReviewReplies(reviewId: number, enabled: boolean) {
  return useQuery({
    queryKey: reviewKeys.replies(reviewId),
    queryFn: () => getReviewReplies(reviewId),
    enabled,
  });
}
