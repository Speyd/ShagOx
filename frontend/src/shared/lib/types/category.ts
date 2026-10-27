export type Category = {
  id: number;
  code: string;
  productType: {
    id: number;
    code: string;
    label: string;
  };
  label: string;
};
