export type Category = {
  id: number;
  code: string;
  productTypeId: number;
  label: string;
  entityStatus: string;
  productType?: {
    id?: number;
    code?: string;
    name?: string;
    label?: string;
    description?: string;
  } | null;
};
