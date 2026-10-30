import React, { useState } from "react";
import {  } from "@/entities/advertisement";
import { prepareCompareTable, type FilterMode } from "../lib/prepareCompareTable";
import { getAdvertisementPrice, type Advertisement } from "@/shared/lib/types/advertisements";

interface CompareTableProps {
  advertisements: Advertisement[];
  onRemove: (id: number) => void;
}

export const CompareTable: React.FC<CompareTableProps> = ({
  advertisements,
  onRemove,
}) => {
  const [filterMode, setFilterMode] = useState<FilterMode>("all");

  const rows = prepareCompareTable(advertisements, filterMode);

  if (!advertisements.length) {
    return <div>Список порівняння порожній</div>;
  }

  return (
    <div className="w-full overflow-x-auto">
      <div className="flex gap-2 mb-6">
        <button
          onClick={() => setFilterMode("all")}
          className={`px-4 py-2 rounded ${filterMode === "all" ? "bg-slate-900 text-white" : "bg-gray-100"}`}
        >
          Усі характеристики
        </button>
        <button
          onClick={() => setFilterMode("diff")}
          className={`px-4 py-2 rounded ${filterMode === "diff" ? "bg-slate-900 text-white" : "bg-gray-100"}`}
        >
          Тільки відмінності
        </button>
        <button
          onClick={() => setFilterMode("main")}
          className={`px-4 py-2 rounded ${filterMode === "main" ? "bg-slate-900 text-white" : "bg-gray-100"}`}
        >
          Основні
        </button>
      </div>

      <table className="w-full border-collapse text-left">
        <thead>
          <tr className="border-b">
            <th className="p-4 min-w-[200px] sticky left-0 bg-white z-10">
              Характеристики
            </th>
            {/* Колонки товарів */}
            {advertisements.map((ad) => (
              <th key={ad.id} className="p-4 min-w-[220px] align-top">
                <div className="relative border rounded-lg p-3">
                  <button
                    onClick={() => onRemove(ad.id)}
                    className="absolute top-2 right-2 text-gray-400 hover:text-red-500"
                  >
                    ✕
                  </button>
                  {ad.pictures?.[0] && (
                    <img
                      src={ad.pictures[0].url}
                      alt={ad.title}
                      className="h-32 object-contain mx-auto mb-2"
                    />
                  )}
                  <h4 className="font-semibold text-sm line-clamp-2">
                    {ad.title}
                  </h4>
                  <p className="font-bold mt-1">
                    {getAdvertisementPrice(ad)} {ad.currency?.symbol ?? "грн"}
                  </p>
                </div>
              </th>
            ))}
          </tr>
        </thead>
        <tbody>
          {rows.map((row) => (
            <tr key={row.key} className="border-b hover:bg-gray-50">
              <td className="p-4 font-medium text-gray-600 sticky left-0 bg-white z-10 border-r">
                {row.key}
              </td>
              {advertisements.map((ad) => (
                <td key={ad.id} className="p-4">
                  {row.values[ad.id]}
                </td>
              ))}
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};
