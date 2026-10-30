import { useCompareStore } from "@/entities/compare/model/compareStore";
import { CompareTable } from "@/widgets/compare-table/ui/CompareTable";
import { useMemo, useState } from "react";

export default function ComparePage() {
  const { compareList, removeAdvertisement } = useCompareStore();

  const categories = useMemo(() => {
    const map = new Map<number, { id: number; name: string }>();
    compareList.forEach((item) => {
      if (item.category && !map.has(item.category.id)) {
        map.set(item.category.id, item.category);
      }
    });
    return Array.from(map.values());
  }, [compareList]);

  const [activeCategoryId, setActiveCategoryId] = useState<number | null>(
    categories[0]?.id ?? null,
  );

  const activeAds = useMemo(() => {
    const categoryId = activeCategoryId ?? categories[0]?.id;
    return compareList.filter((item) => item.category?.id === categoryId);
  }, [compareList, activeCategoryId, categories]);

  return (
    <div className="container mx-auto py-8">
      <h1 className="text-2xl font-bold mb-6">Порівняння товарів</h1>

      <div className="flex border-b mb-6">
        {categories.map((cat) => (
          <button
            key={cat.id}
            onClick={() => setActiveCategoryId(cat.id)}
            className={`px-6 py-3 font-medium border-b-2 ${
              (activeCategoryId ?? categories[0]?.id) === cat.id
                ? "border-blue-600 text-blue-600"
                : "border-transparent text-gray-500"
            }`}
          >
            {cat.name} (
            {compareList.filter((i) => i.category?.id === cat.id).length})
          </button>
        ))}
      </div>

      <CompareTable advertisements={activeAds} onRemove={removeAdvertisement} />
    </div>
  );
}
