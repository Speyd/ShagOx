import type { Region } from "./region";

export type City = {
  id: number;
  name?: string;
  label?: string;
  code?: string;
  region?: Region | null;
};
