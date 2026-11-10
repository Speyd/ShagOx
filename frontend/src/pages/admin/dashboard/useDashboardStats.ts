import { useMemo } from "react";
import { useQuery } from "@tanstack/react-query";
import { api } from "@/shared/api/api";
import { ADMIN_KEY } from "@/shared/api/crudHooks";

export type DashboardTotals = {
  users: number;
  usersLast30Days: number;
  advertisements: number;
  advertisementsLast30Days: number;
  soldAdvertisements: number;
  totalStock: number;
  reviews: number;
  averageRating: number;
  categories: number;
  productTypes: number;
  attributeDictionaries: number;
  cities: number;
};

export type DashboardUser = {
  id: number;
  userName: string;
  email: string | null;
  phone: string | null;
  avatarUrl: string | null;
  registeredAt: string;
};

export type DashboardAdvertisement = {
  id: number;
  title: string;
  pictureUrl: string | null;
  price: number | null;
  currencySymbol: string;
  soldAt: string | null;
};

export type DashboardSeller = {
  id: number;
  userName: string;
  avatarUrl: string | null;
  advertisementsCount: number;
};

export type DashboardLowStock = {
  id: number;
  title: string;
  pictureUrl: string | null;
  sellerUserName: string | null;
  stock: number;
};

export type AdminDashboard = {
  totals: DashboardTotals;
  growth: { month: string; users: number; advertisements: number }[];
  advertisementsByCategory: { categoryId: number | null; name: string; count: number }[];
  grades: { grade: number; count: number }[];
  latestUsers: DashboardUser[];
  latestAdvertisements: DashboardAdvertisement[];
  topSellers: DashboardSeller[];
  lowStock: DashboardLowStock[];
};

async function fetchDashboard() {
  const { data } = await api.get<AdminDashboard>("/admin/dashboard");
  return data;
}

function monthLabel(month: string) {
  const [year, monthIndex] = month.split("-").map(Number);
  return new Date(year, monthIndex - 1, 1).toLocaleDateString("uk-UA", {
    month: "short",
    year: "2-digit",
  });
}

export function useDashboardStats() {
  const { data, isLoading } = useQuery({
    queryKey: [ADMIN_KEY, "dashboard"],
    queryFn: fetchDashboard,
    staleTime: 60_000,
  });

  const charts = useMemo(
    () => ({
      growth: (data?.growth ?? []).map((item) => ({
        month: monthLabel(item.month),
        users: item.users,
        ads: item.advertisements,
      })),
      grades: (data?.grades ?? []).map((item) => ({
        grade: `${item.grade} ★`,
        count: item.count,
      })),
      adsByCategory: (data?.advertisementsByCategory ?? []).map((item) => ({
        category: item.name,
        count: item.count,
      })),
    }),
    [data],
  );

  return {
    totals: data?.totals,
    charts,
    latestUsers: data?.latestUsers ?? [],
    latestAds: data?.latestAdvertisements ?? [],
    topSellers: data?.topSellers ?? [],
    lowStock: data?.lowStock ?? [],
    isLoading,
  };
}
