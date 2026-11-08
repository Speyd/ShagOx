import { api } from "@/shared/api/api";
import type { ReviewRating } from "../model/types";

type RatingDto = { advertisementId: number; average: number; count: number };
type Pending = { resolve: (rating: ReviewRating) => void; reject: (error: unknown) => void };

const MAX_BATCH = 100;
const EMPTY_RATING: ReviewRating = { average: 0, count: 0 };

let queue = new Map<number, Pending[]>();
let scheduled = false;

async function requestRatings(ids: number[]) {
  const { data } = await api.get<RatingDto[]>("/review-comment/ratings", {
    params: { advertisementIds: ids },
    paramsSerializer: { indexes: null },
  });
  return new Map(data.map((r) => [r.advertisementId, { average: r.average, count: r.count }]));
}

async function flush() {
  const batch = queue;
  queue = new Map();
  scheduled = false;

  const ids = [...batch.keys()];
  for (let i = 0; i < ids.length; i += MAX_BATCH) {
    const chunk = ids.slice(i, i + MAX_BATCH);
    try {
      const ratings = await requestRatings(chunk);
      chunk.forEach((id) => batch.get(id)?.forEach((p) => p.resolve(ratings.get(id) ?? EMPTY_RATING)));
    } catch (error) {
      chunk.forEach((id) => batch.get(id)?.forEach((p) => p.reject(error)));
    }
  }
}

export function getAdvertisementRating(id: number): Promise<ReviewRating> {
  return new Promise((resolve, reject) => {
    queue.set(id, [...(queue.get(id) ?? []), { resolve, reject }]);
    if (!scheduled) {
      scheduled = true;
      setTimeout(flush, 0);
    }
  });
}
