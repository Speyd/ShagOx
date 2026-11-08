export const reviewKeys = {
  all: ["reviews"] as const,
  list: (advertisementId: number, grade?: number) =>
    ["reviews", advertisementId, "list", grade ?? "all"] as const,
  stats: (advertisementId: number) =>
    ["reviews", advertisementId, "stats"] as const,
  rating: (advertisementId: number) =>
    ["reviews", advertisementId, "rating"] as const,
  mine: (advertisementId: number, userId?: number) =>
    ["reviews", advertisementId, "mine", userId] as const,
  byAdvertisement: (advertisementId: number) =>
    ["reviews", advertisementId] as const,
  replies: (reviewId: number) => ["review-replies", reviewId] as const,
};
