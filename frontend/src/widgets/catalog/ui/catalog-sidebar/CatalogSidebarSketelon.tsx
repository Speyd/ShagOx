import { Skeleton, Stack } from "@mantine/core";

export const CatalogSidebarSkeleton = () => {
  return (
    <Stack gap="md" p="md" style={{ maxWidth: 280 }}>
      <Skeleton height={32} width={140} mb="xs" radius="sm" />

      <Skeleton height={20} width="75%" radius="sm" />
      <Skeleton height={20} width="85%" radius="sm" />
      <Skeleton height={20} width="60%" radius="sm" />
      <Skeleton height={20} width="70%" radius="sm" />
      <Skeleton height={20} width="55%" radius="sm" />
      <Skeleton height={20} width="80%" radius="sm" />
      <Skeleton height={20} width="90%" radius="sm" />
      <Skeleton height={20} width="65%" radius="sm" />
    </Stack>
  );
};
