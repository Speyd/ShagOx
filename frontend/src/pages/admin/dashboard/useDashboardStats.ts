import { useMemo } from "react";
import { useQuery } from "@tanstack/react-query";
import { fetchAll, fetchPage } from "@/shared/api/crud";
import { ADMIN_KEY } from "@/shared/api/crudHooks";
import type { Advertisement } from "@/shared/lib/types/advertisements";
import type { AdminCategory, AdminReview } from "@/shared/lib/types/admin";
import type { User } from "@/shared/lib/types/user";

const DAY = 24 * 60 * 60 * 1000;
const MONTHS = 12;
const TOP_CATEGORIES = 8;

const monthKey = (date: Date) =>
  `${date.getFullYear()}-${String(date.getMonth() + 1).padStart(2, "0")}`;

function lastMonths(count: number) {
  const now = new Date();
  return Array.from({ length: count }, (_, index) => {
    const date = new Date(now.getFullYear(), now.getMonth() - (count - 1 - index), 1);
    return {
      key: monthKey(date),
      label: date.toLocaleDateString("uk-UA", { month: "short", year: "2-digit" }),
    };
  });
}

const countSince = (dates: string[], days: number) => {
  const from = Date.now() - days * DAY;
  return dates.filter((date) => new Date(date).getTime() >= from).length;
};

async function countOf(path: string) {
  const page = await fetchPage(path, { pageSize: 1, status: "ActiveAndHidden" });
  return page.totalCount;
}

export function useDashboardStats() {
  const users = useQuery({
    queryKey: [ADMIN_KEY, "dashboard", "users"],
    queryFn: () => fetchAll<User>("/admin/users", { status: "All" }),
  });
  const ads = useQuery({
    queryKey: [ADMIN_KEY, "dashboard", "ads"],
    queryFn: () => fetchAll<Advertisement>("/admin/advertisements", { status: "All" }),
  });
  const reviews = useQuery({
    queryKey: [ADMIN_KEY, "dashboard", "reviews"],
    queryFn: () => fetchAll<AdminReview>("/admin/review-comment", { status: "All" }),
  });
  const categories = useQuery({
    queryKey: [ADMIN_KEY, "dashboard", "categories"],
    queryFn: () => fetchAll<AdminCategory>("/admin/categories", { status: "ActiveAndHidden" }),
  });
  const catalogCounts = useQuery({
    queryKey: [ADMIN_KEY, "dashboard", "catalog-counts"],
    queryFn: async () => {
      const [productTypes, dictionaries, cities] = await Promise.all([
        countOf("/admin/product-types"),
        countOf("/admin/attribute-dictionaries"),
        countOf("/admin/cities"),
      ]);
      return { productTypes, dictionaries, cities };
    },
  });

  const stats = useMemo(() => {
    const userList = users.data ?? [];
    const adList = ads.data ?? [];
    const reviewList = reviews.data ?? [];
    const categoryList = categories.data ?? [];

    const months = lastMonths(MONTHS);
    const growth = months.map(({ key, label }) => ({
      month: label,
      users: userList.filter((u) => monthKey(new Date(u.registeredAt)) === key).length,
      ads: adList.filter((a) => monthKey(new Date(a.createdAt)) === key).length,
    }));

    const categoryLabels = new Map(categoryList.map((c) => [c.id, c.label || c.code]));
    const byCategory = new Map<string, number>();
    for (const ad of adList) {
      const name =
        categoryLabels.get(ad.category?.id) ?? ad.category?.lable ?? ad.category?.code ?? "Без категорії";
      byCategory.set(name, (byCategory.get(name) ?? 0) + 1);
    }
    const sortedCategories = [...byCategory.entries()].sort((a, b) => b[1] - a[1]);
    const adsByCategory = sortedCategories
      .slice(0, TOP_CATEGORIES)
      .map(([category, count]) => ({ category, count }));
    const otherCount = sortedCategories
      .slice(TOP_CATEGORIES)
      .reduce((sum, [, count]) => sum + count, 0);
    if (otherCount > 0) adsByCategory.push({ category: "Інші", count: otherCount });

    const grades = [5, 4, 3, 2, 1].map((grade) => ({
      grade: `${grade} ★`,
      count: reviewList.filter((r) => r.grade === grade).length,
    }));
    const rated = reviewList.filter((r) => typeof r.grade === "number");
    const averageRating = rated.length
      ? rated.reduce((sum, r) => sum + (r.grade ?? 0), 0) / rated.length
      : 0;

    const stockOf = (ad: Advertisement) =>
      (ad.variants ?? []).reduce((sum, v) => sum + (v.stock ?? 0), 0);
    const totalStock = adList.reduce((sum, ad) => sum + stockOf(ad), 0);
    const soldCount = adList.filter((ad) => ad.soldAt).length;
    const outOfStock = adList
      .filter((ad) => !ad.soldAt && stockOf(ad) <= 2)
      .sort((a, b) => stockOf(a) - stockOf(b))
      .slice(0, 5)
      .map((ad) => ({ ad, stock: stockOf(ad) }));

    const sellers = new Map<number, { user: Advertisement["seller"]; count: number }>();
    for (const ad of adList) {
      if (!ad.seller) continue;
      const entry = sellers.get(ad.seller.id) ?? { user: ad.seller, count: 0 };
      entry.count += 1;
      sellers.set(ad.seller.id, entry);
    }
    const topSellers = [...sellers.values()].sort((a, b) => b.count - a.count).slice(0, 5);

    const byDateDesc = <T,>(list: T[], date: (item: T) => string) =>
      [...list].sort((a, b) => new Date(date(b)).getTime() - new Date(date(a)).getTime());

    return {
      usersTotal: userList.length,
      usersLast30: countSince(userList.map((u) => u.registeredAt), 30),
      adsTotal: adList.length,
      adsLast30: countSince(adList.map((a) => a.createdAt), 30),
      reviewsTotal: reviewList.length,
      averageRating,
      categoriesTotal: categoryList.length,
      totalStock,
      soldCount,
      growth,
      adsByCategory,
      grades,
      outOfStock,
      topSellers,
      latestUsers: byDateDesc(userList, (u) => u.registeredAt).slice(0, 5),
      latestAds: byDateDesc(adList, (a) => a.createdAt).slice(0, 5),
    };
  }, [users.data, ads.data, reviews.data, categories.data]);

  return {
    stats,
    catalogCounts: catalogCounts.data,
    isLoading: users.isLoading || ads.isLoading || reviews.isLoading || categories.isLoading,
  };
}
