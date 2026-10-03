import type { Region } from "./region";

export type City = {
  id: number;
  name?: string;
  lable?: string;
  code?: string;
  region?: Region | null;
};
