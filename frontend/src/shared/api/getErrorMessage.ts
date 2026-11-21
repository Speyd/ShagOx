import axios from "axios";

type ProblemBody = {
  message?: string;
  title?: string;
  detail?: string;
  errors?: Record<string, string[] | string>;
};

function fromProblem(body: ProblemBody): string | null {
  if (body.errors && typeof body.errors === "object") {
    const first = Object.values(body.errors).flat()[0];
    if (first) return String(first);
  }
  return body.message || body.detail || body.title || null;
}

export function getErrorMessage(error: unknown, fallback: string): string {
  if (!axios.isAxiosError(error)) return fallback;

  const data = error.response?.data;

  if (typeof data === "string") return data || fallback;
  if (data && typeof data === "object") {
    return fromProblem(data as ProblemBody) ?? fallback;
  }

  return fallback;
}
