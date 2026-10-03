import { useQuery } from "@tanstack/react-query";

export function useGetImage(id?: number) {
  return useQuery({
    queryKey: ["image", id],
    queryFn: () => getImage(id!),
    enabled: Boolean(id),
    staleTime: Infinity,
  });
}

async function getImage(id: number) {
  const response = await fetch(`/api/images/${id}`);
  return response.json();
}
