export type ProductType = {
  id: number;
  name: string;
  description: string;
  label: string;
  entityStatus: "Active" | "Inactive" | string;
};
