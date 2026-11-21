type WithSeller = { advertisement: { sellerId: number } };

export type SellerGroup<T> = {
  sellerId: number;
  items: T[];
};

/**
 * Групує позиції кошика за продавцем, зберігаючи порядок кошика.
 * Одне замовлення = один продавець: кожен продавець підтверджує,
 * відправляє й отримує оплату лише за свої товари.
 */
export function splitBySeller<T extends WithSeller>(
  items: T[],
): SellerGroup<T>[] {
  const groups = new Map<number, T[]>();

  for (const item of items) {
    const sellerId = item.advertisement.sellerId;
    const group = groups.get(sellerId);

    if (group) group.push(item);
    else groups.set(sellerId, [item]);
  }

  return Array.from(groups, ([sellerId, groupItems]) => ({
    sellerId,
    items: groupItems,
  }));
}
